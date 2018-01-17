using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Kbry.Data.Model
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Registration code must contain 5-20 characters")]
        public string RegistrationCode { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public SchoolClass SchoolClass { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}
