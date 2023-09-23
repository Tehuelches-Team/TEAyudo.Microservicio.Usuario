using System.Security.Cryptography;

namespace Tools
{
    public class Token
    {
        public static string GenerarToken()
        {
            int longitud = 32;
            const string caracteresValidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using var rng = new RNGCryptoServiceProvider();
            var tokenBytes = new byte[longitud];
            rng.GetBytes(tokenBytes);

            var token = new char[longitud];
            for (int i = 0; i < longitud; i++)
            {
                token[i] = caracteresValidos[tokenBytes[i] % caracteresValidos.Length];
            }

            return new string(token);
        }
    }
}