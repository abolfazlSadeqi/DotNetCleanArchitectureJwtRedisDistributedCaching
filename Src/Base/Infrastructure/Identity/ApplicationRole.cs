


using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class ApplicationRole : IdentityRole<long>
{
    public ApplicationRole() { }
    public ApplicationRole(string name) { Name = name; }
}
