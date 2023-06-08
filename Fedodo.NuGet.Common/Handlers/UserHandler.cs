using CommonExtensions;
using Fedodo.NuGet.Common.Constants;
using Fedodo.NuGet.Common.Interfaces;
using Fedodo.NuGet.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Fedodo.NuGet.Common.Handlers;

public class UserHandler : IUserHandler
{
    private readonly ILogger<UserHandler> _logger;
    private readonly IMongoDbRepository _repository;

    public UserHandler(ILogger<UserHandler> logger, IMongoDbRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var filterUserDefinitionBuilder = Builders<User>.Filter;
        var filterUser = filterUserDefinitionBuilder.Eq(i => i.Id, userId);
        var user = await _repository.GetSpecificItem(filterUser, DatabaseLocations.Users.Database,
            DatabaseLocations.Users.Collection);
        return user;
    }    
    
    public async Task UpdateUserAsync(User user)
    {
        var filterUserDefinitionBuilder = Builders<User>.Filter;
        var filterUser = filterUserDefinitionBuilder.Eq(i => i.Id, user.Id);
        await _repository.Update(user, filterUser, DatabaseLocations.Users.Database,
            DatabaseLocations.Users.Collection);
    }

    public async Task<User> GetUserByNameAsync(string userName)
    {
        var filterUserDefinitionBuilder = Builders<User>.Filter;
        var filterUser = filterUserDefinitionBuilder.Eq(i => i.UserName, userName);
        var user = await _repository.GetSpecificItem(filterUser, DatabaseLocations.Users.Database,
            DatabaseLocations.Users.Collection);
        return user;
    }

    public bool VerifyUserId(Guid userId, HttpContext context)
    {
        var activeUserClaims = context.User.Claims.ToList();
        var tokenUserId = activeUserClaims.Where(i =>
                i.ValueType.IsNotNull() &&
                i.Type is "sub" or "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            ?.FirstOrDefault();

        if (tokenUserId.IsNull())
        {
            _logger.LogWarning($"No {nameof(tokenUserId)} found for {nameof(userId)}:\"{userId}\"");
            return false;
        }

        if (tokenUserId.Value.ToLower() == userId.ToString()) return true;

        _logger.LogWarning($"Someone tried to be {userId} but was authorized as {tokenUserId}");
        return false;
    }    
    
    public bool VerifyActorId(Guid actorId, HttpContext context)
    {
        var activeUserClaims = context.User.Claims.ToList();
        var tokenUserId = activeUserClaims.Where(i =>
            i.ValueType.IsNotNull() && i.Type is "actorIds")?.FirstOrDefault();

        if (tokenUserId.IsNull())
        {
            _logger.LogWarning($"No {nameof(tokenUserId)} found for {nameof(actorId)}:\"{actorId}\"");
            return false;
        }

        var actorIds = tokenUserId.Value.Split(";");

        if (actorIds.Any(item => item.ToLower() == actorId.ToString()))
        {
            return true;
        }
        
        _logger.LogWarning($"Someone tried to post as {actorId} but was authorized as {tokenUserId}");
        return false;
    }
}