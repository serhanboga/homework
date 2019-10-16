using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace homework.webspa.Services
{
    public class SolrInitializer
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHostingEnvironment _env;

        public SolrInitializer(IHttpClientFactory clientFactory, IHostingEnvironment env)
        {
            _clientFactory = clientFactory;
            _env = env;
        }
        // SEEDER
        public async Task Initialize()
        {
            var client = _clientFactory.CreateClient("solrapi");

            await client.GetAsync("admin/cores?action=CREATE&name=buildings&configSet=/opt/solr/server/solr/configsets/buildings");
            await client.GetAsync("admin/cores?action=CREATE&name=locks&configSet=/opt/solr/server/solr/configsets/locks");
            await client.GetAsync("admin/cores?action=CREATE&name=groups&configSet=/opt/solr/server/solr/configsets/groups");
            await client.GetAsync("admin/cores?action=CREATE&name=media&configSet=/opt/solr/server/solr/configsets/media");

            await client.PostAsync("buildings/update?commit=true", new StringContent("<delete><query>*:*</query></delete>", Encoding.UTF8, "text/xml"));
            await client.PostAsync("locks/update?commit=true", new StringContent("<delete><query>*:*</query></delete>", Encoding.UTF8, "text/xml"));
            await client.PostAsync("groups/update?commit=true", new StringContent("<delete><query>*:*</query></delete>", Encoding.UTF8, "text/xml"));
            await client.PostAsync("media/update?commit=true", new StringContent("<delete><query>*:*</query></delete>", Encoding.UTF8, "text/xml"));

            var data = FlattenJson();

            var buildings = Newtonsoft.Json.JsonConvert.SerializeObject(data.buildings);
            var locks = Newtonsoft.Json.JsonConvert.SerializeObject(data.locks);
            var groups = Newtonsoft.Json.JsonConvert.SerializeObject(data.groups);
            var media = Newtonsoft.Json.JsonConvert.SerializeObject(data.media);

            await client.PostAsync("buildings/update?commit=true", new StringContent(buildings, Encoding.UTF8, "application/json"));
            await client.PostAsync("locks/update?commit=true", new StringContent(locks, Encoding.UTF8, "application/json"));
            await client.PostAsync("groups/update?commit=true", new StringContent(groups, Encoding.UTF8, "application/json"));
            await client.PostAsync("media/update?commit=true", new StringContent(media, Encoding.UTF8, "application/json"));

        }
        Data FlattenJson()
        {
            var path = Path.Combine(_env.ContentRootPath, "Setup/solr/sv_lsm_data.json");
            string content = File.ReadAllText(path).Replace("null", "\"\""); // convert nulls to empty string TODO: rethink about joins below

            var sourceData = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>(content);

            var buildingPrefix = "building";

            var mappedLocks = sourceData.locks.Join(sourceData.buildings, locks => locks["buildingId"], building => building["id"], (locks, building) =>
            {
                locks[buildingPrefix + "ShortCut"] = building["shortCut"];
                locks[buildingPrefix + "Name"] = building["name"];
                locks[buildingPrefix + "Description"] = building["description"];
                return locks;

            }).ToList();

            var groupsPrefix = "group";

            var mappedMedia = sourceData.media.Join(sourceData.groups, media => media["groupId"], group => group["id"], (media, group) =>
            {
                media[groupsPrefix + "Name"] = group["name"];
                media[groupsPrefix + "Description"] = group["description"];
                return media;

            }).ToList();

            sourceData.locks = mappedLocks;
            sourceData.media = mappedMedia;

            return sourceData;
        }
    }

    public class Data
    {
        public List<Dictionary<string, string>> buildings { get; set; }
        public List<Dictionary<string, string>> locks { get; set; }
        public List<Dictionary<string, string>> groups { get; set; }
        public List<Dictionary<string, string>> media { get; set; }
    }
}
