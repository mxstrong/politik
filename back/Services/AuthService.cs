using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Dtos;
using Politics.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Services
{
  public class AuthService : IAuthService
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;

    public AuthService(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<User> Login(string email, string password)
    {
      var user = await _context.Users.Include(user => user.Role).FirstOrDefaultAsync(u => u.Email == email);
      if (user == null)
      {
        return null;
      }

      if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
      {
        return null;
      }

      return user;
    }

    private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        if (!computedHash.SequenceEqual(passwordHash))
        {
          return false;
        }
      }
      return true;
    }

    public async Task<bool> UserExists(string email)
    {
      return await _context.Users.AnyAsync(x => x.Email == email && x.Activated);
    }
    public async Task<User> Register(User user, string password)
    {
      var role = await _context.Roles.SingleOrDefaultAsync(role => role.Name == "User");
      CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
      user.RoleId = role.RoleId;
      user.UserId = Guid.NewGuid().ToString();
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.CreatedAt = DateTime.Now;
      user.UpdatedAt = DateTime.Now;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      return user;
    }
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using var hmac = new System.Security.Cryptography.HMACSHA512();
      passwordSalt = hmac.Key;
      passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
    public async Task<ActivationToken> GenerateActivationToken(string userId)
    {
      var token = new ActivationToken()
      {
        Id = Guid.NewGuid().ToString(),
        UserId = userId,
        CreatedAt = DateTime.Now,
      };

      await _context.ActivationTokens.AddAsync(token);
      await _context.SaveChangesAsync();
      return token;
    }
    public async Task<User> ActivateUser(string tokenId)
    {
      var token = await _context.ActivationTokens.SingleAsync(a => a.Id == tokenId);
      var user = await _context.Users.FindAsync(token.UserId);
      user.Activated = true;
      _context.ActivationTokens.Remove(token);
      await _context.SaveChangesAsync();
      return user;
    }

    public async Task<UserProfileDto> GetUserById(string userId)
    {
      var user = await _context.Users.Include(user => user.Role).FirstOrDefaultAsync(user => user.UserId == userId);
      return _mapper.Map<User, UserProfileDto>(user);
    }

    public async Task<UserProfileDto> GetUserByEmail(string email)
    {
      var user = await _context.Users.Include(user => user.Role).FirstOrDefaultAsync(user => user.Email == email && user.Activated);
      return _mapper.Map<User, UserProfileDto>(user);
    }

    public async Task<EmailChangeToken> GenerateEmailChangeToken(string userId, string newEmail)
    {
      var token = new EmailChangeToken()
      {
        Id = Guid.NewGuid().ToString(),
        UserId = userId,
        NewEmail = newEmail,
        CreatedAt = DateTime.Now
      };

      await _context.EmailChangeTokens.AddAsync(token);
      await _context.SaveChangesAsync();
      return token;
    }

    public async Task<User> UpdateEmail(string tokenId)
    {
      var token = await _context.EmailChangeTokens.FirstOrDefaultAsync(token => token.Id == tokenId);
      if (token is null)
      {
        return null;
      }
      var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == token.UserId);
      if (user is null)
      {
        return null;
      }
      if (token.CreatedAt.AddDays(7) > DateTime.Now)
      {
        user.Email = token.NewEmail;
        _context.EmailChangeTokens.Remove(token);
        await _context.SaveChangesAsync();
      }
      return user;
    }

    public async Task<UserProfileDto> ChangePassword(string userId, string newPassword)
    {
      var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
      if (user is null)
      {
        return null;
      }
      CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      await _context.SaveChangesAsync();
      return _mapper.Map<User, UserProfileDto>(user);
    }

    public async Task<UserProfileDto> ChangeDisplayName(string userId, string newDisplayName)
    {
      var user = await _context.Users.Include(user => user.Role).FirstOrDefaultAsync(user => user.UserId == userId);
      if (user is null)
      {
        return null;
      }
      user.DisplayName = newDisplayName;
      await _context.SaveChangesAsync();
      return _mapper.Map<User, UserProfileDto>(user);
    }
    public async Task<bool> MakeModerator(string id)
    {
      var user = await _context.Users.SingleOrDefaultAsync(user => user.UserId == id);
      if (user is null)
      {
        return false;
      }
      var role = await _context.Roles.FirstOrDefaultAsync(role => role.Name == "Mod");
      if (role is null)
      {
        return false;
      }
      user.Role = role;
      await _context.SaveChangesAsync();
      return true;
    }
  }
}
