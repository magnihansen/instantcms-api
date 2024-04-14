using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCmsApi.Service.ViewModels;

namespace InstantCmsApi.Service
{
    public interface IDataService
    {
        Task<ContactVM> GetContactAsync(int contactId);
        Task<List<ContactVM>> GetContactsAsync();
    }
}