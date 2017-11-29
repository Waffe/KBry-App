using System.Collections.ObjectModel;

namespace Kbry.Data.Model
{
    public class Student
    {
        public Student() => Attendances = new Collection<Attendance>();

        public int Id { get; set; }
        public string RegistrationCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual Class Class { get; set; }
        public virtual Collection<Attendance> Attendances { get; set; }
    }
}
