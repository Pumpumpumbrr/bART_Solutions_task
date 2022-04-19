using System.Text.Json.Serialization;

namespace bART_Solutions_task.Data.Models;

public class Account
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? IncidentName { get; set; }
    public Incident? Incident { get; set; }
    [JsonIgnore]
    public List<Contact> Contacts { get; set; } = new();
}
