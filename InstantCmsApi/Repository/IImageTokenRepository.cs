﻿using System.Threading.Tasks;

namespace InstantCmsApi.Repository
{
    public interface ICdnTokenRepository
    {
        Task<DomainModels.CdnToken> GetCdnTokenAsync(int cdnTokenId);
        Task<bool> InsertCdnTokenAsync(DomainModels.CdnToken cdnToken);
        Task<bool> UpdateCdnTokenAsync(DomainModels.CdnToken cdnToken);
        Task<bool> DeleteCdnTokenAsync(int cdnTokenId);
    }
}

