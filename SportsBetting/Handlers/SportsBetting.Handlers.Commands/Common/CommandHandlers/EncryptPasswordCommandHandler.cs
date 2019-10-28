namespace SportsBetting.Handlers.Commands.Common.CommandHandlers
{
    using System.Security.Cryptography;
    using System.Text;

    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class EncryptPasswordCommandHandler : ICommandHandler<PasswordCommand, string>
    {
        public string Handle(PasswordCommand command)
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