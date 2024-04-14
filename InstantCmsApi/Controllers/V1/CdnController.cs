using InstantCmsApi.Helpers;
using InstantCmsApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace InstantCmsApi.Controllers.V1;

[Version(1)]
public class CdnController : BaseController
{
    private readonly ICdnTokenService _cdnTokenService;

    public CdnController(ICdnTokenService cdnTokenService)
    {
        _cdnTokenService = cdnTokenService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<string> GetImagesAsync()
    {
        string token = await _cdnTokenService.GenerateCdnTokenAsync(Request.GetAddressHost());
        return token;
    }
}

