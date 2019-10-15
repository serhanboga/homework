using homework.webspa.ViewModel;
using Newtonsoft.Json;
using SolrNet.Attributes;
using System.Collections.Generic;

namespace web.Model
{  
    public class Building: ISearchResult
    {
        // shortCut^7.0 name^9.0 description^5

        [SolrUniqueKey("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        // W=7        
        [SolrField("shortCut")]
        [JsonProperty("shortCut")]
        public string ShortCut { get; set; }

        // W=9
        [SolrField("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        // W=5
        [SolrField("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [SolrField("score")]
        public float Score { get; set; }
    }
}
