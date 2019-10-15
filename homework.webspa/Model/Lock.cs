
using homework.webspa.ViewModel;
using Newtonsoft.Json;
using SolrNet.Attributes;
using System.Collections.Generic;

namespace web.Model
{
    public class Lock: ISearchResult
    {
        //building_name^8.0 building_shortCut^5.0 building_description^0.0 type^3.0 name^10.0 serialNumber^8.0 floor^6.0 roomNumber^6.0 description^6.0
        
        [SolrUniqueKey("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [SolrField("buildingId")]
        [JsonProperty("buildingId")]
        public string BuildingId { get; set; }

        // Wt(name)=8  
        [SolrField("buildingName")]
        [JsonProperty("buildingName")]
        public string BuildingName { get; set; }

        // Wt(shortCut)=5
        [SolrField("buildingShortCut")]
        [JsonProperty("buildingShortCut")]
        public string BuildingShortCut { get; set; }

        // Wt(description)=0     
        [SolrField("buildingDescription")]
        [JsonProperty("buildingDescription")]
        public string BuildingDescription { get; set; }

        // W=3
        [SolrField("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        // W=10
        [SolrField("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        // W=8
        [SolrField("serialNumber")]
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        // W=6
        [SolrField("floor")]
        [JsonProperty("floor")]
        public string Floor { get; set; }

        // W=6
        [SolrField("roomNumber")]        
        [JsonProperty("roomNumber")]
        public string RoomNumber { get; set; }

        // W=6
        [SolrField("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [SolrField("score")]
        public float Score { get; set; }
    }
}
