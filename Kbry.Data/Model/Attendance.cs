using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbry.Data.Model
{
    public class Attendance
    {

        public int Id { get; set; }


        public double Longitude { get; set; }
        public double Latitude { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Student Student { get; set; }
    }
}
