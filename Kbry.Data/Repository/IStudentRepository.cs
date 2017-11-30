using System.Collections.Generic;
using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IEnumerable<Student> GetStudentsByClass(int classId);
    }
}