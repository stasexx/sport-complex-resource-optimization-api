using SportComplexResourceOptimizationApi.Domain.Entities;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;


namespace SportComplexResourceOptimizationApi.Entities;

public class User : EntityBase 
{
    public Guid GuestId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public List<Role> Roles { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }
}