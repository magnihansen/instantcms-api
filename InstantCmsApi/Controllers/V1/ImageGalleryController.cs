using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCmsApi.Controllers.V1.Requests;
using InstantCmsApi.DomainModels;
using InstantCmsApi.Helpers;
using InstantCmsApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstantCmsApi.Controllers.V1;

[Version(1)]
public class ImageGalleryController : BaseController
{
    private readonly IImageGalleryService _imageGalleryService;

    public ImageGalleryController(IImageGalleryService imageGalleryService)
    {
        _imageGalleryService = imageGalleryService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DomainModels.ImageGallery>))]
    public async Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync()
    {
        List<DomainModels.ImageGallery> imageGalleries = await _imageGalleryService.GetImageGalleriesAsync();
        return imageGalleries;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DomainModels.ImageGallery))]
    public async Task<List<DomainModels.Image>> GetImageGalleryImagesByPageIdAsync(int pageId)
    {
        List<DomainModels.Image> images = await _imageGalleryService.GetImageGalleryImagesByPageIdAsync(Request.GetAddressHost(), pageId);
        return images;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DomainModels.ImageGallery))]
    public async Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId)
    {
        DomainModels.ImageGallery imageGallery = await _imageGalleryService.GetImageGalleryAsync(imageGalleryId);
        return imageGallery;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<bool> InsertImageGalleryAsync(InsertImageGalleryRequest insertImageGalleryRequest)
    {
        var imageGallery = new DomainModels.ImageGallery()
        {
            Name = insertImageGalleryRequest.Name,
            Description = insertImageGalleryRequest.Description,
            ImageGalleryTypeId = insertImageGalleryRequest.ImageGalleryTypeId,
            ImageGalleryTemplateId = insertImageGalleryRequest.ImageGalleryTemplateId,
            CreatedBy = insertImageGalleryRequest.CreatedBy
        };
        bool inserted = await _imageGalleryService.InsertImageGalleryAsync(Request.GetAddressHost(), imageGallery);
        return inserted;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery)
    {
        bool updated = await _imageGalleryService.UpdateImageGalleryAsync(imageGallery);
        return updated;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<bool> DeleteImageGalleryAsync(int imageGalleryId)
    {
        bool deleted = await _imageGalleryService.DeleteImageGalleryAsync(imageGalleryId);
        return deleted;
    }
}

