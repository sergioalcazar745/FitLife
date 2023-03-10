using FitLife.Data;
using FitLife.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCryptographyBBDD.Helpers;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;

#region PROCEDURES
//CREATE PROCEDURE SP_REGISTER_USER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORDENCRYPT VARBINARY(MAX), 
//@SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), @IDUSUARIO INT OUT)
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
//	INSERT INTO USUARIOS VALUES(@ID, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, 0)
//GO

//CREATE PROCEDURE SP_REGISTER_USER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORDENCRYPT VARBINARY(MAX), 
//@SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), @IDUSUARIO INT OUT)
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
//	INSERT INTO USUARIOS VALUES(@ID, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, 0)
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

        #region REGISTRO
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
            return this.context.Usuarios.FirstOrDefault(z => z.Email == email || z.Dni == dni);
        }

        public PerfilUsuario FindPerfilUsuario(int idUsuario)
        {
            return this.context.PerfilUsuarios.FirstOrDefault(z => z.IdUsuario == idUsuario);
        }

        public Solicitud FindSolicitud(int idUsuario)
        {
            return this.context.Solicitud.FirstOrDefault(z => z.IdUsuario == idUsuario);
        }

        public async Task RegistrarSolicitud(string salt, int idusuario)
        {
            Solicitud solicitud = new Solicitud();
            solicitud.Salt = salt;
            solicitud.IdUsuario = idusuario;
            this.context.Solicitud.Add(solicitud);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteSolicitud(int idusuario)
        {
            Solicitud solicitud = this.FindSolicitud(idusuario);
            this.context.Solicitud.Remove(solicitud);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> RegistrarCliente(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo)
        {
            string sql = "SP_REGISTER_CLIENTE @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, @ALTURA, @PESO, @EDAD, @SEXO";
            SqlParameter paraidusuario = new SqlParameter("@IDUSUARIO", -1);
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
            return int.Parse(paraidusuario.Value.ToString());
        }

        public async Task<int> RegistrarUsuario(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role)
        {
            string sql = "SP_REGISTER_USER @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE";
            SqlParameter paraidusuario = new SqlParameter("@IDUSUARIO", -1);
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paraapellidos = new SqlParameter("@APELLIDOS", apellidos);
            SqlParameter paradni = new SqlParameter("@DNI", dni);
            SqlParameter paraemail = new SqlParameter("@EMAIL", email);
            SqlParameter parapasswordencrypt = new SqlParameter("@PASSWORDENCRYPT", passwordencrypt);
            SqlParameter parasalt = new SqlParameter("@SALT", salt);
            SqlParameter parapassword = new SqlParameter("@PASSWORD", password);
            SqlParameter pararole = new SqlParameter("@ROLE", role);
            await this.context.Database.ExecuteSqlRawAsync(sql, paranombre, paraapellidos, paradni, paraemail, parapasswordencrypt, parasalt,  parapassword, pararole);
            return int.Parse(paraidusuario.Value.ToString());
        }

        public async Task UpdateEstadoUsuario(int idusuario)
        {
            Usuario usuario = this.FindUsuario(idusuario);
            usuario.Estado = true;
            await this.context.SaveChangesAsync();
        }
        #endregion
    }
}
