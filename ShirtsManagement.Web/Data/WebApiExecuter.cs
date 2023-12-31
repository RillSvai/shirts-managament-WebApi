﻿using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ShirtsManagement.Web.Data;

public class WebApiExecuter : IWebApiExecuter
{
    private const string ApiName = "web-api";
    private const string AuthApi = "auth-api";
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebApiExecuter(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<T?> InvokeGet<T>(string relativeUrl)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        await AddJwtToken(httpClient);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
        HttpResponseMessage response = await httpClient.SendAsync(request);
        await HandlePotentialError(response);
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        await AddJwtToken(httpClient);
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(relativeUrl,obj);
        await HandlePotentialError(response);
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task InvokePut<T>(string relativeUrl, T obj)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        await AddJwtToken(httpClient);
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
        await HandlePotentialError(response);
    }

    public async Task InvokeDelete(string relativeUrl)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        await AddJwtToken(httpClient);
        HttpResponseMessage response = await httpClient.DeleteAsync(relativeUrl);
        await HandlePotentialError(response);
    }

    private async Task HandlePotentialError(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            string errorJson = await response.Content.ReadAsStringAsync();
            throw new WebApiException(errorJson);
        }
    }

    private async Task AddJwtToken(HttpClient httpClient) 
    {
        JwtToken? token = null;
        string? sessionToken = _httpContextAccessor.HttpContext?.Session.GetString("accessToken");
        if (sessionToken is not null) 
        {
            token = JsonConvert.DeserializeObject<JwtToken>(sessionToken);
        }
        if (sessionToken is null || token.ExpiresAt <= DateTime.UtcNow) 
        {
            string clientId = _configuration.GetValue<string>("ClientId")!;
            string secret = _configuration.GetValue<string>("Secret")!;
            HttpClient authClient = _clientFactory.CreateClient(AuthApi);
            HttpResponseMessage response = await authClient.PostAsJsonAsync("auth", new AppCredential { ClientId = clientId, Secret = secret });
            response.EnsureSuccessStatusCode();
            string tokenJson = await response.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JwtToken>(tokenJson);
            _httpContextAccessor.HttpContext?.Session.SetString("accessToken", tokenJson);
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
    }
}
