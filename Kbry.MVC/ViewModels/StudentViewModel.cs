using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kbry.Data.Model;

namespace Kbry.MVC.ViewModels
{
    public class StudentViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = "Registration code must contain 5-20 characters")]
        public string RegistrationCode { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        [HiddenInput(DisplayValue = false)]
        public string FullName => $"{LastName}, {FirstName}";

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DisplayName("Class")]
        public string SchoolClassName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? SchoolClassId { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}