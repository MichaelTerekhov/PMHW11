using DepsWebApp.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Authentication.Base64EncryptionHelper;
using DepsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp.Services
{
#pragma warning disable CS1591
    public class AccountCoordinatorService : IAccountCoordinatorService
    {
        private readonly DatabaseContext _context;
        
        public AccountCoordinatorService(DatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<bool> GetUserAccount(string encryptedString)
        {
            if (encryptedString == null)
                throw new ArgumentNullException("Encrypted string with account credentials is null!");
            if (String.IsNullOrEmpty(encryptedString.Trim()))
                throw new NotSupportedException("This type of encrypted string not supported!");

            var decryptedString = AccountEncryptionToBase64.Decode(encryptedString);
            var splittedString = decryptedString.Split(':');

            var result = await _context.Accounts.FirstOrDefaultAsync(acc =>
            acc.Login == splittedString[0] && acc.Password == splittedString[1]);

            return result == null 
                ? false 
                : true;
        }
        public async Task<string> RegisterAsync(string login, string password)
        {
            if (login == null || password == null)
                return string.Empty;

            try
            {
                _context.Accounts.Add(new Account(login,password));
                await _context.SaveChangesAsync();

                return AccountEncryptionToBase64.Encode($"{login}:{password}");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
#pragma warning restore CS1591
}
