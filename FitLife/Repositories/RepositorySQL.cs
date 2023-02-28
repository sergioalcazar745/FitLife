using FitLife.Models;

#region PROCEDURES
//CREATE PROCEDURE SP_REGISTER_USER
//(@NAME NVARCHAR(50), @LASTNAME NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORD NVARCHAR(50))
//AS
//    DECLARE @ID INT
//	SELECT @ID = MAX(idUser) FROM USERS
//	INSERT INTO USERS VALUES(@ID, @NAME, @LASTNAME, @EMAIL, @PASSWORD)
//GO
#endregion

namespace FitLife.Repositories
{
    public class RepositorySQL : IRepository
    {
        private IRepository repo;
        public RepositorySQL(IRepository repo)
        {
            this.repo = repo;
        }

        public User Login()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void RegisterClient(string name, string lastname, string email, string password, int age, int height, string sexo)
        {
            
        }

        public void RegisterUser(string name, string lastname, string email, string password)
        {
            string sql = "SP_REGISTER_USER";

        }
    }
}
