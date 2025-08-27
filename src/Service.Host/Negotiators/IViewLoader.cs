namespace Linn.Portal.Authorization.Service.Host.Negotiators
{
    public interface IViewLoader
    {
        string Load(string viewName);
    }
}
