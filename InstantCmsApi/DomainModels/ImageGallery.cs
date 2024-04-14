﻿namespace InstantCmsApi.DomainModels;

public class ImageGallery : BaseDateColumns
{
    public int Id { get; set; }
    public int DomainId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ImageGalleryTypeId { get; set; }
    public int ImageGalleryTemplateId { get; set; }
    public bool Active { get; set; }
}