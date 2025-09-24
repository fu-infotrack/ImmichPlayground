using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace ImmichPlaygrounda.Client;

public class ApiKeyProvider : IAuthenticationProvider
{
    private readonly string ApiKey;

    private readonly string ParameterName;

    public ApiKeyProvider(string apiKey, string parameterName)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException("apiKey");
        }

        if (string.IsNullOrEmpty(parameterName))
        {
            throw new ArgumentNullException("parameterName");
        }

        ApiKey = apiKey;
        ParameterName = parameterName;
    }

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        request.Headers.Add(ParameterName, ApiKey);
        return Task.CompletedTask;
    }
}