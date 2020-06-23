using System.Collections.Generic;

namespace ImageSearch.DataModel.Contracts
{
    public interface IResponseContext
    {
        List<string> ImageThumbnail { get; set; }

    }
}
