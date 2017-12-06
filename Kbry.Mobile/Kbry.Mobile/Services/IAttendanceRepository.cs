using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kbry.Mobile.Models;

namespace Kbry.Mobile.Services
{
    public interface IAttendanceRepository
    {
        void AddAttendanceAsync(Attendance attendance);
        void ClearFileAsync();
        Task<Attendance> GetAttendanceAsync();
    }
}
