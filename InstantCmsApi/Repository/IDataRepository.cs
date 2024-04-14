using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCmsApi.DomainModels;

namespace InstantCmsApi.Repository
{
    public interface IDataRepository
    {
        Task<Contact> GetContactAsync(int contactId);

        Task<List<DomainModels.Contact>> GetContactsAsync();
    }
}