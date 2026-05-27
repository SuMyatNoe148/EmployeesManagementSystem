using EmployeeManagement.Models;
using System.ComponentModel;

namespace EmployeeManagement.ViewModels
{
    public class SystemCodeDetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("System Code")]
        public int SystemCodeId { get; set; }
        public SystemCode? SystemCode { get; set; }

        [DisplayName("Code")]
        public string? Code { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Order No")]
        public int? OrderNo { get; set; }

        [DisplayName("Created By")]
        public string CreatedById { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn {  get; set; }

        public List<SystemCodeDetail> SystemCodeDetails { get; set; }
    }
}
