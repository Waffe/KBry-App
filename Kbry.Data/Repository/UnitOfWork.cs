using System;

namespace Kbry.Data.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly KbryDbContext _context = new KbryDbContext();
        private IClassRepository _classRepository;
        private IStudentRepository _studentRepository;
        private IAttendanceRepository _attendanceRepository;

        public IClassRepository ClassRepository => _classRepository ?? new ClassRepository(_context);

        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(_context);

        public IAttendanceRepository AttendanceRepository => _attendanceRepository ?? new AttendanceRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
