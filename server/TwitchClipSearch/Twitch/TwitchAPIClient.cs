using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TwitchClipSearch.Models;
using TwitchLib.Api;

namespace TwitchClipSearch.Twitch
{
  public class TwitchAPIClient : ITwitchAPIClient
  {
    public TwitchAPIClient(IOptions<TwitchApiSettings> twitchApiSettings)
    {
      var clientId      = twitchApiSettings.Value.ClientId;
      var clientSecret  = twitchApiSettings.Value.ClientSecret;
      var tokenInfo     = new TokenResponse();

      using(var client = new WebClient())
      {
        var response  = client.UploadString($"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials", string.Empty);
        tokenInfo     = JsonConvert.DeserializeObject<TokenResponse>(response);
      }

      Instance                      = new TwitchAPI();
      Instance.Settings.ClientId    = clientId;
      Instance.Settings.AccessToken = tokenInfo.AccessToken;
    }

    public TwitchAPI Instance { get; }
  }
}
