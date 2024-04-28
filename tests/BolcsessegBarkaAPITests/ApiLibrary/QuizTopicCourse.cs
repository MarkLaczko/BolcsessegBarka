using Newtonsoft.Json;

namespace ApiLibrary;

public class QuizTopicCourse
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("max_attempts")]
    public int? MaxAttempts { get; set; }
    
    [JsonProperty("opens")]
    public int? Opens { get; set; }
    
    [JsonProperty("closes")]
    public int? Closes { get; set; }
    
    [JsonProperty("time")]
    public int? Time { get; set; }
    
    [JsonProperty("number_of_tasks")]
    public int NumberOfTasks { get; set; }
    
    [JsonProperty("topic")]
    public TopicQuiz Topic { get; set; }
}