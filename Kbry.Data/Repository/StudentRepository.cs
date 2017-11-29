using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public class StudentRepository : GenericRepository<Student, KbryDbContext>, IStudentRepository
    {
        public StudentRepository(KbryDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Student> GetStudentsByClass(int classId)
        {
            return Context.Set<Student>().ToList().Where(s => s.Class.Id == classId);
        }
    }
}
