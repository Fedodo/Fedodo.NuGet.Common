using Fedodo.NuGet.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Fedodo.NuGet.Common.Interfaces;

public interface IUserHandler
{
    public Task<User> GetUserByIdAsync(Guid userId);
    public bool VerifyUser(Guid userId, HttpContext context);
    public Task<User> GetUserByNameAsync(string userName);
}