namespace Kbry.Data.Repository
{
    public interface IUnitOfWork
    {
        IAttendanceRepository AttendanceRepository { get; }
        IClassRepository ClassRepository { get; }
        IStudentRepository StudentRepository { get; }

        void Dispose();
        void Save();
    }
}