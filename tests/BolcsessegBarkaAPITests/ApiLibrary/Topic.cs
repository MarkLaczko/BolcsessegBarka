using Newtonsoft.Json;

namespace ApiLibrary;

public class Topic
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("order")]
    public object Order { get; set; } 
    
    [JsonProperty("course_id")]
    public int CourseId { get; set; } 
}