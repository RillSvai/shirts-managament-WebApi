using Newtonsoft.Json;

namespace ShirtsManagement.Web.Data
{
    public class JwtToken
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("expiresAt")]
        public DateTime ExpiresAt { get; set; }
    }
}
