namespace InstantCmsApi.Auth;

public interface IJWTManagerRepository
{
    Tokens Authenticate(Users users);
}
