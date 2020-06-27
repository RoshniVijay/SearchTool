using SearchTool.Common;
using System.Collections.ObjectModel;

namespace SearchTool.DataModel
{
    /// <summary>
    /// response after parsing flicker XML response  
    /// </summary>
    public class ImageResponseDataModel : IResponseContext
    {
        public ObservableCollection<string> URI { get; set; }
        public string Status { get; set; }
    }
}
