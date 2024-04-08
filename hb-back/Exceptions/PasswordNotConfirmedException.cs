namespace StudentHubBackend.Exceptions
{
    public class PasswordNotConfirmedException : Exception
    {
        public PasswordNotConfirmedException() : base("Пароли не совпадают")
        {

        }
    }
}
