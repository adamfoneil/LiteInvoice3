using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;

namespace WebApp;

public class AuthorizationMessageHandler(IAccessTokenProvider accessTokenProvider) : DelegatingHandler
{
    private readonly IAccessTokenProvider _accessTokenProvider = accessTokenProvider;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
		var tokenResult = await _accessTokenProvider.RequestAccessToken();

		if (tokenResult.TryGetToken(out var token))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
		}

		return await base.SendAsync(request, cancellationToken);
    }
}