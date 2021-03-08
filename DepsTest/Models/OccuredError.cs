using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DepsTest.Models
{
    public class OccuredError
    {
        public OccuredError()
        {
        }
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string ErrorMsg { get; set; }
        public override string ToString()
        {
            return $"[STATUS CODE <{Code}>]\n" +
                $"Error Message ----->\t{ErrorMsg}";
        }
    }
}
