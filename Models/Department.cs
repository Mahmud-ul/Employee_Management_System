using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Department name is required!!!")]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("DepartmentManager")]
        public int? ManagerID { get; set; }

        [Required(ErrorMessage = "Budget is required!!!")]
        public int Budget { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Employee? DepartmentManager { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
