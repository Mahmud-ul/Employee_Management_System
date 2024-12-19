using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models
{
    public class PerformanceReview
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; } 

        [Required(ErrorMessage = "Review Date is required!!!")]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        [Required(ErrorMessage = "Review Score is required!!!")]
        [Range(1, 10, ErrorMessage = "Review Score must be between 1 and 10.")]
        public int ReviewScore { get; set; }

        [MaxLength(1000, ErrorMessage = "Review Notes cannot exceed 1000 characters.")]
        public string ReviewNotes { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
