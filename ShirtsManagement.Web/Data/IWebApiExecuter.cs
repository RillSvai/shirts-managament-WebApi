namespace ShirtsManagement.Web.Data;

public interface IWebApiExecuter
{
    public Task<T?> InvokeGet<T>(string relativeUrl);
}