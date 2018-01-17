using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Class")]
        public string SchoolClassName { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}