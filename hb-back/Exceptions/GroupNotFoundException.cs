namespace StudentHubBackend.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException() : base("Group is not found, try again")
        {

        }
    }
}
