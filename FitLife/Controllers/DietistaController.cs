using FitLife.Extensions;
using FitLife.Filters;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace FitLife.Controllers
{
    [AuthorizeUsers(Policy = "Nutricionista")]
    public class DietistaController : Controller
    {
        IRepository repo;
        IMemoryCache memory;
        public DietistaController(IRepository repo, IMemoryCache memory)
        {
            this.repo = repo;
            this.memory = memory;
        }

        public async Task<IActionResult> Dietas()
        {
            string mensaje = TempData["MENSAJEDIETAS"] as string;
            if (mensaje != null)
            {
                ViewData["MENSAJE"] = mensaje;
            }
            int idcliente = this.memory.Get<int>("idcliente");
            List<ModelDieta> dietas = await this.repo.DietasId(idcliente);
            return View(dietas);
        }

        [HttpPost]
        public async Task<IActionResult> Dietas(DateTime fechainicio, DateTime fechafinal)
        {
            TempData.Remove("MENSAJEDIETAS");
            int idcliente = this.memory.Get<int>("idcliente");
            int idnutricionista = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<ModelDieta> dietas = await this.repo.FilterDietas(fechainicio, fechafinal, idcliente, idnutricionista);
            ViewData["FILTRO"] = dietas.Count + " RESULTADOS";
            return View(dietas);
        }

        public async Task<IActionResult> DetallesDieta(int iddieta)
        {
            string error = TempData["Error"] as string;
            if(error != null)
            {
                ViewData["Error"] = error;
            }
            Dieta dieta = await this.repo.GetDieta(iddieta);
            List<ModelComidaAlimento> modelComidaAlimento = null;
            if (dieta != null)
            {
                List<Comida> comidas = await this.repo.GetComidas(dieta.IdDieta);
                modelComidaAlimento = new List<ModelComidaAlimento>();
                foreach (Comida comida in comidas)
                {
                    List<ModelComidaAlimentoNombre> comidaalimento = await this.repo.GetComidaAlimento(dieta.IdDieta, comida.IdComida);
                    ModelComidaAlimento model = new ModelComidaAlimento { Comida = comida, ComidaAlimento = comidaalimento };
                    modelComidaAlimento.Add(model);
                }
            }
            ViewData["DIETA"] = dieta;
            List<Alimento> alimentos = await this.repo.Alimentos();
            ViewData["ALIMENTOS"] = alimentos;
            HttpContext.Session.Remove("Alimentos");
            return View(modelComidaAlimento);
        }

        [HttpPost]
        public async Task<IActionResult> DetallesDieta(int iddieta, DateTime fecha, string nombre)
        {
            int response = await this.repo.ActualizarDieta(iddieta, fecha, nombre);
            if(response == 0)
            {
                TempData["Error"] = "Ya existe una dieta con esa fecha";
                return RedirectToAction("DetallesDieta", new { iddieta = iddieta });
            }
            TempData["MENSAJEDIETAS"] = "Se ha guardado correctamente";
            return RedirectToAction("Dietas");
        }

        public async Task<IActionResult> CrearDieta()
        {
            HttpContext.Session.Remove("Alimentos");
            List<Alimento> alimentos = await this.repo.Alimentos();
            return View(alimentos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearDieta(DateTime fecha, string nombre)
        {
            List<AlimentoAñadir> alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            if(alimentos.Count() == 0)
            {
                return View();
            }

            int idcliente = this.memory.Get<int>("idcliente");
            int idnutricionista = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            int iddieta = await this.repo.AñadirDieta(idnutricionista, idcliente, fecha, nombre);
            string[] comidas = { "Desayuno", "Almuerzo", "Comida", "Merienda", "Cena" };
            foreach (string tipo in comidas)
            {
                List<AlimentoAñadir> alimentosAñadir = alimentos.FindAll(z => z.Comida == tipo);
                if (alimentosAñadir.Count() > 0)
                {
                    double totalKcals = alimentosAñadir.Sum(z => z.Kcal);
                    int idcomida = await this.repo.CrearComida(iddieta, tipo, totalKcals);
                    await this.repo.AñadirAlimentosDieta(alimentosAñadir, iddieta, idcomida);
                }
            }
            HttpContext.Session.Remove("Alimentos");
            TempData["MENSAJEDIETAS"] = "Se ha guardado correctamente";
            return RedirectToAction("Dietas");
        }

        [HttpPost]
        public async Task<IActionResult> AñadirAlimento(int idalimentoañadir, string comida, int alimento, int peso)
        {
            AlimentoAñadir alimentoAñadir = new AlimentoAñadir();
            alimentoAñadir.IdAlimentoAñadir = idalimentoañadir;
            alimentoAñadir.Alimento = alimento;
            alimentoAñadir.Comida = comida;
            alimentoAñadir.Peso = peso;

            List<AlimentoAñadir> alimentos;
            if(HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos") == null)
            {
                alimentos = new List<AlimentoAñadir>();
            }
            else
            {
                alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            }

            Alimento alimentoCheck = await this.repo.GetAlimento(alimento);
            alimentoAñadir.Kcal = (alimentoCheck.Kcal * peso) / 100;
            alimentoAñadir.Carbohidratos = (alimentoAñadir.Kcal * alimentoCheck.Carbohidratos) / alimentoCheck.Kcal;
            alimentoAñadir.Proteinas = (alimentoAñadir.Kcal * alimentoCheck.Proteinas) / alimentoCheck.Kcal;
            alimentoAñadir.Fibra = (alimentoAñadir.Kcal * alimentoCheck.Fibra) / alimentoCheck.Kcal;
            alimentoAñadir.Grasas = (alimentoAñadir.Kcal * alimentoCheck.Grasas) / alimentoCheck.Kcal;

            alimentos.Add(alimentoAñadir);
            HttpContext.Session.SetObject("Alimentos", alimentos);
            return Json(alimentoAñadir);
        }

        [HttpPost]
        public async Task<IActionResult> AñadirAlimentoDetalles(int iddieta, int idcomida, string comida, int idalimento, int peso)
        {
            if(idcomida == 0)
            {
                Alimento alimento = await this.repo.GetAlimento(idalimento);
                double kcal = (alimento.Kcal * peso) / 100;
                idcomida = await this.repo.CrearComida(iddieta, comida, kcal);
            }
            await this.repo.AñadirComidaAlimento(iddieta, idcomida, idalimento, peso);
            return Json("");
        }

        [HttpPost]
        public async Task<IActionResult> ModificarAlimento(int idcomidaalimento, int idcomida, string comida, int iddieta, int idalimento, int peso)
        {
            bool existe = true;
            if(idcomida == 0)
            {
                Alimento alimento = await this.repo.GetAlimento(idalimento);
                double kcal = (alimento.Kcal * peso) / 100;
                idcomida = await this.repo.CrearComida(iddieta, comida, kcal);
                existe = false;
            }
            await this.repo.ActualizarComidaAlimento(idcomidaalimento, idcomida, idalimento, peso, existe);
            return Json("");   
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAlimento(int id, int idcomida)
        {
            List<AlimentoAñadir> alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            AlimentoAñadir alimento = alimentos.Find(x => x.IdAlimentoAñadir == id);
            alimentos.Remove(alimento);
            HttpContext.Session.SetObject("Alimentos", alimentos);
            return Json(alimento.Comida[0].ToString().ToLower() + alimento.Comida.Substring(1));
        }

        public async Task<IActionResult> EliminarAlimentoDetalles(int id)
        {
            await this.repo.EliminarComidaAlimento(id);
            return Json("");
        }

        public async Task<IActionResult> EliminarDieta(int iddieta)
        {
            await this.repo.EliminarDieta(iddieta);
            return RedirectToAction("Dietas");
        }
    }
}
