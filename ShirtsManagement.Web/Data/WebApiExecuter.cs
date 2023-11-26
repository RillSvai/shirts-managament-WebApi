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
        return await httpClient.GetFromJsonAsync<T>(relativeUrl);

    }

    public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
    {
        HttpClient httpClient = _clientFactory.CreateClient(ApiName);
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(relativeUrl,obj);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}
