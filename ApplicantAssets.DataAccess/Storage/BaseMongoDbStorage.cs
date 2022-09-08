namespace ApplicantAssets.DataAccess.Storage;

using ApplicantAssets.Domain.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

/// <summary>
/// An abstract class to provide CRUD operations on MongoDb.
/// </summary>
/// <typeparam name="TEntity"><inheritdoc/></typeparam>
/// <typeparam name="TId"><inheritdoc/></typeparam>
public abstract class BaseMongoDbStorage<TEntity, TId> : IBaseDbStorage<TEntity, TId>
    where TEntity : class
{
    private readonly MongoDbConfig configuration;
    private readonly IMongoDatabase db;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseMongoDbStorage{TEntity, TId}"/> class.
    /// </summary>
    /// <param name="configuration">MongoDb configuration.</param>
    /// <param name="collectionName">MongoDb collection to work with.</param>
    public BaseMongoDbStorage(IOptions<MongoDbConfig> configuration, string collectionName)
    {
        this.configuration = configuration!.Value;
        var client = new MongoClient(this.configuration.Connection);
        this.db = client.GetDatabase(this.configuration.Database);
        this.Collection = this.db.GetCollection<TEntity>(collectionName);
    }

    /// <summary>
    /// Gets a specific mongo collection.
    /// </summary>
    protected IMongoCollection<TEntity> Collection { get; }

    /// <inheritdoc/>
    public async Task StoreAsync(TEntity entity)
        => await this.Collection.InsertOneAsync(entity);

    /// <inheritdoc/>
    public async Task StoreRangeAsync(IEnumerable<TEntity> entities)
        => await this.Collection.InsertManyAsync(entities);

    /// <inheritdoc/>
    public async Task DeleteAsync(TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);

        _ = await this.Collection.FindOneAndDeleteAsync(filter);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync()
    {
        var result = await this.Collection.FindAsync(new BsonDocument());

        return await result.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsync(TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);

        var result = await this.Collection.FindAsync(filter);

        return await result.FirstAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity, TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);

        _ = await this.Collection.ReplaceOneAsync(filter, entity);
    }
}
