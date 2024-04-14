namespace InstantCmsApi.Auth;

public interface IBasicAuthenticationService
{
    Task<bool> IsValidUserAsync(string user, string password);
}
