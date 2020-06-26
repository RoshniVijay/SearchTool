using ImageSearch.Common;
using System.Collections.ObjectModel;

namespace ImageSearch.DataModel
{
    /// <summary>
    /// Response for NewsAPI caller
    /// </summary>
    public class TextResponseDataModel : IResponseContext
    {
        public ObservableCollection<Articles> NewsItems { get; set; }
        public string Status { get; set; }
    }
}
