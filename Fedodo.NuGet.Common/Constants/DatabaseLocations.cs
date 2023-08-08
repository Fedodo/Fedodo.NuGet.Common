using Fedodo.NuGet.Common.Models;

namespace Fedodo.NuGet.Common.Constants;

public static class DatabaseLocations
{
    public static DatabaseCollectionPair KnownSharedInbox { get; } = new()
    {
        Database = "Others",
        Collection = "SharedInboxes"
    };

    public static DatabaseCollectionPair Actors { get; } = new()
    {
        Database = "Account",
        Collection = "Actors"
    };
    
    public static DatabaseCollectionPair ActorSecrets { get; } = new()
    {
        Database = "Account",
        Collection = "ActorSecrets"
    };

    public static DatabaseCollectionPair Webfinger { get; } = new()
    {
        Database = "Account",
        Collection = "Webfingers"
    };

    public static DatabaseCollectionPair Users { get; } = new()
    {
        Database = "Account",
        Collection = "Users"
    };    
    
    public static DatabaseCollectionPair Activity { get; } = new()
    {
        Database = "Activity"
    };
}