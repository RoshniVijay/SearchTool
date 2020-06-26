﻿using ImageSearch.DataModel.Contracts;
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
}
