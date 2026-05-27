using EmployeeManagement.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeViewModel:UserActivity
    {
        public int Id { get; set; }

        [DisplayName("Employee No")]
        public string EmpNo { get; set; }

        [DisplayName("First Name"),Required]
        public string? FirstName { get; set; }

        [DisplayName("Middle Name"), Required]
        public string? MiddleName { get; set; }

        [DisplayName("Last Name"), Required]
        public string? LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        [DisplayName("Phone Number"), Required]
        public int? PhoneNumber { get; set; }

        [DisplayName("Email Address"), Required]
        public string? EmailAddress { get; set; }

        [DisplayName("Country Name"), Required]
        public int? CountryId { get; set; }

        [DisplayName("Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }

        [DisplayName("Department Name"), Required]
        public int? DepartmentId { get; set; }

        [DisplayName("Designation Name"), Required]
        public int? DesignationId { get; set; }

        [DisplayName("Gender Name")]
        public int? GenderId { get; set; }

        [DisplayName("Employee Photo ")]
        public string? Photo { get; set; }

        [DisplayName("Employment Date"), Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EmploymentDate { get; set; }

        [DisplayName("Status")]
        public int? StatusId { get; set; }

        [DisplayName("Inactive Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? InactiveDate { get; set; }
        public int? CauseofInactivityId { get; set; }

        [DisplayName("Termination Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? TerminationDate { get; set; }
        public int? ReasonforterminationId { get; set; }

        [DisplayName("Bank Name"), Required]
        public int BankId { get; set; }

        [DisplayName("Bank Account No"), Required]
        public string BankAccountNo { get; set; }

        [DisplayName("International Bank Account Number"), Required]
        public string IBAN { get; set; }

        [DisplayName("SWIFT Code"), Required]
        public string SWIFTCode { get; set; }

        [DisplayName("N.S.S.F Number"), Required]
        public string NSSFNO { get; set; }

        [DisplayName("N.H.I.F Number"), Required]
        public string NHIF { get; set; }

        [DisplayName("Company Email Address"), Required]
        public string CompanyEmail { get; set; }

        [DisplayName("KAR PIN")]
        public string KRAPIN { get; set; }

        [DisplayName("Passport No")]
        public string? PassportNo { get; set; }

        [DisplayName("Employment Terms"), Required]
        public int EmploymentTermsId { get; set; }

        [DisplayName("Allocated Leave Days")]
        public Decimal? AllocatedLeaveDays { get; set; }

        [DisplayName("Leave Balance")]
        public Decimal? LeaveOutstandingBalance { get; set; }

        [DisplayName("Pays Taxes")]
        public bool? PaysTax { get; set; }

        [DisplayName("Disability Type")]
        public int? DisabilityId { get; set; }

        [DisplayName("Disability Certificate")]
        public string? DisabilityCertificate { get; set; }

        public Employee? Employee { get; set; }
        public List<Employee>? Employees { get; set; }


    }
}
