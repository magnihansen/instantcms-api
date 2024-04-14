using System.Threading.Tasks;
using InstantCmsApi.Auth;

namespace InstantCmsApi.Service
{
    public interface ICdnTokenService
    {
        Task<string> GenerateCdnTokenAsync(string host);
    }
}

