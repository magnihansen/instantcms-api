﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InstantCmsApi.Service.Authentication;

public interface IJwtManager
{
    Task<string> Authenticate(string username, string password);
    Task<ViewModels.UserVM> GetUserByIdentity(ClaimsIdentity identity);
    Tuple<bool, string> ValidateCurrentToken(string token);
    string GetClaim(string token, string claimType);
    Task<string> GenerateImageCdnToken(string host);
}
