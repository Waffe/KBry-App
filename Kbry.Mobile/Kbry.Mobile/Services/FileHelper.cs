using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace Kbry.Mobile.Services
{
    public static class FileHelper
    {
        public static async Task<IFolder> GetOrCreateFolderAsync(IFolder rootFolder, string folderName)
        {
            return await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
        }

        public static async Task<IFile> GetOrCreateFileAsync(IFolder folder, string fileName)
        {
            return await folder.CreateFileAsync($"{fileName}{DateTime.Now:yyyyMMdd}.txt", CreationCollisionOption.OpenIfExists);
        }
    }
}
