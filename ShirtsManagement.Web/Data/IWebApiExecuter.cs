namespace ShirtsManagement.Web.Data;

public interface IWebApiExecuter
{
    public Task<T?> InvokeGet<T>(string relativeUrl);
    public Task<T?> InvokePost<T>(string relativeUrl, T obj);
    public Task InvokePut<T>(string relativeUrl, T obj);
    public Task InvokeDelete(string relativeUrl);
}