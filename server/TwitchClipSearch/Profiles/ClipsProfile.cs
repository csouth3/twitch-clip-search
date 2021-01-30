using AutoMapper;
using TwitchClipSearch.Models;
using TwitchLib.Api.Helix.Models.Clips.GetClips;

namespace TwitchClipSearch.Profiles
{
  public class ClipsProfile : Profile
  {
    public ClipsProfile()
    {
      CreateMap<GetClipsResponse, ClipsResult>();
    }
  }
}
