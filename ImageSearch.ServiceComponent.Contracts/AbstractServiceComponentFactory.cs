
using ImageSearch.Common;

namespace ImageSearch.ServiceComponent.Contracts
{
    /// <summary>
    /// Abstract factory
    /// </summary>
    public interface IAbstractServiceComponentFactory
    {
        IServiceComponent CreateSingleton(DataSources dataSource);
    }
}
