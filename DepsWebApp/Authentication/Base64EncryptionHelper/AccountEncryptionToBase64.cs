using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepsWebApp.Authentication.Base64EncryptionHelper
{
#pragma warning disable CS1591
    public static class AccountEncryptionToBase64
    {
        public static string Encode(string accountCred) => "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(accountCred));
       //Added for testing
        public static string Decode(string accountCred) => Encoding.UTF8.GetString(Convert.FromBase64String(accountCred.ToString().Split("Basic ")[1]));
    }
#pragma warning restore CS1591
}
