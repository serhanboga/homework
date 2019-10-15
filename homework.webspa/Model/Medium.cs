
using homework.webspa.ViewModel;
using Newtonsoft.Json;
using SolrNet.Attributes;

namespace web.Model
{
    public class Medium: ISearchResult
    {
        [SolrUniqueKey("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [SolrField(FieldName = "groupId", Boost = 8.0F)]
        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        // Wt(name)=8
        [SolrField(FieldName = "groupName", Boost = 8.0F)]
        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        // Wt(description)=0
        [SolrField(FieldName = "groupDescription", Boost = 0.0F)]
        [JsonProperty("groupDescription")]
        public string GroupDescription { get; set; }

        // W=3
        [SolrField(FieldName = "type", Boost = 3.0F)]
        [JsonProperty("type")]
        public string Type { get; set; }

        // W=10
        [SolrField(FieldName = "owner", Boost = 10.0F)]
        [JsonProperty("owner")]
        public string Owner { get; set; }

        // W=8
        [SolrField(FieldName = "serialNumber", Boost = 8.0F)]
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        // W=6
        [SolrField(FieldName = "description", Boost = 6.0F)]
        [JsonProperty("description")]
        public string Description { get; set; }

        [SolrField("score")]
        public float Score { get; set; }
    }
}