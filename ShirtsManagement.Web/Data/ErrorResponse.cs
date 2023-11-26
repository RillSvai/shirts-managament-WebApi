using System.Text.Json.Serialization;

namespace ShirtsManagement.Web.Data;

public class ErrorResponse
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("status")]
    public int Status { get; set; }
    [JsonPropertyName("error")]
    public Dictionary<string, List<string>>? Errors { get; set; }
}