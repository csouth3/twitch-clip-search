using Newtonsoft.Json;

namespace TwitchClipSearch.Models
{
  public class TokenResponse
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    [JsonProperty("expires_in")]
    public int ExpiresIn      { get; set; }
  }
}
