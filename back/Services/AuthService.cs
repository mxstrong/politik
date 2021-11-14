using Microsoft.EntityFrameworkCore;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Services
{
  public class AuthService : IAuthService
  {
    private readonly PoliticsDbContext _context;

    public AuthService(PoliticsDbContext context)
    {
      _context = context;
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
      if (role is null)
      {
        role = new Role
        {
          RoleId = Guid.NewGuid().ToString(),
          Name = "User"
        };
        await _context.Roles.AddAsync(role);
      }
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

    public async Task<User> GetUserById(string userId)
    {
      return await _context.Users.FindAsync(userId);
    }

    public async Task<User> GetUserByEmail(string email)
    {
      return await _context.Users.FirstOrDefaultAsync(user => user.Email == email && user.Activated);
    }
  }
}
