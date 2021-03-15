using DepsWebApp.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Authentication.Base64EncryptionHelper;
using DepsWebApp.Models;

namespace DepsWebApp.Services
{
#pragma warning disable CS1591
    public class AccountCoordinatorService : IAccountCoordinatorService
    {
        public Task<bool> GetUserAccount(string encryptedString)
        {
            if (encryptedString == null)
                throw new ArgumentNullException("Encrypted string with account credentials is null!");
            if (String.IsNullOrEmpty(encryptedString.Trim()))
                throw new NotSupportedException("This type of encrypted string not supported!");
            var decryptedString = AccountEncryptionToBase64.Decode(encryptedString);
            var splittedString = decryptedString.Split(':');
            return Task.FromResult(_accounts.Values.Any(account =>
            account.Login == splittedString[0] && account.Password == splittedString[1]));
        }
        public async Task<string> RegisterAsync(string login, string password)
        {
            var release = await _semaphore.WaitAsync(1000);
            try
            {
                var id = _accounts.Count + 1;
                var encryptedKey = (AccountEncryptionToBase64.Encode($"{login}:{password}"));
                if (_accounts.Any(account => account.Value.Login == login))
                    throw new ArgumentException("Account with this login already exists");
                _accounts.TryAdd(encryptedKey, new Account(id, login, password));
                return encryptedKey;
            }
            finally
            {
                if (release) _semaphore.Release();
            }
        }
        //Used encrypted string as key for future
        private readonly ConcurrentDictionary<string,Account> _accounts = new ConcurrentDictionary<string,Account>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    }
#pragma warning restore CS1591
}
