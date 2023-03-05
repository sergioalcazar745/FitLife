using FitLife.Data;
using FitLife.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

#region PROCEDURES
//CREATE PROCEDURE SP_REGISTER_USER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50))
//AS
//    DECLARE @ID INT
//	SELECT @ID = MAX(IDUSUARIO) FROM USUARIOS
//	IF @ID != NULL
//	BEGIN
//	INSERT INTO USUARIOS VALUES(@ID, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORD, @ROLE)
//	END
//GO

//ALTER PROCEDURE SP_REGISTER_OTHERUSER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), 
//@ALTURA INT, @PESO INT, @EDAD INT, @SEXO NVARCHAR(50))
//AS
//    DECLARE @IDUSU INT
//	DECLARE @IDPERFIL INT
//	SELECT @IDUSU = MAX(IDUSUARIO) FROM USUARIOS
//	SELECT @IDPERFIL = MAX(IDPERFILUSUARIO) FROM PERFILUSUARIO
//	IF @IDUSU = NULL
//	BEGIN
//	SET @IDUSU = 1
//	END
//	ELSE
//	BEGIN 
//	SET @IDUSU = @IDUSU + 1
//	END
//	IF @IDPERFIL = NULL
//	BEGIN
//	SET @IDPERFIL = 1
//	END
//	ELSE
//	BEGIN 
//	SET @IDPERFIL = @IDPERFIL + 1
//	END
//	INSERT INTO USUARIOS VALUES(@IDUSU, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORD, @ROLE)
//	INSERT INTO PERFILUSUARIO VALUES(@IDPERFIL, @EDAD, @SEXO, @ALTURA, @PESO, @IDUSU)
//GO
#endregion

namespace FitLife.Repositories
{
    public class RepositorySQL : IRepository
    {
        private FitLifeContext context;
        public RepositorySQL(FitLifeContext context)
        {
            this.context = context;
        }

        public Usuario Login(string email, string password)
        {
            var consulta = from datos in this.context.Usuarios.AsEnumerable()
                           where datos.Email == email && datos.Password == password
                           select datos;
            return consulta.FirstOrDefault();
        }

        public Usuario FindUsuario(int idUsuario)
        {
            var consulta = from datos in this.context.Usuarios.AsEnumerable()
                           where datos.IdUsuario == idUsuario
                           select datos;
            return consulta.FirstOrDefault();
        }

        //public PerfilUsuario FindPerfilUsuario(int idUsuario)
        //{
        //    var consulta = from datos in this.context.PerfilUsuarios.AsEnumerable()
        //                   where datos.IdUsuario == idUsuario
        //                   select datos;
        //    return consulta.FirstOrDefault();
        //}

        public async Task RegistrarCliente(string nombre, string apellidos, string dni, string email, string password, string role, int altura, int peso, int edad, string sexo)
        {
            string sql = "SP_REGISTER_OTHERUSER @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORD, @ROLE, @ALTURA, @EDAD, @PESO, @SEXO";
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paraapellidos = new SqlParameter("@APELLIDOS", apellidos);
            SqlParameter paradni = new SqlParameter("@DNI", dni);
            SqlParameter paraemail = new SqlParameter("@EMAIL", email);
            SqlParameter parapassword = new SqlParameter("@PASSWORD", password);
            SqlParameter pararole = new SqlParameter("@ROLE", role);
            SqlParameter paraaltura = new SqlParameter("@ALTURA", altura);
            SqlParameter parapeso = new SqlParameter("@PESO", peso);
            SqlParameter paraedad = new SqlParameter("@EDAD", edad);
            SqlParameter parasexo = new SqlParameter("@SEXO", sexo);
            await this.context.Database.ExecuteSqlRawAsync(sql, paranombre, paraapellidos, paradni, paraemail, parapassword, pararole, paraaltura, parapeso, paraedad, parasexo);
        }

        public async Task RegistrarUsuario(string nombre, string apellidos, string dni, string email, string password, string role)
        {
            string sql = "SP_REGISTER_USER @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORD, @ROLE";
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paraapellidos = new SqlParameter("@APELLIDOS", apellidos);
            SqlParameter paradni = new SqlParameter("@DNI", dni);
            SqlParameter paraemail = new SqlParameter("@EMAIL", email);
            SqlParameter parapassword = new SqlParameter("@PASSWORD", password);
            SqlParameter pararole = new SqlParameter("@ROLE", role);
            await this.context.Database.ExecuteSqlRawAsync(sql, paranombre, paraapellidos, paradni, paraemail, parapassword, pararole);
        }
    }
}
