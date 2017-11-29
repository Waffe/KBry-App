using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public class ClassRepository : GenericRepository<Class, KbryDbContext>
    {
        public ClassRepository(KbryDbContext context)
            : base(context)
        {
        }
    }
}
