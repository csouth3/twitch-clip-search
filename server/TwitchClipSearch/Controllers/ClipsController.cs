using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TwitchClipSearch.Models;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Clips.GetClip;

namespace TwitchClipSearch.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClipsController : ControllerBase
	{
    private static TwitchAPI api;

    public ClipsController(IOptions<TwitchApiSettings> twitchApiSettings)
    {
      var clientId            = twitchApiSettings.Value.ClientId;
      var clientSecret        = twitchApiSettings.Value.ClientSecret;
      TokenResponse tokenInfo = new TokenResponse();

      using(var client = new WebClient())
      {
        var response  = client.UploadString($"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials", string.Empty);
        tokenInfo     = JsonConvert.DeserializeObject<TokenResponse>(response);
      }

      api                       = new TwitchAPI();
      api.Settings.ClientId     = clientId;
      api.Settings.AccessToken  = tokenInfo.AccessToken;
    }

		[HttpGet]
		public async Task<ActionResult<GetClipResponse>> Get()
		{
      var user  = await api.Helix.Users.GetUsersAsync(logins: new List<string> { "PaymoneyWubby" });
      var clips = await api.Helix.Clips.GetClipAsync(broadcasterId: user.Users.First().Id, first: 100);

      return clips;
		}
	}
}
