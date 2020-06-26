
using ImageSearch.Common;

namespace ImageSearch.ServiceComponent.Contracts
{
    /// <summary>
    /// Abstract factory
    /// </summary>
    public interface IServiceComponentFactory
    {
        IServiceComponent CreateServiceComponent(DataSources dataSource);
    }
}
