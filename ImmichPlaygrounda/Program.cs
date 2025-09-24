// kiota generate -l CSharp -c ImmichClient -n Immich.Client -d ./openapi.json -o ./Client

using ImmichPlaygrounda.Client;
using Microsoft.Kiota.Http.HttpClientLibrary;

Console.WriteLine("Hello, World!");

// Configure your Immich server URL here
var baseUrl = "http://192.168.1.23:2283/api"; // Replace with your actual Immich server URL

var authProvider = new ApiKeyProvider("REDACTED", "x-api-key");
var adapter = new HttpClientRequestAdapter(authProvider)
{
    BaseUrl = baseUrl,
};

Immich.Client.ImmichClient client = new Immich.Client.ImmichClient(adapter);
var result = await client.Duplicates.GetAsync();
Console.WriteLine(result);