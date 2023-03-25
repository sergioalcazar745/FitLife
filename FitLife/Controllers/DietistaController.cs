using FitLife.Extensions;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace FitLife.Controllers
{
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
            int idcliente = this.memory.Get<int>("idcliente");
            List<ModelDieta> dietas = await this.repo.DietasId(idcliente);
            return View(dietas);
        }

        [HttpPost]
        public async Task<IActionResult> Dietas(DateTime fechainicio, DateTime fechafinal)
        {
            int idcliente = this.memory.Get<int>("idcliente");
            int idnutricionista = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<ModelDieta> dietas = await this.repo.FilterDietas(fechainicio, fechafinal, idcliente, idnutricionista);
            ViewData["FILTRO"] = dietas.Count + " RESULTADOS";
            return View(dietas);
        }

        public async Task<IActionResult> DetallesDieta(int iddieta)
        {
            int idcliente = this.memory.Get<int>("idcliente");
            Dieta dieta = await this.repo.GetDieta(iddieta);
            //Buscar comida y alimentos
            
            return View(dieta);
        }

        public async Task<IActionResult> CrearDieta()
        {
            HttpContext.Session.Remove("Alimentos");
            HttpContext.Session.Remove("TotalKcal");
            List<Alimento> alimentos = await this.repo.Alimentos();
            return View(alimentos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearDieta(DateTime fecha, string nombre)
        {
            List<AlimentoAñadir> alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            List<ModelKcal> totalkcal = HttpContext.Session.GetObject<List<ModelKcal>>("TotalKcal");
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

            //List<ModelKcal> totalkcals = null;
            //if (HttpContext.Session.GetObject<List<ModelKcal>>("TotalKcal") == null)
            //{
            //    totalkcals = new List<ModelKcal>() {
            //    new ModelKcal() { Comida = "Desayuno", TotalKcal= 0 },
            //    new ModelKcal() { Comida = "Almuerzo", TotalKcal = 0 },
            //    new ModelKcal() { Comida = "Comida", TotalKcal = 0 },
            //    new ModelKcal() { Comida = "Merienda", TotalKcal = 0 },
            //    new ModelKcal() { Comida = "Cena", TotalKcal = 0 } };
            //}
            //else
            //{
            //    totalkcals = HttpContext.Session.GetObject<List<ModelKcal>>("TotalKcal");
            //}

            Alimento alimentoCheck = await this.repo.GetAlimento(alimento);
            alimentoAñadir.Kcal = (alimentoCheck.Kcal * peso) / 100;
            alimentoAñadir.Carbohidratos = (alimentoAñadir.Kcal * alimentoCheck.Carbohidratos) / alimentoCheck.Kcal;
            alimentoAñadir.Proteinas = (alimentoAñadir.Kcal * alimentoCheck.Proteinas) / alimentoCheck.Kcal;
            alimentoAñadir.Fibra = (alimentoAñadir.Kcal * alimentoCheck.Fibra) / alimentoCheck.Kcal;
            alimentoAñadir.Grasas = (alimentoAñadir.Kcal * alimentoCheck.Grasas) / alimentoCheck.Kcal;

            //totalkcals.FirstOrDefault(z => z.Comida == comida).TotalKcal += alimentoAñadir.Kcal;
            alimentos.Add(alimentoAñadir);
            HttpContext.Session.SetObject("Alimentos", alimentos);
            //HttpContext.Session.SetObject("TotalKcal", totalkcals);
            return Json(alimentoAñadir);
        }

        [HttpPost]
        public IActionResult EliminarAlimento(int id)
        {
            //List<ModelKcal> totalkcals = HttpContext.Session.GetObject<List<ModelKcal>>("TotalKcal");
            List<AlimentoAñadir> alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            AlimentoAñadir alimento = alimentos.Find(x => x.IdAlimentoAñadir == id);
            alimentos.Remove(alimento);
            //totalkcals.FirstOrDefault(z => z.Comida == alimento.Comida).TotalKcal -= alimento.Kcal;
            HttpContext.Session.SetObject("Alimentos", alimentos);
            //HttpContext.Session.SetObject("TotalKcal", totalkcals);
            return Json(alimento.Comida[0].ToString().ToLower() + alimento.Comida.Substring(1));
        }
    }
}
