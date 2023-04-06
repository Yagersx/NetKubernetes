using Microsoft.AspNetCore.Identity;

namespace NetKubernates.Models;

public class Usuario : IdentityUser{

    // public override Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
}