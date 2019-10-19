namespace SportsBetting.Handlers.Commands.Accounts.CommandHandlers
{
    using System.Security.Cryptography;
    using System.Text;

    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class EncryptPasswordCommandHandler : ICommandHandler<EncryptPasswordCommand, string>
    {
        public string Handle(EncryptPasswordCommand command)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(command.Password);
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