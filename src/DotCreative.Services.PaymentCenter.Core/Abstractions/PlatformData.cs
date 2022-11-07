using DotCreative.Services.PaymentCenter.Core.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public class PlatformData
{
  public EPlatforms Platform { get; set; }
  public string Email { get; set; }
  public string Token { get; set; }
  public string Key { get; set; }

  public PlatformData(EPlatforms platform, string email, string token, string key)
  {
    Platform = platform;
    Email = email;
    Token = token;
    Key = key;
  }

  public PlatformData(EPlatforms platform, string token, string key)
  {
    Platform = platform;
    Token = token;
    Key = key;
  }

  public PlatformData(EPlatforms platform, string key)
  {
    Platform = platform;
    Key = key;
  }
}
