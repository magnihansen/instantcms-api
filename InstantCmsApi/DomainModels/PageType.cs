namespace InstantCmsApi.DomainModels;

public class PageType : BaseDateColumns
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool Active { get; set; }      
}
