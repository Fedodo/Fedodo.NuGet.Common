using MongoDB.Driver;

namespace Fedodo.NuGet.Common.Interfaces;

public interface IMongoDbRepository
{
    public Task Create<T>(T item, string databaseName, string collectionName);
    public Task<IEnumerable<T>> GetAll<T>(string databaseName, string collectionName);
    public Task<T> GetSpecificItem<T>(FilterDefinition<T> filter, string databaseName, string collectionName);

    public Task<IEnumerable<T>> GetSpecificItems<T>(FilterDefinition<T> filter, string databaseName,
        string collectionName);

    public Task Update<T>(T item, FilterDefinition<T> filter, string databaseName, string collectionName);
    public Task Delete<T>(FilterDefinition<T> filter, string databaseName, string collectionName);
    public Task<long> CountAll<T>(string databaseName, string collectionName);

    public Task<IEnumerable<T>> GetAllPaged<T>(string databaseName, string collectionName, int pageId, int pageSize,
        SortDefinition<T> sortDefinition);

    public Task<IEnumerable<T>> GetSpecificPaged<T>(string databaseName, string collectionName, int pageId,
        int pageSize, SortDefinition<T> sortDefinition, FilterDefinition<T> filter);

    public Task<long> CountSpecific<T>(string databaseName, string collectionName, FilterDefinition<T> filter);

    public Task<IEnumerable<T>> GetAllPagedFromCollections<T>(string databaseName, string collectionName, int pageId,
        int pageSize, SortDefinition<T> sortDefinition, string foreignCollectionName);

    public Task<long> CountSpecificFromCollections<T>(string databaseName, IEnumerable<string> collectionNames,
        FilterDefinition<T> filter);

    public Task<IEnumerable<T>> GetSpecificPagedFromCollections<T>(string databaseName,
        IEnumerable<string> collectionNames, int pageId, int pageSize, SortDefinition<T> sortDefinition,
        FilterDefinition<T> filter);

    public IEnumerable<string> GetCollectionNames(string databaseName);

    public Task<T> GetSpecificItemFromCollections<T>(FilterDefinition<T> filter, string databaseName,
        IEnumerable<string> collectionNames);
}