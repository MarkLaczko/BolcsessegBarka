using Newtonsoft.Json;

namespace ApiLibrary;

public class Note
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; } 
    
    [JsonProperty("user_id")]
    public int UserId { get; set; } 
}