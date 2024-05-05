using Newtonsoft.Json;

namespace ApiLibrary;

public class Assignment
{
    [JsonProperty("id")]
    
    public int Id { get; set; }
    
    [JsonProperty("task_name")]
    
    public string TaskName { get; set; }
    
    [JsonProperty("comment")]
    
    public string? Comment { get; set; }
    
    [JsonProperty("deadline")]
    
    public DateTime Deadline { get; set; }
    
    [JsonProperty("courseable_id")]
    
    public int? Courseable_id { get; set; }
    
    [JsonProperty("topic_id")]
    
    public int? Topic_id { get; set; }
}