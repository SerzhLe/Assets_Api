namespace ApplicantAssets.Domain.Models;

/// <summary>
/// A model of car entity.
/// </summary>
public class CarModel
{
    /// <summary>
    /// Gets or sets the brand.
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Gets or sets the year of manufacture.
    /// </summary>
    public DateTime ManufactureYear { get; set; }
}
