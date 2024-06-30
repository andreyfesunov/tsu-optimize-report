namespace StudentHubBackend.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base("User with this nickname already exists")
        {

        }
    }
}
