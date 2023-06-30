using Common.UI.Jwt;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.AccessJwt;
using PublicApi.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PublicAPI.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UserJwtController : ApiControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserJwtController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null) return Unauthorized();

        var check = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!check) return Unauthorized();

        var userRoles = await _userManager.GetRolesAsync(user);

        var _listClaim = HelperJwt.GetClaim(userRoles, user.UserName, user.Id.ToString());
        var token = HelperJwt.GetToken(_listClaim, _configuration);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });

    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistration model)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status100Continue, "Not Valid");

        var userExists = await _userManager.FindByNameAsync(model.Username);

        if (userExists != null) return StatusCode(StatusCodes.Status500InternalServerError, "username exists");

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

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create user");

        return Ok("created successfully");
    }


}
