using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DepsTest.Services
{
    public static class ExchangeTestService
    {
        public static async Task GetAmountInAnotherCurrency(HttpClient client, string partOfUrn, string approximateAmount)
        {
            try
            {
                var newUri = client.BaseAddress + partOfUrn;
                var response = await client.GetAsync(newUri);
                string body = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Congratulations!\n" +
                        $"BY THIS  URL: {newUri}\n" +
                        $"Approx. what you can get: [{approximateAmount}]\n" +
                        $"What you get after request: [{body}]");
                }
                else 
                {
                    Console.WriteLine("We cant process this!\n" +
                        $"BY THIS URL: {newUri}\n" +
                        $"Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OOPs smth went wrong\n" +
                    $"Check type of exception: {ex}\n" +
                    $"{ex.Message}");
            }
        }
    }
}
