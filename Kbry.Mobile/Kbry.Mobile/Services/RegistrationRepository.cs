using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kbry.Mobile.Models;
using PCLStorage;

namespace Kbry.Mobile.Services
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IFolder _rootFolder;
        private readonly IFolder _folder;
        private readonly IFile _file;

        public RegistrationRepository()
        {
            _rootFolder = FileSystem.Current.LocalStorage;
            _folder = FileHelper.GetOrCreateFolderAsync(_rootFolder, "registration").Result;
            _file = FileHelper.GetOrCreateFileAsync(_folder, "registration").Result;
        }
    }
}