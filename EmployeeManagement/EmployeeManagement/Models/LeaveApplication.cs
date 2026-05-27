using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class LeaveApplication : UserActivity
    {
        public int Id { get; set; }

        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Display(Name = "Leave Days")]
        [Required(ErrorMessage = "Leave Days is required")]
        public double NoOfDays { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Leave Duration")]
        [Required(ErrorMessage = "Leave Duration is required")]
        public int DurationId { get; set; }
        public SystemCodeDetail? Duration { get; set; }

        [Display(Name = "Leave Type")]
        [Required(ErrorMessage = "Leave Type is required")]
        public int LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public string? Attachment {  get; set; }

        [Display(Name = "Notes")]
        public string? Description { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail? Status { get; set; }
        public string? ApprovedById { get; set; }
        public DateTime ApprovedOn { get; set; }

        [Display(Name = "Approval Notes")]
        public string? ApprovalNotes { get; set; }

    }
}
