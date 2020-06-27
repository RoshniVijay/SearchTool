using System.Collections.Generic;

namespace SearchTool.DataModel
{
    /// <summary>
    /// Response after parsing news API
    /// </summary>
    public class NewsApiResponse
    {
        public List<Articles> Articles { get; set; }
    }
}
