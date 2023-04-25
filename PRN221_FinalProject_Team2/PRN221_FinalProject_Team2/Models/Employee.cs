using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_FinalProject_Team2.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Accounts = new HashSet<Account>();
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; } = null!;
        public int? DepartmentId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "TitleOfCourtesy is required.")]
        public string? TitleOfCourtesy { get; set; }
        [Required(ErrorMessage = "BirthDate is required.")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "HireDate is required.")]
        public DateTime? HireDate { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
