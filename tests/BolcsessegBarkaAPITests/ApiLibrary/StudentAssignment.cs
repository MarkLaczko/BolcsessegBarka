using Newtonsoft.Json;

namespace ApiLibrary;

public class StudentAssignment
{
    [JsonProperty("id")]
    
    public int Id { get; set; }
    
    [JsonProperty("assignment_id")]
    
    public int AssignmentId { get; set; }
    
    [JsonProperty("student_task_name")]
    
    public string? StudentTaskName { get; set; }
    
    [JsonProperty("student_task")]
    
    public string StudentTask { get; set; }
    
    [JsonProperty("user_id")]
    
    public int? UserId { get; set; }
}