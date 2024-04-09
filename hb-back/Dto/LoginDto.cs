using System.ComponentModel.DataAnnotations;

namespace BackendBase.Dto
{
    public class LoginDto
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
