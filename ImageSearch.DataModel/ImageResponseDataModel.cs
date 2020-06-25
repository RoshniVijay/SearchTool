using ImageSearch.DataModel.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearch.DataModel
{
    public class ImageResponseDataModel : IResponseContext
    {
        public ObservableCollection<string> URI { get; set; }
        public string Status { get; set; }
    }
}
