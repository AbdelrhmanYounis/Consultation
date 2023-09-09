using Newtonsoft.Json;

namespace Consultation.Helpers.Payment.secondStep
{
    public class OrderRegistrationResponse
    {
        [JsonProperty("id")]
        public string? Id { get; set; }  
    }
}
