using Newtonsoft.Json;

namespace ApiLibrary;

public class Course
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("image")]
    public string Image { get; set; }
    
    [JsonProperty("created_by")]
    public object CreatedBy { get; set; }
}