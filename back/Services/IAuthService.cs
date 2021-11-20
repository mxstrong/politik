using Politics.Dtos;
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
    Task<UserProfileDto> GetUserByEmail(string email);
    Task<UserProfileDto> GetUserById(string userId);
    Task<User> UpdateEmail(string tokenId);
    Task<EmailChangeToken> GenerateEmailChangeToken(string userId, string newEmail);
    Task<UserProfileDto> ChangePassword(string userId, string newPassword);
    Task<UserProfileDto> ChangeDisplayName(string userId, string newDisplayName);
  }
}
