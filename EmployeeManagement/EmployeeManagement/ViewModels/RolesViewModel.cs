using System.ComponentModel;

namespace EmployeeManagement.ViewModels
{
    public class RolesViewModel
    {
        public string? Id { get; set; }

        [DisplayName("Role Name")]
        public string? RoleName { get; set; }
    }
}
