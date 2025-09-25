// kiota generate -l CSharp -c ImmichClient -n Immich.Client -d ./openapi.json -o ./Client

using ImmichPlaygrounda.Client;
using Microsoft.Kiota.Http.HttpClientLibrary;

Console.WriteLine("Hello, World!");


// Configure your Immich server URL here
var baseUrl = "http://192.168.1.23:2283/api"; // Replace with your actual Immich server URL

var authProvider = new ApiKeyProvider("Redacted", "x-api-key");
var adapter = new HttpClientRequestAdapter(authProvider)
{
    BaseUrl = baseUrl,
};

Immich.Client.ImmichClient client = new Immich.Client.ImmichClient(adapter);

var results = await client.Duplicates.GetAsync();

var filtered = results.Where(r => r.Assets.Count == 2 && r.Assets.DistinctBy(a => a.OriginalFileName).Count() == 1).ToList();

foreach (var item in filtered)
{
    await client.Stacks.PostAsync(new Immich.Client.Models.StackCreateDto
    {
        AssetIds = [.. item.Assets!
        .OrderByDescending(a => a.FileModifiedAt)
        .Select(a => (Guid?)Guid.Parse(a.Id))]
    });

    await client.Duplicates.DeleteAsync(new Immich.Client.Models.BulkIdsDto { Ids = [Guid.Parse(item.DuplicateId)] });
}

Console.WriteLine($"Found {filtered.Count} duplicates and stacked them.");


//await client.Assets.PutAsync(new Immich.Client.Models.AssetBulkUpdateDto
//{
//    Ids = [Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851")],
//    DuplicateId = null,
//});
