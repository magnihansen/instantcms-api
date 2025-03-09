﻿using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using InstantCmsApi.Helpers;
using InstantCmsApi.Service.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace InstantCmsApi.Auth;

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    // private readonly ILmAuthService _authService;
    private const string AuthorizationHeaderName = "Authorization";
    private const string AuthTokenSchemeName = "AuthToken";
    private readonly UserClaimsHandler _claimsHandler;
    private readonly IJwtManager _jwtManager;

  public BasicAuthenticationHandler(
          IOptionsMonitor<BasicAuthenticationOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          ISystemClock clock,
          UserClaimsHandler claimsHandler,
          IJwtManager jwtManager) : base(options, logger, encoder, clock)
  {
    _claimsHandler = claimsHandler;
    _jwtManager = jwtManager;
  }

  /// <summary>
  /// Searches the 'Authorization' header for a 'AuthToken' token. If the 'AuthToken' token is found, it is validated using the JWT from AuthA8.
  /// </summary>
  /// <returns></returns>
  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var authToken = GetTokenFromHeader() ?? GetTokenFromQuery();

            // If no authorization header found, nothing to process further
            if (string.IsNullOrEmpty(authToken))
            {
                return AuthenticateResult.NoResult();
            }

            if (authToken.StartsWith($"{Scheme.Name} ", StringComparison.OrdinalIgnoreCase))
            {
                authToken = authToken.Substring($"{Scheme.Name} ".Length).Trim();
            }

            // If no token found, no further work possible
            if (string.IsNullOrEmpty(authToken))
            {
                return AuthenticateResult.NoResult();
            }

            (bool success, string message, string data) authenticationResult = await TryVerifyAccessTokenAsync(authToken);
            if (!authenticationResult.success)
            {
                return AuthenticateResult.Fail(authenticationResult.message);
            }

            var jwt = authenticationResult.data;
            var claims = DecodeToken(jwt);

            if (claims == null)
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            if (claims.ExpiresAt.FromEpochAsDateTimeOffset() < DateTimeOffset.UtcNow)
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            var ticket = CreateAuthTicket(authToken, claims);
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }
    }

    private Task<(bool, string, string)> TryVerifyAccessTokenAsync(string accessToken)
    {
        Tuple<bool,string> jwtToken = _jwtManager.ValidateCurrentToken(accessToken);

        var response = (success: jwtToken.Item1, message: "", data: jwtToken.Item2);

        return Task.FromResult(response);
    }

    private static UserClaims DecodeToken(string jwt)
    {
        var jwtUrlEncoder = new Base64UrlEncoder();
        var jwtDecoder = new HeadlessJwtDecoder<UserClaims>(jwtUrlEncoder); // This decoder does not verify the validity of the token, it just decodes it.

        // TODO: Get the public key and verify the token. Then again, since this token is coming from a "trusted" source, we don't really need to verify it.

        return jwtDecoder.Decode(jwt);
    }

    private AuthenticationTicket CreateAuthTicket(string authToken, UserClaims userClaims)
    {
        var claims = _claimsHandler.GetClaims(userClaims, authToken, Scheme.Name);

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new System.Security.Principal.GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return ticket;
    }

    private string GetTokenFromHeader()
    {
        // If no authorization header found, nothing to process further
        if (!Request.Headers.ContainsKey(AuthorizationHeaderName) ||
        !AuthenticationHeaderValue.TryParse(Request.Headers[AuthorizationHeaderName], out AuthenticationHeaderValue authorization))
        {
            return null;
        }

        // Only allow certain schemes
        string authToken = null;
        if (Scheme.Name.Equals(authorization.Scheme, StringComparison.OrdinalIgnoreCase) ||
        AuthTokenSchemeName.Equals(authorization.Scheme, StringComparison.OrdinalIgnoreCase))
        {
            authToken = authorization.Parameter;
        }

        return authToken;
    }

    private string GetTokenFromQuery()
    {
        // SignalR
        // https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-3.1
        return Request.Query["access_token"];
    }
}
