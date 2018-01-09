using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public class ClassRepository : GenericRepository<Class, KbryDbContext>, IClassRepository
    {
        public ClassRepository(KbryDbContext context)
            : base(context)
        {
        }
    }
}
