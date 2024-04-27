using Newtonsoft.Json;

namespace ApiLibrary;

public class Task
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("order")]
    public int Order { get; set; }
    
    [JsonProperty("header")]
    public string Header { get; set; }
    
    [JsonProperty("text")]
    public string Text { get; set; }
    
    [JsonProperty("subtasks")]
    public List<Subtask> Subtasks { get; set; }
}