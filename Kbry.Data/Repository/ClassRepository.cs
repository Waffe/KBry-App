using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public class ClassRepository : GenericRepository<SchoolClass, KbryDbContext>, IClassRepository
    {
        public ClassRepository(KbryDbContext context)
            : base(context)
        {
        }
    }
}
