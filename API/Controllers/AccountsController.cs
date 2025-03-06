using System.Security.Claims;
using API.Services;
using API.UserDtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users
            .Include(u => u.Ingredients)
            .Include(u => u.Recipes)
            .Include(u => u.DayPlans)
            .FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        
        if (user == null) return Unauthorized();
        
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result)
        {
            return CreateUserObject(user);
        }
        return Unauthorized();
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
        {
            ModelState.AddModelError("Username", "Username taken");
            return ValidationProblem();
            //return BadRequest(ModelState);
        }
        
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        {
            ModelState.AddModelError("Email", "Email taken");
            return ValidationProblem();
            //return BadRequest(ModelState);
        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.UserName
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            return CreateUserObject(user);
        }
        
        return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User?.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized("Email claim not found in token.");
        }

        var user = await _userManager.Users
            .Include(u => u.Ingredients)
            .Include(u => u.Recipes)
            .Include(u => u.DayPlans)
            .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
        //var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Unauthorized("User not found.");
        }

        return CreateUserObject(user);
    }
    
    private UserDto CreateUserObject(AppUser user)
    {
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Image = null,
            Token = _tokenService.CreateToken(user),
            Username = user.UserName,
            // Add the counts
            IngredientsCount = user.Ingredients?.Count ?? 0,
            RecipesCount = user.Recipes?.Count ?? 0,
            DayPlansCount = user.DayPlans?.Count ?? 0
        };
    }
}