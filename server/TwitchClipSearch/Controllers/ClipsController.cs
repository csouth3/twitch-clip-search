using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TwitchClipSearch.Models;
using TwitchClipSearch.Twitch;
using TwitchLib.Api.Helix.Models.Clips.GetClips;

namespace TwitchClipSearch.Controllers
{
  [ApiController]
  [Route("[controller]")]
	public class ClipsController : ControllerBase
	{
    private readonly ITwitchAPIClient APIClient;
    private readonly IMapper _mapper;

    public ClipsController(ITwitchAPIClient twitchAPIClient, IMapper mapper)
    {
      APIClient = twitchAPIClient;
      _mapper   = mapper;
    }

		[HttpGet]
		public async Task<ActionResult<ClipsResult>> Get([FromQuery] SearchCriteria criteria)
		{
      GetClipsResponse clipsResponse;
      if(!string.IsNullOrEmpty(criteria.Streamer))
      {
        var user  = await APIClient.Instance.Helix.Users.GetUsersAsync(logins: new List<string> { criteria.Streamer });
        clipsResponse = await APIClient.Instance.Helix.Clips.GetClipsAsync(broadcasterId: user.Users.First().Id, first: 100);
      }
      else if(!string.IsNullOrEmpty(criteria.Game))
      {
        var game  = await APIClient.Instance.Helix.Games.GetGamesAsync(gameNames: new List<string> { criteria.Game });
        clipsResponse = await APIClient.Instance.Helix.Clips.GetClipsAsync(gameId: game.Games.First().Id, first: 100);
      }
      else
      {
        return BadRequest();
      }

      var clips = _mapper.Map<ClipsResult>(clipsResponse);
      if(!criteria.TitleKeywords.Any())
        return clips;

      clips.Clips = clips.Clips.Where(clip => criteria.TitleKeywords.Any(keyword => clip.Title.ToLower().Contains(keyword.ToLower()))).ToList();

      return clips;
		}
	}
}
