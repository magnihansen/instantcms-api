using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantCmsApi.Repository
{
    public interface IImageGalleryRepository
    {
        Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync();
        Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId);
        Task<List<DomainModels.Image>> GetImageGalleryImagesByPageIdAsync(int domainId, int pageId);
        Task<bool> InsertImageGalleryAsync(DomainModels.ImageGallery imageGallery);
        Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery);
        Task<bool> DeleteImageGalleryAsync(int imageGalleryId);
    }
}

