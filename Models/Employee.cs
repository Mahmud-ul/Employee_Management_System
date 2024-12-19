using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is Required!!!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required!!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is Required!!!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is Required!!!")]
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Position is Required!!!")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Joining Date is Required!!!")]
        [DataType (DataType.DateTime)]
        public DateTime JoiningDate { get; set; }

        public bool Status { get; set; } = true;
        public bool IsDeleted { get; set; } = false;



        public virtual Department? Department { get; set; }
        public virtual ICollection<PerformanceReview>? PerformanceReviews { get; set; }

    }
}
