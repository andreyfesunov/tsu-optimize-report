namespace BackendBase.Models
{
    public class RoleUser : Base
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
