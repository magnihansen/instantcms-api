using System.Security.Principal;

namespace InstantCmsApi.Auth;

public interface IUserIdentity : IIdentity
{
    int UserId { get; }

    string Username { get; }

    string FullName { get; }

    string FirstName { get; }

    string LastName { get; }
}
