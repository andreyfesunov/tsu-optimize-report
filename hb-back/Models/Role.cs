namespace BackendBase.Models
{
    public class Role : Base
    {
        public ICollection<User> Users { get; set; }
        
        public string Name { get; set; }
    }
}
