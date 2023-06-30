
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dto;
using PublicApi.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PublicAPI.Controllers.Admin;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UserController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager
        )
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    [HttpGet]
    [Route("GetAllUsers")]
    public IActionResult GetAllUsers() => Ok(_userManager.Users.ToList());

    [HttpGet]
    [Route("GetByIdUsers")]
    public IActionResult GetByIdUsers(int id) => Ok(_userManager.Users.FirstOrDefault(user => user.Id == id));

    
    [HttpPost("InsertUserWithRole")]
    public async Task<IActionResult> InsertWithRole([FromBody] UserDto model)
    {
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status100Continue, "Not Valid");


        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Title = model.Title,
            BirthDate = model.BirthDate,
        };

        ApplicationRole role = await _roleManager.FindByNameAsync(model.RoleName).ConfigureAwait(false);
        if (role == null) return StatusCode(StatusCodes.Status100Continue, "find not exists");

        IdentityResult result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
        if (!result.Succeeded) return StatusCode(StatusCodes.Status100Continue, "Create User Faild");

        IdentityResult result2 = await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
        if (!result2.Succeeded) return StatusCode(StatusCodes.Status100Continue, "Create User Faild");


        return Ok("Create User SccessFull");


    }


}
