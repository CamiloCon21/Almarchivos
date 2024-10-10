using System.Security.Cryptography;
using System.Text;

namespace AlmarchivosBackend.Controllers
{
    public class EncriptadorMD5
    {
        // Método para encriptar un string en MD5
        public static string EncriptarMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

              
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); 
                }

                return sb.ToString();
            }
        }
    }
}
