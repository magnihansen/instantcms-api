namespace InstantCmsApi.DomainModels;

public class CdnToken : BaseDateColumns
{
	public int Id { get; set; }
	public required string Token { get; set; }
	public DateTime ExpireDate { get; set; }
}

