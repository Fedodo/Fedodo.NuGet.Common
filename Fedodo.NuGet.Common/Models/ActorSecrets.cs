namespace Fedodo.NuGet.Common.Models;

public class ActorSecrets
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? PrivateKeyActivityPub { get; set; }
}