using FitLife.Data;
using FitLife.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCryptographyBBDD.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;

#region PROCEDURES
//CREATE PROCEDURE SP_REGISTER_CLIENTE
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(MAX), @PASSWORDENCRYPT VARBINARY(MAX), @SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), 
//@ALTURA INT, @PESO INT, @EDAD INT, @SEXO NVARCHAR(50), @IDUSUARIO INT OUT)
//AS
//    DECLARE @IDPERFIL INT
//	SELECT @IDUSUARIO = MAX(IDUSUARIO) FROM USUARIOS
//	SELECT @IDPERFIL = MAX(IDPERFILUSUARIO) FROM PERFILUSUARIO
//	IF @IDUSUARIO IS NULL
//	BEGIN
//	SET @IDUSUARIO = 1
//	PRINT 'YES USU'
//	END
//	ELSE
//	BEGIN 
//	SET @IDUSUARIO = @IDUSUARIO + 1
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
//	INSERT INTO USUARIOS VALUES(@IDUSUARIO, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, 0)
//	INSERT INTO PERFILUSUARIO VALUES(@IDPERFIL, @EDAD, @SEXO, @ALTURA, @PESO, @IDUSUARIO, NULL, NULL)
//GO

//CREATE PROCEDURE SP_REGISTER_USER
//(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORDENCRYPT VARBINARY(MAX), 
//@SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), @IDUSUARIO INT OUT)
//AS
//    SELECT @IDUSUARIO = MAX(IDUSUARIO) FROM USUARIOS
//	IF @IDUSUARIO IS NULL
//	BEGIN
//	SET @IDUSUARIO = 1
//	END
//	ELSE
//	BEGIN
//	SET @IDUSUARIO = @IDUSUARIO + 1
//	END
//	INSERT INTO USUARIOS VALUES(@IDUSUARIO, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, 0)
//GO

//CREATE PROCEDURE SP_REGISTER_SOLICITUD
//(@SALT NVARCHAR(MAX), @CODIGO INT, @IDUSUARIO INT)
//AS
//    DECLARE @IDSOLICITUD INT

//	SELECT @IDSOLICITUD = MAX(IDSOLICITUD) FROM SOLICITUD

//	IF @IDSOLICITUD IS NULL
//	BEGIN
//		SET @IDSOLICITUD = 1
//	END
//	ELSE
//	BEGIN
//		SET @IDSOLICITUD = @IDSOLICITUD + 1
//	END
	
//	DELETE FROM SOLICITUD WHERE IDUSUARIO = @IDUSUARIO
//	INSERT INTO SOLICITUD VALUES(@IDSOLICITUD, @SALT, @CODIGO, @IDUSUARIO)
//GO

