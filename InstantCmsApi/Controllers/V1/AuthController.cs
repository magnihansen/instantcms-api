using System;
using System.Threading.Tasks;
using InstantCmsApi.DomainModels;
using InstantCmsApi.Helpers;
using InstantCmsApi.Service.Authentication;
using InstantCmsApi.Controllers.V1.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstantCmsApi.Controllers.V1;

[Version(1)]
public class AuthController : BaseController
{
    private readonly IJwtManager _jwtManager;

    public AuthController(IJwtManager jwtManager)
    {
        _jwtManager = jwtManager;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<IActionResult> Login(AuthenticateRequest authenticateRequest)
    {
        string token = await _jwtManager.Authenticate(authenticateRequest.Username, authenticateRequest.Password);
        if (token == null)
        {
            return Unauthorized("Invalid login");
        }
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> ValidateToken(string token)
    {
        Tuple<bool, string> tokenValidation = await Task.FromResult(_jwtManager.ValidateCurrentToken(token));
        return Ok(tokenValidation.Item1);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetClaimValue(string claimType)
    {
        object user = await Task.FromResult<object>(this.Request.HttpContext.Items["User"]);
        if (user != null)
        {
            object propValue = user.GetPropValue(claimType);
            if (propValue != null)
            {
                return Ok(propValue);
            }
        }

        return NotFound("Invalid claim request");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> GetCdnToken()
    {
        string imageCdnToken = await _jwtManager.GenerateImageCdnToken(Request.GetAddressHost());
        return Ok(imageCdnToken);
    }
}
