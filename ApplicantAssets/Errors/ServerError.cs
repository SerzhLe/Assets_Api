namespace ApplicantAssets.Api.Exceptions;

/// <summary>
/// General exception for holding necessary error information.
/// </summary>
public class ServerError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerError"/> class.
    /// </summary>
    /// <param name="message"> An exception message.</param>
    /// <param name="statusCode"> An http response status code.</param>
    public ServerError(string message, int statusCode)
    {
        this.Message = message;
        this.StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerError"/> class.
    /// </summary>
    /// <param name="message"> An exception message.</param>
    /// <param name="statusCode"> An http response status code.</param>
    /// <param name="details"> A stack trace of an exception.</param>
    public ServerError(string message, int statusCode, string details)
    {
        this.Message = message;
        this.StatusCode = statusCode;
        this.Details = details;
    }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the details.
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets the http response status code.
    /// </summary>
    public int StatusCode { get; set; }
}
