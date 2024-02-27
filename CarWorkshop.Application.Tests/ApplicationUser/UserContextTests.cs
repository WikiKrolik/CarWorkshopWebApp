using Xunit;
using CarWorkshop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions;

namespace CarWorkshop.Application.ApplicationUser.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            var currentUser = userContext.GetCurrentUser();

            //arange
            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser!.Email.Should().Be("test@test.com");
        }
    }
}