using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Talent.Models
{
    public class Token
    {
        [JsonProperty("token")]
        public string TokenId { get; set; }  
        [JsonProperty("expires")]
        public string Expires { get; set; }
        [JsonProperty("userRole")]
        public string UserRole { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

    }
}
