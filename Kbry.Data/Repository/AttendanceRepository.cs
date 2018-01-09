using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public class AttendanceRepository : GenericRepository<Attendance, KbryDbContext>, IAttendanceRepository
    {
        public AttendanceRepository(KbryDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Attendance> GetAttendancesByDate(DateTime date)
        {
            return Context.Attendances.Include(a => a.Student).ToList().Where(a => a.Date.Date == date.Date);
        }

        public IEnumerable<Attendance> GetAttendancesBetweenDates(DateTime fromDate, DateTime toDate)
        {
            return Context.Attendances.Include(a => a.Student).ToList().Where(a => a.Date.Date >= fromDate.Date && a.Date.Date <= toDate.Date);
        }

    }
}
