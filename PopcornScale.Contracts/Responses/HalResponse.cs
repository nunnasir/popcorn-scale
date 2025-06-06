using System.Text.Json.Serialization;

namespace PopcornScale.Contracts.Responses;

public class HalResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Link> Links { get; set; } = [];
}

public class Link
{
    public required string Href { get; init; }
    public required string Rel { get; init; }
    public required string Type { get; init; }
}
