using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Institute> Institutes { get; set; }
    }
}
