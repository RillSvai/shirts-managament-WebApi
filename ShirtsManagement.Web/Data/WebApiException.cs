using System.Text.Json;

namespace ShirtsManagement.Web.Data;

public class WebApiException : Exception
{
    public ErrorResponse? ErrorResponse { get; set; }

    public WebApiException(string errorJson)
    {
        ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
    }
}