using DepsTest.Models;
using DepsTest.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsTest
{
    class Program
    {
        private static HttpClient client;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Test app by (c)Michael Terekhov");
            try
            {
                var jsonSettings = await File.ReadAllTextAsync("connectionSettings.json");
                var customUri = JsonSerializer.Deserialize<CustomUri>(jsonSettings);
                client = new HttpClient()
                {
                    BaseAddress = new Uri(customUri.Uri)
                };
                await RegistrationTestPart();
                await ExchangeTest();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Smth bad");
            }
        }

        private async static Task RegistrationTestPart()
        {
            try
            {
                List<AccountDto> testAccounts = new List<AccountDto>();
                using (FileStream fs = new FileStream("registrationOptions.json", FileMode.OpenOrCreate))
                {
                    testAccounts = await JsonSerializer.DeserializeAsync<List<AccountDto>>(fs);
                }
                var tasks = testAccounts.Select(x => AuthControllerTesterService.RegistrationChecker(client, x));
                await Task.WhenAll(tasks);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Smth happened wrong!");
            }
        }
        private async static Task ExchangeTest()
        {
            var jsonSettings = await File.ReadAllTextAsync("exchangeOptions.json");
            var testingDictionary = JsonSerializer.Deserialize<ConcurrentDictionary<string, string>>(jsonSettings);
            var tasks = await Task.Factory.StartNew(() =>
                testingDictionary.Select(x => ExchangeTestService.GetAmountInAnotherCurrency(client, x.Key, x.Value)));
            
            await Task.WhenAll(tasks);

            Console.WriteLine("\n Trying to excange(authenticated in platform)\n");

            var tasks2 = await Task.Factory.StartNew(() =>
                testingDictionary.Select(x => ExchangeTestService.GetAmountInAnotherCurrencyLogined(x.Key, x.Value, "c2Rmc2RmOnBhc3Nmcw==")));
            await Task.WhenAll(tasks2);
        }
    }
}
