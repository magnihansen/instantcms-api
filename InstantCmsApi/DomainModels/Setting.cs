namespace InstantCmsApi.DomainModels;

public class Setting : BaseDateColumns
{
    public Setting(int id, int settingKeyId, string updatedBy, string value)
    {
        Id = id;
        SettingKeyId = settingKeyId;
        UpdatedBy = updatedBy;
        Value = value;
    }

    public int Id { get; set; }
	public int? DomainId { get; set; }
	public int SettingKeyId { get; set; }
	public string Value { get; set; }
}

