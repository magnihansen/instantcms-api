namespace InstantCmsApi.DomainModels;

public class SettingDTO : BaseDateColumns
{
    public int Id { get; set; }
    public int? DomainId { get; set; }
    public int SettingKeyId { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; }
}
