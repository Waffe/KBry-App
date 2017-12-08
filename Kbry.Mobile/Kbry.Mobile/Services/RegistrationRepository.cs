using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kbry.Mobile.Models;
using Newtonsoft.Json;
using PCLStorage;

[assembly: Xamarin.Forms.Dependency(typeof(Kbry.Mobile.Services.RegistrationRepository))]
namespace Kbry.Mobile.Services
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IFolder _folder;
        private readonly IFile _file;

        public RegistrationRepository()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            _folder = rootFolder.CreateFolderAsync("registration", CreationCollisionOption.OpenIfExists).Result;
            _file = _folder.CreateFileAsync("registration.txt", CreationCollisionOption.OpenIfExists).Result;
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