

using System.ComponentModel.DataAnnotations;

namespace WebAPiCaching.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Department { get; set; }
    }
}
