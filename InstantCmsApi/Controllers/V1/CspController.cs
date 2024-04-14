using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InstantCmsApi.Controllers.V1;

[Version(1)]
public class CspController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<string> GetNonce()
    {
        Guid nonce = Guid.NewGuid(); ;
        return await Task.FromResult(nonce.ToString());
    }
}

