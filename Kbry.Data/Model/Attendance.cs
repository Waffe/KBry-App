using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbry.Data.Model
{
    class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Student Student { get; set; }
    }
}
