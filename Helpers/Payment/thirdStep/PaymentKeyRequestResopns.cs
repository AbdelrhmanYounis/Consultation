using Newtonsoft.Json;

namespace Consultation.Helpers.Payment.thirdStep
{
    public class PaymentKeyRequestResopns
    {
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
