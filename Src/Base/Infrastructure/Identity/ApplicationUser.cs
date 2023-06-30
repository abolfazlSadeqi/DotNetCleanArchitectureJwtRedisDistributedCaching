
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure;

public class ApplicationUser : IdentityUser<long>
{

   


    [Required, Column(TypeName = "VARCHAR(250)")]
    public string FirstName { get; set; }

    [Required, Column(TypeName = "VARCHAR(250)")]
    public string LastName { get; set; }

    [Required, Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }

    public DateTime BirthDate { get; set; }

}
