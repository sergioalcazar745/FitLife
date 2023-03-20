using System.Security.Cryptography;
using System.Text;

namespace Fitlife.Helpers
{
    public class CryptoEngine
    {
        private IConfiguration configuration;
        public CryptoEngine(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Encrypt(string source)
        {
            string key = this.configuration.GetValue<string>("Key");
            var byteHash = MD5.HashData(Encoding.UTF8.GetBytes(key));
            var tripleDes = TripleDES.Create();
            tripleDes.Key = byteHash;
            tripleDes.Mode = CipherMode.ECB;


            var byteBuff = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(tripleDes.CreateEncryptor()
                .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }

        public string Decrypt(string encodedText)
        {
            string key = this.configuration.GetValue<string>("Key");
            var byteHash = MD5.HashData(Encoding.UTF8.GetBytes(key));
            var tripleDes = TripleDES.Create();
            tripleDes.Key = byteHash;
            tripleDes.Mode = CipherMode.ECB;

            var byteBuff = Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(
                tripleDes
                    .CreateDecryptor()
                    .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }
    }
}