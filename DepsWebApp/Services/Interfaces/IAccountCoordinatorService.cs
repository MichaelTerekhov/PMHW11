using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Services.Interfaces
{

#pragma warning disable CS1591 
    public interface IAccountCoordinatorService
    {
        Task<string> RegisterAsync(string login, string password);
        Task<bool> GetUserAccount(string encryptedString);
    }
#pragma warning restore CS1591
}
