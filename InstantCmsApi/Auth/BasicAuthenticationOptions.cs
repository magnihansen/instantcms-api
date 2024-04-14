using Microsoft.AspNetCore.Authentication;

namespace InstantCmsApi.Auth;

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
    public string? Realm { get; set; }
}
