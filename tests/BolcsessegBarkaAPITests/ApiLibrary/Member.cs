using Newtonsoft.Json;

namespace ApiLibrary;

public class Member
{
    [JsonProperty("group_id")]
    public int GroupId { get; set; }
    
    [JsonProperty("user_id")]
    public int UserId { get; set; }

    [JsonProperty("permission")]
    public string Permission { get; set; }
}