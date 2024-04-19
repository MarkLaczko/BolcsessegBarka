using Newtonsoft.Json;

namespace ApiLibrary;

public class Group
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("users")]
    public List<UserMemberPivot> Users { get; set; }
    
    [JsonProperty("courses")]
    public List<Course> Courses { get; set; }
}