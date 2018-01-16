using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbry.Data.Model
{
    public class ApiKey
    {
        public ApiKey()
        {
            Key = Guid.NewGuid();
        }
        [Key]
        public int Id { get; set; }
        public Guid Key { get; set; }
    }
}
