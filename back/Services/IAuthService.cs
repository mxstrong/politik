using Politics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Services
{
  public interface IAuthService
  {
    Task<User> Register(User user, string password);
    Task<ActivationToken> GenerateActivationToken(string UserId);
    Task<User> ActivateUser(string tokenId);
    Task<User> Login(string email, string password);
    Task<bool> UserExists(string email);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserById(string userId);
  }
}
