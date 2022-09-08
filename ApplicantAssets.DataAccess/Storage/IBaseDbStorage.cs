namespace ApplicantAssets.DataAccess.Storage;

/// <summary>
/// A basic interface storage with CRUD operations.
/// </summary>
/// <typeparam name="TEntity">A type of entity model.</typeparam>
/// <typeparam name="TId">A type of model's id.</typeparam>
public interface IBaseDbStorage<TEntity, TId>
    where TEntity : class
{
    /// <summary>
    /// Gets all available entities.
    /// </summary>
    /// <returns>The collection of entities.</returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets an entity by its id.
    /// </summary>
    /// <param name="id">An entity id.</param>
    /// <returns>The desired entity.</returns>
    Task<TEntity> GetByIdAsync(TId id);

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">An entity model.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task StoreAsync(TEntity entity);

    /// <summary>
    /// Creates a list of entities at a time.
    /// </summary>
    /// <param name="entities">A list of entities.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task StoreRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">An entity model.</param>
    /// <param name="id">An entity id.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(TEntity entity, TId id);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="id">An id of an entity to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(TId id);
}
