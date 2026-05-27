using System.ComponentModel;

namespace EmployeeManagement.Models
{
    public class LeaveType:UserActivity
    {
        public int Id { get; set; }

        [DisplayName("Leave Type Code")]
        public string? Code { get; set; }

        [DisplayName("Leave Type Name")]
        public string? Name { get; set; }

        [DisplayName("Leave Type Day")]
        public Decimal Days { get; set; }
    }
}
