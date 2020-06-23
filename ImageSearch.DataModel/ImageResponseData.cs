using ImageSearch.DataModel.Contracts;
using System.Collections.Generic;

namespace ImageSearch.DataModel
{
    public class ImageResponseData : IResponseContext
    {
        public List<string> ImageThumbnail { get; set; }
        
    }
}
