using Kbry.Mobile.Models;
using System;
using System.Threading.Tasks;
using PCLStorage;
using Newtonsoft.Json;

namespace Kbry.Mobile.Services
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IFolder _folder;
        private readonly IFile _file;

        public AttendanceRepository()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            _folder = rootFolder.CreateFolderAsync("attendance", CreationCollisionOption.OpenIfExists).Result;
            _file = _folder.CreateFileAsync($"attendance{DateTime.Now:yyyyMMdd}.txt", CreationCollisionOption.OpenIfExists).Result;
        }

        public async void AddAttendanceAsync(Attendance attendance)
        {
            if (!await IsDateRegistered(attendance.Date))
                await _file.WriteAllTextAsync(JsonConvert.SerializeObject(attendance));
            else
                throw new Exception("Attendance is already registered today");
        }

        private async Task<bool> IsDateRegistered(DateTime date)
        {
            var registeredAttendence = await GetAttendanceAsync();
            return registeredAttendence.Date.Date == date.Date;
        }

        public async void ClearFileAsync()
        {
            await _folder.DeleteAsync();
        }

        public async Task<Attendance> GetAttendanceAsync()
        {
            var json = await _file.ReadAllTextAsync();
            return JsonConvert.DeserializeObject<Attendance>(json);
        }
    }
}
