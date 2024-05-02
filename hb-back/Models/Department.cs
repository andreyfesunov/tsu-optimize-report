using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Department : Base
    {
        public ICollection<State> States { get; set; }
        public Guid InstituteId { get; set; }
        public Institute Institute { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
