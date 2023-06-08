using Fedodo.NuGet.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Fedodo.NuGet.Common.Interfaces;

public interface IUserHandler
{
    public Task<User> GetUserByIdAsync(Guid userId);
    public Task<User> GetUserByNameAsync(string userName);
    public bool VerifyUserId(Guid userId, HttpContext context);
    public bool VerifyActorId(Guid actorId, HttpContext context);
}