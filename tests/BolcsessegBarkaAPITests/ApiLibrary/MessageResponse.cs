using Newtonsoft.Json;

namespace ApiLibrary;

public class MessageResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }
}