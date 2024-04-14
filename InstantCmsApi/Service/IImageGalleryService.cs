using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantCmsApi.Service
{
    public interface IImageGalleryService
    {
        Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync();
        Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId);
        Task<List<DomainModels.Image>> GetImageGalleryImagesByPageIdAsync(string host, int pageId);
        Task<bool> InsertImageGalleryAsync(string host, DomainModels.ImageGallery imageGallery);
        Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery);
        Task<bool> DeleteImageGalleryAsync(int imageGalleryId);
    }
}

