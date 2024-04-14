using System;
using System.Threading.Tasks;

namespace InstantCmsApi.Repository
{
	public interface IDomainRepository
	{
        Task<DomainModels.Domain> GetDomainAsync(string host);

    }
}
