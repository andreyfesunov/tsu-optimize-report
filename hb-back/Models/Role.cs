namespace BackendBase.Models
{
    public class Role : Base
    {
        public ICollection<RoleUser> RolesUsers { get; set; }
        public string Name { get; set; }
    }
}
