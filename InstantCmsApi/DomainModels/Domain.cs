using System;
namespace InstantCmsApi.DomainModels;

public class Domain : BaseDateColumns
{
	public int Id { get; set; }
	public string? Host { get; set; }
	public bool Active { get; set; }
}

