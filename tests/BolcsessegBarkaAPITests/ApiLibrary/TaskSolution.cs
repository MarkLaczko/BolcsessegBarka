using Newtonsoft.Json;

namespace ApiLibrary;

public class TaskSolution
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("solution")]
    public List<string>? Solution { get; set; }
}