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
                var response = await client.PostAsync(newUri, account,new JsonMediaTypeFormatter());

                var content = await response.Content.ReadAsStringAsync();
                var errorOccured =  JsonSerializer.Deserialize<OccuredError>(content);
                if (errorOccured.Code == 228)
                {
                    Console.WriteLine("TEST PASSED(THIS METHOD ON API SIDE NOT IMPLEMENTED)\n" +
                        $"This account credentials tried to register on the server\n{account}");
                }
                else 
                {
                    Console.WriteLine("Hmmm, its seems that this method were implemented! Or smth happened wrong!!!");
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
