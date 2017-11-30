using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbry.Data.Model
{
    public class Class
    {
        public Class() => Students = new Collection<Student>();

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
