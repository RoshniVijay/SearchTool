using ImageSearch.DataModel.Contracts;
using System;
using System.Collections.Generic;

namespace ImageSearch.DataModel
{
    /// <summary>
    /// Response after parsing news API
    /// </summary>
    public class NewsApiResponse
    {
        public List<Articles> Articles { get; set; }
    }

    /// <summary>
    /// Article is the json representatoion of newsapi reponse
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
