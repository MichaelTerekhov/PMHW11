using DepsTest.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsTest.Services
{
    public static class AuthControllerTesterService
    {
        public static async Task RegistrationChecker(HttpClient client, AccountDto account)
        {
            try
            {
                var newUri = client.BaseAddress + "Auth/register";
                var response = await client.PostAsync(newUri, account, new JsonMediaTypeFormatter());
                if ((int)response.StatusCode == 400)
                {
                    Console.WriteLine("It seems that you tried register with bad credentials(Login and Password length must be >= 6 characters!)\n" +
                        $"This account credentials tried to register on the server\n{account}");
                         return;
                }
                var content = await response.Content.ReadAsStringAsync();
                
                var errorOccured = new OccuredError()
                { 
                Code =999,
                ErrorMsg ="Plug"
                };
                try
                {
                    errorOccured = JsonSerializer.Deserialize<OccuredError>(content);
                }
                catch (JsonException)
                {
                    Console.WriteLine("Everything is ok.\n" +
                        "Registration completed!\n" +
                        $"Account credentials: {account.Login}\t{account.Password}\n" +
                        $"Expected encrypted account: {"Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{account.Login}:{account.Password}"))}\n" +
                        $"Actual encrypted account: {content}\n" +
                        $"Are they EQUAL?\t[{content.Equals("Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{account.Login}:{account.Password}")))}]\n");
                    return;
                }
                if (errorOccured.Code == 228)
                {
                    Console.WriteLine("TEST PASSED(THIS METHOD ON API SIDE NOT IMPLEMENTED)\n" +
                        $"This account credentials tried to register on the server\n{account}");
                    return;
                }
                else if (errorOccured.Code == 999)
                {
                    Console.WriteLine("This login exists)\n" +
                            $"This account credentials tried to register on the server\n{account}" +
                            $"Error message:{errorOccured.ErrorMsg}\n");
                             return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Smth bad :(\n" +
                    $"Exception type: {ex}");
            }
        }
    }
}
