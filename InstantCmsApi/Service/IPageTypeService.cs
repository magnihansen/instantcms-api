using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCmsApi.Controllers.V1.Requests;

namespace InstantCmsApi.Service
{
    public interface IPageTypeService
    {
        Task<List<ViewModels.PageTypeVM>> GetPageTypesAsync();

        Task<ViewModels.PageTypeVM> GetPageTypeAsync(int pageTypeId);

        Task<bool> InsertPageTypeAsync(DomainModels.PageType pageType);

        Task<bool> UpdatePageTypeAsync(DomainModels.PageType pageType);

        Task<bool> DeletePageTypeAsync(int pageTypeId);
    }
}
