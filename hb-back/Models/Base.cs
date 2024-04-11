using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Base
    {
        [Required]
        public Guid Id { get; set; }
    }
}
