using System.Security.Claims;
using Fedodo.NuGet.Common.Handlers;
using Fedodo.NuGet.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Fedodo.NuGet.Common.Test.Handlers;

public class UserHandlerShould
{
    private readonly UserHandler _userHandler;
    
    public UserHandlerShould()
    {
        var logger = new Mock<ILogger<UserHandler>>();
        var repository = new Mock<IMongoDbRepository>();
        
        _userHandler = new UserHandler(logger.Object, repository.Object);
    }
    
    [Fact]
    public void VerifyUser()
    {
        // Arrange
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(i => i.User.Claims).Returns(new List<Claim>()
        {
            new Claim("sub", "AF0A675A-295D-447D-87D6-EF43BAB3E6F1"),
        });

        // Act
        var result = _userHandler.VerifyUser(Guid.Parse("AF0A675A-295D-447D-87D6-EF43BAB3E6F1"), httpContextMock.Object);

        // Assert
        result.ShouldBeTrue();
    }
}