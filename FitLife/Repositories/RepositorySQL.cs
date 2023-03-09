using FitLife.Data;
using FitLife.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;

#region PROCEDURES
//ALTER PROCEDURE SP_REGISTER_USER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORDENCRYPT VARBINARY(MAX), @SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50))
//AS
//DECLARE @ID INT
//	SELECT @ID = MAX(IDUSUARIO) FROM USUARIOS
//	IF @ID IS NULL
//	BEGIN
//	SET @ID = 1
//	END
//	ELSE
//	BEGIN
//	SET @ID = @ID + 1
//	END
//	INSERT INTO USUARIOS VALUES(@ID, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE)
//GO

//CREATE PROCEDURE SP_REGISTER_OTHERUSER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORDENCRYPT VARBINARY(MAX), @SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), 
//@ALTURA INT, @PESO INT, @EDAD INT, @SEXO NVARCHAR(50))
//AS
//    DECLARE @IDUSU INT
//	DECLARE @IDPERFIL INT
//	SELECT @IDUSU = MAX(IDUSUARIO) FROM USUARIOS
//	SELECT @IDPERFIL = MAX(IDPERFILUSUARIO) FROM PERFILUSUARIO
//	IF @IDUSU IS NULL
//	BEGIN
//	SET @IDUSU = 1
//	PRINT 'YES USU'
//	END
//	ELSE
//	BEGIN 
//	SET @IDUSU = @IDUSU + 1
//	PRINT 'NO USU'
//	END

//	IF @IDPERFIL IS NULL
//	BEGIN
//	SET @IDPERFIL = 1
//	PRINT 'YES PERFIL'
//	END
//	ELSE
//	BEGIN 
//	SET @IDPERFIL = @IDPERFIL + 1
//	PRINT 'NO PERFIL'
//	END
//	INSERT INTO USUARIOS VALUES(@IDUSU, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE)
//	INSERT INTO PERFILUSUARIO VALUES(@IDPERFIL, @EDAD, @SEXO, @ALTURA, @PESO, @IDUSU, NULL, NULL)
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

        public Usuario Login(string email)
        {
            return this.context.Usuarios.FirstOrDefault(z => z.Email == email);
        }

        public Usuario FindUsuario(int idUsuario)
        {
            return this.context.Usuarios.FirstOrDefault(z => z.IdUsuario == idUsuario);
        }

        public Usuario FindUsuarioByEmailAndDNI(string email, string dni)
        {
            return this.context.Usuarios.FirstOrDefault(z => z.Email == email && z.Dni == dni);
        }

        public PerfilUsuario FindPerfilUsuario(int idUsuario)
        {
            return this.context.PerfilUsuarios.FirstOrDefault(z => z.IdUsuario == idUsuario);
        }

        public void FindSolicitud(int idUsuario)
        {
            
        }

        public async Task RegistrarCliente(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo)
        {
            string sql = "SP_REGISTER_OTHERUSER @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, @ALTURA, @PESO, @EDAD, @SEXO";
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paraapellidos = new SqlParameter("@APELLIDOS", apellidos);
            SqlParameter paradni = new SqlParameter("@DNI", dni);
            SqlParameter paraemail = new SqlParameter("@EMAIL", email);
            SqlParameter parapasswordencrypt = new SqlParameter("@PASSWORDENCRYPT", passwordencrypt);
            SqlParameter parasalt = new SqlParameter("@SALT", salt);
            SqlParameter parapassword = new SqlParameter("@PASSWORD", password);
            SqlParameter pararole = new SqlParameter("@ROLE", role);
            SqlParameter paraaltura = new SqlParameter("@ALTURA", altura);
            SqlParameter parapeso = new SqlParameter("@PESO", peso);
            SqlParameter paraedad = new SqlParameter("@EDAD", edad);
            SqlParameter parasexo = new SqlParameter("@SEXO", sexo);
            await this.context.Database.ExecuteSqlRawAsync(sql, paranombre, paraapellidos, paradni, paraemail, parapasswordencrypt, parasalt, parapassword, pararole, paraaltura, parapeso, paraedad, parasexo);
        }

        public async Task RegistrarUsuario(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role)
        {
            string sql = "SP_REGISTER_USER @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE";
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paraapellidos = new SqlParameter("@APELLIDOS", apellidos);
            SqlParameter paradni = new SqlParameter("@DNI", dni);
            SqlParameter paraemail = new SqlParameter("@EMAIL", email);
            SqlParameter parapasswordencrypt = new SqlParameter("@PASSWORDENCRYPT", passwordencrypt);
            SqlParameter parasalt = new SqlParameter("@SALT", salt);
            SqlParameter parapassword = new SqlParameter("@PASSWORD", password);
            SqlParameter pararole = new SqlParameter("@ROLE", role);
            await this.context.Database.ExecuteSqlRawAsync(sql, paranombre, paraapellidos, paradni, paraemail, parapasswordencrypt, parasalt,  parapassword, pararole);
        }
    }
}
