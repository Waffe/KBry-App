using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kbry.Mobile.Models;
using Newtonsoft.Json;
using PCLStorage;

namespace Kbry.Mobile.Services
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IFolder _folder;
        private readonly IFile _file;

        public RegistrationRepository()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            _folder = FileHelper.GetOrCreateFolderAsync(rootFolder, "registration").Result;
            _file = FileHelper.GetOrCreateFileAsync(_folder, "registration").Result;
        }

        public async void ClearFileAsync()
        {
            await _folder.DeleteAsync();
        }

        public async Task<RegistrationCode> GetRegistrationAsync()
        {
            var json = await _file.ReadAllTextAsync();
            return JsonConvert.DeserializeObject<RegistrationCode>(json);
        }

        public async void AddRegistration(RegistrationCode registration)
        {
            await _file.WriteAllTextAsync(JsonConvert.SerializeObject(registration));
        }
    }
}