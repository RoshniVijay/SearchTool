using System;

namespace SearchTool.DataModel
{
    /// <summary>
    /// Article is the json representation of newsapi response. Used by Newtonsoft.json dll
    /// </summary>
    public class Articles
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}