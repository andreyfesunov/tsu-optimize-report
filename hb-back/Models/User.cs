namespace BackendBase.Models
{
    public class User : Base
    {
        public ICollection<StateUser> StatesUsers { get; set; }
        public ICollection<RoleUser> RolesUsers { get; set; }
        public string? Firstname { get; set; } = null;
        public string? Lastname { get; set; } = null;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
