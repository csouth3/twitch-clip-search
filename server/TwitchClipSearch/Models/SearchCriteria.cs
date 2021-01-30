using System.Collections.Generic;

namespace TwitchClipSearch.Models
{
  public class SearchCriteria
  {
    public string Streamer            { get; set; }
    public string Game                { get; set; }
    public List<string> TitleKeywords { get; set; } = new List<string>();
  }
}
