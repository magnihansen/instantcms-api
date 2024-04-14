namespace InstantCmsApi.DomainModels;

public class SettingKey
{
	public int Id { get; set; }
	public int SettingTypeId { get; set; }
	public required string Key { get; set; }
	public bool IsDefault { get; set; }
	public string? DefaultValue { get; set; }
}

