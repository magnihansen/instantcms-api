﻿namespace InstantCmsApi.Service.ViewModels;

public class UserVM
{
    public UserVM() { }

    public UserVM(int id, int domainId, string username, string firstname, string lastname, string address,
        string zip, string city, string country, string email, string phone, bool active,
        DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy,
        bool isAdmin)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
        }

        if (string.IsNullOrWhiteSpace(firstname))
        {
            throw new ArgumentException($"'{nameof(firstname)}' cannot be null or whitespace.", nameof(firstname));
        }

        if (string.IsNullOrWhiteSpace(lastname))
        {
            throw new ArgumentException($"'{nameof(lastname)}' cannot be null or whitespace.", nameof(lastname));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(createdBy))
        {
            throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace.", nameof(createdBy));
        }

        Id = id;
        DomainId = domainId;
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Zip = zip ?? throw new ArgumentNullException(nameof(zip));
        City = city ?? throw new ArgumentNullException(nameof(city));
        Country = country ?? throw new ArgumentNullException(nameof(country));
        Email = email;
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        Active = active;
        CreatedBy = createdBy;
        IsAdmin = isAdmin;
    }

    public int Id { get; }
    public int DomainId { get; }
    public string Username { get; }
    public string Firstname { get; }
    public string Lastname { get; }
    public string Address { get; }
    public string Zip { get; }
    public string City { get; }
    public string Country { get; }
    public string Email { get; }
    public string Phone { get; }
    public bool Active { get; }
    public string CreatedBy { get; }
    public bool IsAdmin { get; }
}
