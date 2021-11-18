using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Politics.Dtos;
using Politics.Model;
using Politics.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Politics.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;
    private readonly IEmailSender _sender;

    public AuthController(IAuthService authService, IConfiguration config, IEmailSender sender)
    {
      _authService = authService;
      _config = config;
      _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var existingUser = await _authService.GetUserByEmail(registerDto.Email);
      if (existingUser is not null)
      { 
        return ValidationProblem("Vartotojas su šiuo elektroniniu paštu jau egzistuoja");
      }

      var userToCreate = new User
      {
        Email = registerDto.Email,
        DisplayName = registerDto.DisplayName,
        Activated = false
      };

      var createdUser = await _authService.Register(userToCreate, registerDto.Password);

      var token = await _authService.GenerateActivationToken(createdUser.UserId);

      await _sender.SendActivationEmail(createdUser, token);

      return StatusCode(201);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto registerUserDto)
    {
      var userFromRepo = await _authService.Login(registerUserDto.Email, registerUserDto.Password);

      if (userFromRepo is null)
      {
        return Unauthorized();
      }

      if (!userFromRepo.Activated)
      {
        return BadRequest("Kad prisijungtumėte jums reikia aktyvuoti savo paskyrą");
      }

      // generate token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWTSecret"));
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]{
          new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId),
          new Claim(ClaimTypes.Email, userFromRepo.Email),
          new Claim(ClaimTypes.Role, userFromRepo.Role.Name)
        }),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      HttpContext.Response.Cookies.Append("JWT", tokenString, new CookieOptions { HttpOnly = true, Expires = DateTime.Now.AddDays(7) });
      return Ok();
    }
    [Authorize]
    [HttpPost]
    public IActionResult Logout()
    {
      HttpContext.Response.Cookies.Delete("JWT");
      return Ok();
    }

    [HttpGet("{tokenId}")]
    public async Task<IActionResult> Activate(string tokenId)
    {
      var user = await _authService.ActivateUser(tokenId);
      if (user.Activated)
      {
        return Redirect("https://politik-rust.vercel.app");
      }
      return BadRequest("Wrong activation token");
    }

    [HttpPost]
    public async Task<IActionResult> EmailTaken(string email)
    {
      if (await _authService.UserExists(email))
      {
        return BadRequest("Email is already taken");
      }
      return Ok();
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CurrentUser()
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var user = await _authService.GetUserById(userId);
      if (user is null)
      {
        return BadRequest();
      }
      var profile = new UserProfileDto
      {
        UserId = user.UserId,
        DisplayName = user.DisplayName,
        Email = user.Email,
        Role = user.Role.Name,
      };
      return Ok(profile);
    }
  }
}
