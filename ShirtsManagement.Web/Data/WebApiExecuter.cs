namespace ShirtsManagement.Web.Data;

public class WebApiExecuter : IWebApiExecuter
{
    private const string ApiName = "web-api";
    private readonly IHttpClientFactory _clientFactory;

    public WebApiExecuter(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T?> InvokeGet<T>(string relativeUrl)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
        HttpResponseMessage response = await httpClient.SendAsync(request);
        await HandlePotentialError(response);
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(relativeUrl,obj);
        await HandlePotentialError(response);
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task InvokePut<T>(string relativeUrl, T obj)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
        await HandlePotentialError(response);
    }

    public async Task InvokeDelete(string relativeUrl)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
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
}
