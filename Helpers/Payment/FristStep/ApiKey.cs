﻿using Newtonsoft.Json;

namespace Consultation.Helpers.Payment.FristStep
{
    public class ApiKey
    {
        [JsonProperty("api-key")]
        public string Key { set; get; }
    }
}