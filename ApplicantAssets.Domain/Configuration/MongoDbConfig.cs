namespace ApplicantAssets.Domain.Configuration;

/// <summary>
/// A mongo db configuration class.
/// </summary>
public class MongoDbConfig
{
    /// <summary>
    /// Gets or sets a connection string.
    /// </summary>
    public string Connection { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a database name.
    /// </summary>
    public string Database { get; set; } = string.Empty;
}
