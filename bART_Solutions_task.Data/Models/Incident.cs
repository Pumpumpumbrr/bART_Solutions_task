using System.Text.Json.Serialization;

namespace bART_Solutions_task.Data.Models;

public class Incident
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public List<Account> Accounts { get; set; } = new();
}

