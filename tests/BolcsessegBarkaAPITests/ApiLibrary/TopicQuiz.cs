using Newtonsoft.Json;

namespace ApiLibrary;

public class TopicQuiz
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("order")]
    public int Order { get; set; }
    
    [JsonProperty("course")]
    public Course Course { get; set; }
}