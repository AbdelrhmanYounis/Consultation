using Newtonsoft.Json;

namespace Consultation.Helpers.Payment.FristStep
{
    public class AuthTokenResponse
    {
        [JsonProperty("token")]
        public string ?Token { get; set; }
    }
}
