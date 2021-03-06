using DepsTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
                using var requestMessage = new HttpRequestMessage(HttpMethod.Get,newUri);
                var response = await client.SendAsync(requestMessage);
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
        public static async Task GetAmountInAnotherCurrencyLogined(string partOfUrn, string approximateAmount,string key)
        {
            HttpClient _client;
            try
            {
                var jsonSettings = await File.ReadAllTextAsync("connectionSettings.json");
                var customUri = JsonSerializer.Deserialize<CustomUri>(jsonSettings);
                _client = new HttpClient()
                {
                    BaseAddress = new Uri(customUri.Uri)
                };
                var newUri = _client.BaseAddress + partOfUrn;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", key);
                var response = await _client.GetAsync(newUri);
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
