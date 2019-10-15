using homework.webspa.ViewModel;
using Newtonsoft.Json;
using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace web.Model
{
    public class Group: ISearchResult
    {
        [SolrUniqueKey("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        // W=9
        [SolrField(FieldName ="name", Boost =9.0F)]
        [JsonProperty("name")]
        public string Name { get; set; }

        // W=5
        [SolrField(FieldName = "description", Boost = 5.0F)]
        [JsonProperty("description")]
        public string Description { get; set; }

        [SolrField("score")]
        public float Score { get; set; }
    }
}
