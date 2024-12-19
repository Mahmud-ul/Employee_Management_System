using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.ViewModel
{
    public class DepartmentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public int Budget { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