//CREATE PROCEDURE SP_DELETE_SOLICITUD_UPDATE_ESTADO_USUARIO
//(@IDUSUARIO INT)
//AS
//    DELETE FROM SOLICITUD WHERE IDUSUARIO = @IDUSUARIO
//	UPDATE USUARIOS SET ESTADO = 1 WHERE IDUSUARIO = @IDUSUARIO
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
        public async Task<Usuario> LoginAsync(string email)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.Email == email);
        }

        public async Task<Usuario> FindUsuarioAsync(int idUsuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
        }

        public async Task<Usuario> FindUsuarioByEmailAsync(string email)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.Email == email);
        }

        public async Task<Usuario> FindUsuarioByEmailOrDNIAsync(string email, string dni)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.Email == email || z.Dni == dni);
        }

        public async Task<UsuarioPerfil> FindClienteAsync(int idusuario)
        {
            var consulta = from datos in this.context.Usuarios
                           join datos2 in this.context.PerfilUsuarios
                           on datos.IdUsuario equals datos2.IdUsuario
                           where datos2.IdUsuario == idusuario
                           select new UsuarioPerfil
                           {
                               IdUsuario = datos.IdUsuario,
                               Nombre = datos.Nombre,
                               Apellidos = datos.Apellidos,
                               Dni = datos.Dni,
                               Email = datos.Email,
                               Password = datos.Password,
                               Role = datos.Role,
                               Altura = datos2.Altura,
                               Edad = datos2.Edad,
                               Peso = datos2.Peso,
                               Sexo = datos2.Sexo
                           };
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<List<UsuarioPerfil>> GetClientesAsync()
        {
            var consulta = from datos in this.context.Usuarios
                           join datos2 in this.context.PerfilUsuarios
                           on datos.IdUsuario equals datos2.IdUsuario
                           select new UsuarioPerfil
                           {
                               IdUsuario = datos.IdUsuario,
                               Nombre = datos.Nombre,
                               Apellidos = datos.Apellidos,
                               Dni = datos.Dni,
                               Email = datos.Email,
                               Password = datos.Password,
                               Role = datos.Role,
                               Altura = datos2.Altura,
                               Edad = datos2.Edad,
                               Peso = datos2.Peso,
                               Sexo = datos2.Sexo,
                               IdEntrenador = datos2.IdEntrenador,
                               IdDietista = datos2.IdDietista
                           };
            return await consulta.ToListAsync();
        }

        public async Task<List<UsuarioIdEmail>> FindUsuariosIdAsync()
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Role == "cliente"
                           select new UsuarioIdEmail
                           {
                               IdUsuario = datos.IdUsuario,
                               Email = datos.Email,
                               Nombre = datos.Nombre + datos.Apellidos
                           };
            return await consulta.ToListAsync();
        }

        //public async Task<PerfilUsuario> FindPerfilUsuario(int idUsuario)
        //{
        //    return await this.context.PerfilUsuarios.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
        //}

        public async Task<Solicitud> FindSolicitudAsync(int idUsuario)
        {
            return await this.context.Solicitud.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
        }

        public async Task RegistrarSolicitudAsync(string salt, int codigo, int idusuario)
        {
            string sql = "SP_REGISTER_SOLICITUD @SALT, @CODIGO, @IDUSUARIO";
            SqlParameter parasalt= new SqlParameter("@SALT", salt);
            SqlParameter paracodigo = new SqlParameter("@CODIGO", codigo);
            SqlParameter paraidusuario = new SqlParameter("@IDUSUARIO", idusuario);
            await this.context.Database.ExecuteSqlRawAsync(sql, parasalt, paracodigo, paraidusuario);
        }

        public async Task DeleteSolicitudAsync(int idusuario)
        {
            Solicitud solicitud = await this.FindSolicitudAsync(idusuario);
            this.context.Solicitud.Remove(solicitud);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> RegistrarClienteAsync(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo)
        {
            string sql = "SP_REGISTER_CLIENTE @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, @ALTURA, @PESO, @EDAD, @SEXO, @IDUSUARIO OUT";
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
            await this.context.Database.ExecuteSqlRawAsync(sql, paraidusuario, paranombre, paraapellidos, paradni, paraemail, parapasswordencrypt, parasalt, parapassword, pararole, paraaltura, parapeso, paraedad, parasexo, paraidusuario);
            return int.Parse(paraidusuario.Value.ToString());
        }

        public async Task<int> RegistrarUsuarioAsync(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role)
        {
            string sql = "SP_REGISTER_USER @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, @IDUSUARIO OUT";
            SqlParameter paraidusuario = new SqlParameter("@IDUSUARIO", -1);
            paraidusuario.Direction = ParameterDirection.Output;
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paraapellidos = new SqlParameter("@APELLIDOS", apellidos);
            SqlParameter paradni = new SqlParameter("@DNI", dni);
            SqlParameter paraemail = new SqlParameter("@EMAIL", email);
            SqlParameter parapasswordencrypt = new SqlParameter("@PASSWORDENCRYPT", passwordencrypt);
            SqlParameter parasalt = new SqlParameter("@SALT", salt);
            SqlParameter parapassword = new SqlParameter("@PASSWORD", password);
            SqlParameter pararole = new SqlParameter("@ROLE", role);
            await this.context.Database.ExecuteSqlRawAsync(sql, paranombre, paraapellidos, paradni, paraemail, parapasswordencrypt, parasalt,  parapassword, pararole, paraidusuario);
            return int.Parse(paraidusuario.Value.ToString());
        }

        public async Task UpdatePasswordAsync(int idusuario, string password, string salt, byte[] passwordencrypt)
        {
            Usuario usuario = await this.FindUsuarioAsync(idusuario);
            usuario.Password = password;
            usuario.Salt = salt;
            usuario.PasswordEncrypt = passwordencrypt;
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEstadoUsuarioAsync(int idusuario)
        {
            Usuario usuario = await this.FindUsuarioAsync(idusuario);
            usuario.Estado = true;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteSolicitudUpdateEstadoUsuarioAsync(int idusuario)
        {
            string sql = "SP_DELETE_SOLICITUD_UPDATE_ESTADO_USUARIO @IDUSUARIO";
            SqlParameter paraidusuario = new SqlParameter("@IDUSUARIO", idusuario);
            await this.context.Database.ExecuteSqlRawAsync(sql, paraidusuario);
        }
        #endregion

        #region ENTRENADOR
        public async Task<List<UsuarioPerfil>> FindPerfilUsuariosByIdEntrenadorAsync(int idusuario)
        {
            var consulta = from datos in this.context.Usuarios
                           join datos2 in this.context.PerfilUsuarios
                           on datos.IdUsuario equals datos2.IdUsuario
                           where datos2.IdEntrenador == idusuario
                           select new UsuarioPerfil
                           {
                               IdUsuario = datos.IdUsuario,
                               Nombre =  datos.Nombre,
                               Apellidos = datos.Apellidos,
                               Dni = datos.Dni,
                               Email = datos.Email,
                               Password = datos.Password,
                               Role = datos.Role,
                               Altura = datos2.Altura,
                               Edad = datos2.Edad,
                               Peso = datos2.Peso,
                               Sexo = datos2.Sexo
                           };
            return await consulta.ToListAsync();
        }
        #endregion

        #region NUTRICIONISTA
        public async Task<List<UsuarioPerfil>> FindPerfilUsuariosByIdNutricionistaAsync(int idusuario)
        {
            var consulta = from datos in this.context.Usuarios
                           join datos2 in this.context.PerfilUsuarios
                           on datos.IdUsuario equals datos2.IdUsuario
                           where datos2.IdDietista == idusuario
                           select new UsuarioPerfil
                           {
                               IdUsuario = datos.IdUsuario,
                               Nombre = datos.Nombre,
                               Apellidos = datos.Apellidos,
                               Dni = datos.Dni,
                               Email = datos.Email,
                               Password = datos.Password,
                               Role = datos.Role,
                               Altura = datos2.Altura,
                               Edad = datos2.Edad,
                               Peso = datos2.Peso,
                               Sexo = datos2.Sexo
                           };
            return consulta.ToList();
        }
        #endregion
    }
}
