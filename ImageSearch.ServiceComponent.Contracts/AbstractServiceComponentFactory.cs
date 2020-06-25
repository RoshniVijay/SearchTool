
using ImageSearch.Common;

namespace ImageSearch.ServiceComponent.Contracts
{
    public interface AbstractServiceComponentFactory
    {
        IServiceComponent CreateSingleton(DataSources dataSource);
    }
}
