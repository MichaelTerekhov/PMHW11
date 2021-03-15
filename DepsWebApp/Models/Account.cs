using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DepsWebApp.Models
{
    /// <summary>
    /// User account model
    /// </summary>
    public class Account
    {
#pragma warning disable CS1591
        public Account()
        {
        }
#pragma warning restore CS1591
        /// <summary>
        /// Ctor for setting account parameters
        /// </summary>
        public Account(int id,string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
        /// <summary>
        /// Id of account
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; } 
        /// <summary>
        /// User-invented login for sign up / registration
        /// </summary>
        [JsonPropertyName("login")]
        public string Login { get; private set; }
        /// <summary>
        /// User-invented password for sign up / registration
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; private set; }
    }
}
