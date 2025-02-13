using Newtonsoft.Json;

namespace WebApplication13.Model
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
