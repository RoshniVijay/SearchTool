using ImageSearch.DataModel.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearch.DataModel
{
    public class TextResponseDataModel : IResponseContext
    {
        public ObservableCollection<Articles> NewsItems { get; set; }
        public string Status { get; set; }
    }
}
