using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        public ICollection<State> States { get; set; }
         
        public ICollection<Role> Roles { get; set; }

    }
}
