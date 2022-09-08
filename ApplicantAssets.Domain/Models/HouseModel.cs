namespace ApplicantAssets.Domain.Models;

/// <summary>
/// A model of house entity.
/// </summary>
public class HouseModel
{
    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current price.
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Gets or sets the year of construction.
    /// </summary>
    public DateTime ConstructionYear { get; set; }

    /// <summary>
    /// Gets or sets the total number of rooms.
    /// </summary>
    public int RoomsNumber { get; set; }
}