using ImageSearch.DataModel.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearch.DataModel
{
    /// <summary>
    /// response after parsing flicker XML response  
    /// </summary>
    public class ImageResponseDataModel : IResponseContext
    {
        public ObservableCollection<string> URI { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// Currently the URL
    /// </summary>
    public class ThumbnailData
    {
        public string URI { get; set; }
    }

    /// <summary>
    /// Size at which image has to be displayed. selected by user
    /// </summary>
    public class Size
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

}
