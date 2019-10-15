
using CommonServiceLocator;
using homework.webspa.ViewModel;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.Impl;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web.Model;

namespace homework.webspa.Services
{
    public class SolrService : ISearchService
    {
        private ISolrOperations<Building> _buildings;
        private ISolrOperations<Lock> _locks;
        private ISolrOperations<Group> _groups;
        private ISolrOperations<Medium> _medium;

        // TODO: too many injection
        public SolrService(ISolrOperations<Building> buildings, ISolrOperations<Lock> locks, ISolrOperations<Group> groups, ISolrOperations<Medium> medium)
        {
            _buildings = buildings;
            _locks = locks;
            _groups = groups;
            _medium = medium;
        }
        // All query weights are sent as qf boost parameter
        public IEnumerable<Building> SearchBuildings(string text)
        {
            var extraParams = new List<KeyValuePair<string, string>>();

            extraParams.Add(new KeyValuePair<string, string>("defType", "edismax"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "shortCut^7"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "name^9"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "description^5"));

            var options = new QueryOptions();

            options.ExtraParams = extraParams;
            options.Fields = new[] { "id", "shortCut", "name", "description", "score" };


            var result = _buildings.Query(new SolrQuery(text), options);

            return result;
        }

        public IEnumerable<Lock> SearchLocks(string text)
        {
            var extraParams = new List<KeyValuePair<string, string>>();

            // All query weights are sent as qf boost parameter
            extraParams.Add(new KeyValuePair<string, string>("defType", "edismax"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "buildingShortCut^5"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "buildingName^8"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "buildingDescription^0"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "type^3"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "name^10"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "serialNumber^8"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "floor^6"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "roomNumber^6"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "description^6"));

            var options = new QueryOptions();

            options.ExtraParams = extraParams;
            options.Fields = new[] { "id", "buildingId", "buildingshortCut", "buildingName", "buildingDescription", "type", "name", "serialNumber", "floor", "roomNumber", "description", "score" };

            var result = _locks.Query(new SolrQuery(text), options);
            return result;
        }

        public IEnumerable<Group> SearchGroups(string text)
        {
            var extraParams = new List<KeyValuePair<string, string>>();

            extraParams.Add(new KeyValuePair<string, string>("defType", "edismax"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "name^9"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "description^5"));

            var options = new QueryOptions();

            options.ExtraParams = extraParams;
            options.Fields = new[] { "id", "name", "description", "score" };

            var result = _groups.Query(new SolrQuery(text), options);

            return result;
        }

        public IEnumerable<Medium> SearchMedia(string text)
        {
            var extraParams = new List<KeyValuePair<string, string>>();

            extraParams.Add(new KeyValuePair<string, string>("defType", "edismax"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "groupName^8"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "groupDescription^0"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "type^3"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "owner^10"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "serialNumber^8"));
            extraParams.Add(new KeyValuePair<string, string>("qf", "description^6"));

            var options = new QueryOptions();

            options.ExtraParams = extraParams;
            options.Fields = new[] { "id", "groupId", "groupName", "groupDescription", "type", "owner", "serialNumber", "description", "score" };

            var result = _medium.Query(new SolrQuery(text), options);
            return result;
        }

        public IEnumerable<SearchResult> Search(string text)
        {
            var buildings = SearchBuildings(text);
            var locks = SearchLocks(text);
            var groups = SearchGroups(text);
            var media = SearchMedia(text);

            var result = new List<SearchResult>();

            // TODO: REFACTOR
            // This is bad code
            #region Refactor
            foreach (var item in buildings)
            {
                var searchResult = new SearchResult();

                searchResult.Type = SearchResultType.Building;
                searchResult.TotalWeight = item.Score;
                searchResult.Body = (ISearchResult)item;
                result.Add(searchResult);
            }

            foreach (var item in locks)
            {
                var searchResult = new SearchResult();

                searchResult.Type = SearchResultType.Lock;
                searchResult.TotalWeight = item.Score;
                searchResult.Body = (ISearchResult)item;
                result.Add(searchResult);
            }

            foreach (var item in groups)
            {
                var searchResult = new SearchResult();

                searchResult.Type = SearchResultType.Group;
                searchResult.TotalWeight = item.Score;
                searchResult.Body = (ISearchResult)item;
                result.Add(searchResult);
            }

            foreach (var item in media)
            {
                var searchResult = new SearchResult();

                searchResult.Type = SearchResultType.Medium;
                searchResult.TotalWeight = item.Score;
                searchResult.Body = (ISearchResult)item;
                result.Add(searchResult);
            }
            #endregion

            return result;
        }


       


    }
}
