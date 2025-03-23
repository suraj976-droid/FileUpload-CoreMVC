using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproach.Models
{
    public class EmpView
    {
        [Key]
        public int empId { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string empName { get; set; }
        [Required(ErrorMessage = "Email cannot be empty")]
        public string empEmail { get; set; }
        [Required(ErrorMessage = "Salary cannot be empty")]
        public double empSalary { get; set; }

        public IFormFile eimg { get; set; }
    }
}
