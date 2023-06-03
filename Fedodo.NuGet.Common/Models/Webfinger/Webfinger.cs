using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace Fedodo.NuGet.Common.Models.Webfinger;

public class Webfinger
{
    [JsonIgnore] public ObjectId Id { get; set; } // Needed for storing in MongoDB

    public string? Subject { get; set; }
    public IEnumerable<WebLink>? Links { get; set; }
}