using System.Security.Cryptography;
using System.Text;

namespace JWTProject
{
    public static class EncryptionHelper
    {
        public static string EncryptPassword(string Password)
        {
            string encrptedPasword = "";

            try
            {
                
                  
                    StringBuilder Sb = new StringBuilder();

                    using (SHA256 hash = SHA256Managed.Create())
                    {
                        Encoding enc = Encoding.UTF8;
                        Byte[] result = hash.ComputeHash(enc.GetBytes(Password));

                        foreach (Byte b in result)
                            Sb.Append(b.ToString("x2"));
                    }
                encrptedPasword= Sb.ToString();
                    return Sb.ToString();
                

            }
            catch
            {
            }
            return encrptedPasword;
        }

    }
   


}
