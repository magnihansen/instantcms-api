namespace InstantCmsApi.DomainModels;

public class SettingType : BaseDateColumns
    {
	public int Id { get; set; }
	public int DomainId { get; set; }
	public required string Name { get; set; }
}

