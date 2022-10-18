using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Politics.Model;
using Xunit;
using Politics.Services;

namespace PoliticsTests
{
    // public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    // {
    //     public AsyncEnumerable(Expression expression)
    //         : base(expression) { }
    //
    //     public IAsyncEnumerator<T> GetEnumerator() =>
    //         new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    //
    //     public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    //     {
    //         return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    //     }
    // }
    //
    // public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    // {
    //     private readonly IEnumerator<T> enumerator;
    //
    //     public AsyncEnumerator(IEnumerator<T> enumerator) =>
    //         this.enumerator = enumerator ?? throw new ArgumentNullException();
    //
    //     public ValueTask<bool> MoveNextAsync()
    //     {
    //         return ValueTask.FromResult(enumerator.MoveNext());
    //     }
    //
    //     public T Current => enumerator.Current;
    //
    //     public void Dispose() { }
    //
    //     public Task<bool> MoveNext(CancellationToken cancellationToken) =>
    //         Task.FromResult(enumerator.MoveNext());
    //
    //     public ValueTask DisposeAsync()
    //     {
    //         return ValueTask.CompletedTask;
    //     }
    // }
    public class AuthServiceTests
    {
        [Fact]
        public async void UpdateEmailShouldSetEmailToNewOne()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Activated = true, CreatedAt = DateTime.Now, DisplayName = "test", Email = "test@mail.com",
                    PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>(),
                    Role = new Role() { Name = "User", RoleId = "test" }, RoleId = "test", UpdatedAt = DateTime.Now,
                    UserId = "test"
                }
            };
            var tokens = new List<EmailChangeToken>()
            {
                new EmailChangeToken()
                {
                    CreatedAt = DateTime.Now,
                    Id = "test",
                    NewEmail = "changed@mail.com",
                    UserId = "test"
                }
            };
            var dbContextMock = new Mock<PoliticsDbContext>();
            dbContextMock.Setup(c => c.Users).ReturnsDbSet(users);
            dbContextMock.Setup(c => c.EmailChangeTokens).ReturnsDbSet(tokens);
            var mapperMock = new Mock<IMapper>();
            var authService = new AuthService(dbContextMock.Object, mapperMock.Object);
            var user = await authService.UpdateEmail("test");
            Assert.Equal(user.Email, tokens.First().NewEmail);
        }
        [Fact]
        public async void IfUserIsNotFoundUpdateEmailShouldFail()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Activated = true, CreatedAt = DateTime.Now, DisplayName = "test", Email = "test@mail.com",
                    PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>(),
                    Role = new Role() { Name = "User", RoleId = "test" }, RoleId = "test", UpdatedAt = DateTime.Now,
                    UserId = "test2"
                }
            };
            var tokens = new List<EmailChangeToken>()
            {
                new EmailChangeToken()
                {
                    CreatedAt = DateTime.Now,
                    Id = "test",
                    NewEmail = "changed@mail.com",
                    UserId = "test"
                }
            };
            var dbContextMock = new Mock<PoliticsDbContext>();
            dbContextMock.Setup(c => c.Users).ReturnsDbSet(users);
            dbContextMock.Setup(c => c.EmailChangeTokens).ReturnsDbSet(tokens);
            var mapperMock = new Mock<IMapper>();
            var authService = new AuthService(dbContextMock.Object, mapperMock.Object);
            var user = await authService.UpdateEmail("test");
            Assert.Equal(user, null);
        }
        [Fact]
        public async void IfTokenIsNotFoundUpdateEmailShouldFail()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Activated = true, CreatedAt = DateTime.Now, DisplayName = "test", Email = "test@mail.com",
                    PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>(),
                    Role = new Role() { Name = "User", RoleId = "test" }, RoleId = "test", UpdatedAt = DateTime.Now,
                    UserId = "test"
                }
            };
            var tokens = new List<EmailChangeToken>()
            {
                new EmailChangeToken()
                {
                    CreatedAt = DateTime.Now,
                    Id = "test2",
                    NewEmail = "changed@mail.com",
                    UserId = "test"
                }
            };
            var dbContextMock = new Mock<PoliticsDbContext>();
            dbContextMock.Setup(c => c.Users).ReturnsDbSet(users);
            dbContextMock.Setup(c => c.EmailChangeTokens).ReturnsDbSet(tokens);
            var mapperMock = new Mock<IMapper>();
            var authService = new AuthService(dbContextMock.Object, mapperMock.Object);
            var user = await authService.UpdateEmail("test");
            Assert.Equal(user, null);
        }
        [Fact]
        public async void IfTokenIsExpiredUpdateEmailShouldNotChangeEmail()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Activated = true, CreatedAt = DateTime.Now, DisplayName = "test", Email = "test@mail.com",
                    PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>(),
                    Role = new Role() { Name = "User", RoleId = "test" }, RoleId = "test", UpdatedAt = DateTime.Now,
                    UserId = "test"
                }
            };
            var tokens = new List<EmailChangeToken>()
            {
                new EmailChangeToken()
                {
                    CreatedAt = DateTime.Now - TimeSpan.FromDays(8),
                    Id = "test",
                    NewEmail = "changed@mail.com",
                    UserId = "test"
                }
            };
            var dbContextMock = new Mock<PoliticsDbContext>();
            dbContextMock.Setup(c => c.Users).ReturnsDbSet(users);
            dbContextMock.Setup(c => c.EmailChangeTokens).ReturnsDbSet(tokens);
            var mapperMock = new Mock<IMapper>();
            var authService = new AuthService(dbContextMock.Object, mapperMock.Object);
            var user = await authService.UpdateEmail("test");
            Assert.Equal(user.Email, "test@mail.com");
        }
    }
}


