using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DepsTest.Models
{
    public class AccountDto
    {
        public AccountDto()
        {
        }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Login: {Login}\n" +
                $"Password: {Password}\n";
        }
    }
}
