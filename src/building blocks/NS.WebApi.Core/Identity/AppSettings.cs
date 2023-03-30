namespace NS.WebApi.Core.Identity;

public class AppSettings
{
    public string Secret { get; set; }
    public int ExpirationTimeInHours { get; set; }
    public string Issuer { get; set; }
    public string ValidAudience { get; set; }
}
