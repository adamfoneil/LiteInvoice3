﻿using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace ApiClientBaseLibrary;

public abstract class ApiClientBase(IHttpClientFactory httpClientFactory, ILogger<ApiClientBase> logger)
{
	protected readonly ILogger<ApiClientBase> Logger = logger;

	protected virtual HttpClient CreateClient(IHttpClientFactory httpClientFactory) => httpClientFactory.CreateClient();

	protected HttpClient Client => CreateClient(httpClientFactory);

	protected abstract Task<bool> HandleException(HttpResponseMessage? response, Exception exception, [CallerMemberName]string? methodName = null);

	protected async Task<T?> GetAsync<T>(string uri)
	{
		var response = await Client.GetAsync(uri);

		try
		{
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<T>();
		}
		catch (Exception exc)
		{
			Logger.LogError(exc, "Error in {Method}", nameof(GetAsync));
			if (!await HandleException(response, exc))
			{
				throw;
			}
			return default;
		}		
	}

	protected async Task<TResult?> PostWithResultAsync<TResult>(string uri)
	{
		var response = await Client.PostAsync(uri, null);

		try
		{
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<TResult>();
		}
		catch (Exception exc)
		{
			Logger.LogError(exc, "Error in {Method}", nameof(GetAsync));
			if (!await HandleException(response, exc))
			{
				throw;
			}
		}

		return default;
	}

	protected async Task PostAsync<T>(string uri, T value)
	{
		var response = await Client.PostAsJsonAsync(uri, value);

		try
		{
			response.EnsureSuccessStatusCode();
		}
		catch (Exception exc)
		{
			Logger.LogError(exc, "Error in {Method}", nameof(GetAsync));
			if (!await HandleException(response, exc))
			{
				throw;
			}
		}		
	}
}
