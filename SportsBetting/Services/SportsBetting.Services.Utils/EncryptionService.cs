namespace SportsBetting.Services.Utils
{
    using System.Security.Cryptography;
    using System.Text;

    using SportsBetting.Services.Utils.Contracts;

    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string text)
        {
            using (SHA512 sha512 = SHA512Managed.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha512.ComputeHash(bytes);

                return GetStringFromHash(hash);
            }
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}