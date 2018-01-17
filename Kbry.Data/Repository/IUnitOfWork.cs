using System;

namespace Kbry.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAttendanceRepository AttendanceRepository { get; }
        IClassRepository ClassRepository { get; }
        IStudentRepository StudentRepository { get; }
        
        void Save();
    }
}