using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.ViewModel
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public int? LatestScore { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
