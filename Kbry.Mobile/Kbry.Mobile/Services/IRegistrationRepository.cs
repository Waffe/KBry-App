using Kbry.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kbry.Mobile.Services
{
    public interface IRegistrationRepository
    {
        void ClearFileAsync();
        Task<RegistrationCode> GetRegistrationAsync();
        void AddRegistration(RegistrationCode registration);
    }
}
