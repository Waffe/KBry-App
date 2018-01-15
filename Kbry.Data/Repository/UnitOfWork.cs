using System;
using System.Data.Entity.Core.Objects;

namespace Kbry.Data.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly KbryDbContext _context;

        public UnitOfWork()
        {
            _context = new KbryDbContext();
            StudentRepository = new StudentRepository(_context);
            AttendanceRepository = new AttendanceRepository(_context);
            ClassRepository = new ClassRepository(_context);
        }

        public IStudentRepository StudentRepository { get; }

        public IAttendanceRepository AttendanceRepository { get; }

        public IClassRepository ClassRepository { get; }

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
