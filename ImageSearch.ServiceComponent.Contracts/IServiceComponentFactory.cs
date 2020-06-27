
using SearchTool.Common;

namespace SearchTool.SearchComponent.Contracts
{
    /// <summary>
    /// Abstract factory
    /// </summary>
    public interface ISearchComponentFactory
    {
        ISearchComponent CreateSearchComponent(DataSources dataSource);
    }
}
