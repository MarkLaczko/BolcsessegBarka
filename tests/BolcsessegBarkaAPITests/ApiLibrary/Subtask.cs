using Newtonsoft.Json;

namespace ApiLibrary;

public class Subtask
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("order")]
    public int Order { get; set; }
    
    [JsonProperty("question")]
    public string Question { get; set; }
    
    [JsonProperty("options")]
    public List<string>? Options { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("marks")]
    public double Marks { get; set; }
}