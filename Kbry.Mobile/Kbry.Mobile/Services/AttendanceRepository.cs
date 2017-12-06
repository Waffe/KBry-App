using Kbry.Mobile.Models;
using System;
using System.Threading.Tasks;
using PCLStorage;
using Newtonsoft.Json;

namespace Kbry.Mobile.Services
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IFolder _rootFolder;
        private readonly IFolder _folder;
        private readonly IFile _file;

        public AttendanceRepository()
        {
            _rootFolder = FileSystem.Current.LocalStorage;
            _folder = FileHelper.GetOrCreateFolderAsync(_rootFolder, "attendance").Result;
            _file = FileHelper.GetOrCreateFileAsync(_folder, "attendance").Result;
        }

        public async void AddAttendanceAsync(Attendance item)
        {
            if (!await IsDateRegistered(item.Date))
                await _file.WriteAllTextAsync(JsonConvert.SerializeObject(item));
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
