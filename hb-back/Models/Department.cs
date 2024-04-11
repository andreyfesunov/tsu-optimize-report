using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Department : Base
    {
        public ICollection<Institute> Institutes { get; set; }
        
        public string Name { get; set; }
    }
}
