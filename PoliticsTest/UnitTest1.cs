using Politics.Services;

namespace PoliticsTest
{
  public class AuthServiceTests
  {
    [Fact]
    public void Test1()
    {
      var authService = new AuthService();
      authService.GetUsers();
    }
  }
}