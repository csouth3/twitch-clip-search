using Newtonsoft.Json;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Clips.GetClips;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchClipSearch.Models
{
  public class ClipsResult
  {
    [JsonProperty(PropertyName = "data")]
    public List<Clip> Clips { get; set; }
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; set; }
  }
}
