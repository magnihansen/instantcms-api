﻿namespace InstantCmsApi.DomainModels;

public class User : BaseDateColumns
{
    public int Id { get; set; }
    public int DomainId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Address { get; set; }
    public string? Zip { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public bool Active { get; set; }
    public bool IsAdmin { get; set; }
}