using DotCreative.Services.PaymentCenter.Core.Shared.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Entities;

public class PlatformData
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string Key { get; set; }

    public PlatformData(EPlatforms platform, string email, string token, string key)
    {
        Email = email;
        Token = token;
        Key = key;
    }

    public PlatformData(string token, string key)
    {
        Token = token;
        Key = key;
    }

    public PlatformData(string key)
    {
        Key = key;
    }
}
