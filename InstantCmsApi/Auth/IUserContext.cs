using System.Security.Principal;

namespace InstantCmsApi.Auth;

public interface IUserContext : IPrincipal
{
    IUserIdentity User { get; }

    string JwtToken { get; }

    DateTimeOffset? JwtTokenExpires { get; }
}
