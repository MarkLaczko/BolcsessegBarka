using Newtonsoft.Json;

namespace ApiLibrary;

public class UserMemberPivot
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("is_admin")]
    public bool IsAdmin { get; set; }
    
    [JsonProperty("member")]
    public Member Member { get; set; }
}