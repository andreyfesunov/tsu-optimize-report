using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
