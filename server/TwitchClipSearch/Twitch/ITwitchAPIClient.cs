using TwitchLib.Api;

namespace TwitchClipSearch.Twitch
{
  public interface ITwitchAPIClient
  {
    TwitchAPI Instance { get; }
  }
}
