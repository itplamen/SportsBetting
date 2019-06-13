namespace SportsBetting.Common.Account
{
    public static class AccountConstants
    {
        public const int USERNAME_MAX_LENGTH = 15;

        public const int USERNAME_MIN_LENGTH = 5;

        public const string USERNAME_REG_EX = @"^[a-zA-Z]([/._]?[a-zA-Z0-9]+)+$";

        public const int PASSWORD_MAX_LENGTH = 100;

        public const int PASSWORD_MIN_LENGTH = 6;
    }
}