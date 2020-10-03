using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitchClipSearch.Models;
using TwitchClipSearch.Twitch;
using TwitchLib.Api.Helix.Models.Clips.GetClip;

namespace TwitchClipSearch.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClipsController : ControllerBase
	{
    private readonly ITwitchAPIClient APIClient;

    public ClipsController(ITwitchAPIClient twitchAPIClient)
    {
      APIClient = twitchAPIClient;
    }

		[HttpGet]
		public async Task<ActionResult<GetClipResponse>> Get([FromQuery] SearchCriteria criteria)
		{
      var user  = await APIClient.Instance.Helix.Users.GetUsersAsync(logins: new List<string> { criteria.Streamer });
      var clips = await APIClient.Instance.Helix.Clips.GetClipAsync(broadcasterId: user.Users.First().Id, first: 100);

      return clips;
		}
	}
}
