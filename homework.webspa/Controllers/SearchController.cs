using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework.webspa.Services;
using homework.webspa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace homework.webspa.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        ISearchService _solr;

        public SearchController(ISearchService solr)
        {
            _solr = solr;
        }

        [HttpGet("[action]")]
        public IEnumerable<SearchResult> WithText(string text)
        {
            #region dummy
            //var result = new List<SearchResult>()
            //{
            //    {
            //        new SearchResult()
            //        {
            //            Type = SearchResultType.Building,
            //            TotalWeight = "4.653392",
            //            Body = new BuildingViewModel()
            //            {
            //                Id = "0cccab2b-bc8d-44c5-b248-8a9ca6d7e899",
            //                ShortCut ="HOFF",
            //                Description = "Head Office, Feringastraße 4, 85774 Unterföhring",
            //                Name = "Head Office"
            //            }
            //        }
            //    },
            //     {
            //        new SearchResult()
            //        {
            //            Type = SearchResultType.Lock,
            //            TotalWeight = "0.18878584",
            //            Body = new LockViewModel()
            //            {
            //                Id ="e657a28e-d744-4f62-b5d8-a64123c2400f",
            //                BuildingId="0cccab2b-bc8d-44c5-b248-8a9ca6d7e899",
            //                BuildingShortCut="HOFF",
            //                BuildingName ="Head Office",
            //                BuildingDescription ="Head Office, Feringastraße 4, 85774 Unterföhring",
            //                Type ="Cylinder",
            //                Name="WC Herren 3.OG süd",
            //                SerialNumber ="UID-C043133A",
            //                Floor ="3.OG",
            //                RoomNumber ="WC.HL"
            //            }
            //        }
            //    },
            //     {
            //        new SearchResult()
            //        {
            //            Type = SearchResultType.Lock,
            //            TotalWeight = "0.18878584",
            //            Body = new LockViewModel()
            //            {
            //                 Id ="e657a28e-d744-4f62-b5d8-a62333c2400f",
            //                BuildingId="0cccab2b-bc8d-44c5-b248-8a9c11a6d7e899",
            //                BuildingShortCut="HOFF",
            //                BuildingName ="Head Office",
            //                BuildingDescription ="Head Office, Feringastraße 4, 85774 Unterföhring",
            //                Type ="Cylinder",
            //                Name="Gästezimmer 4.OG",
            //                SerialNumber ="UID-A89F98F3",
            //                Floor ="4.OG",
            //                RoomNumber ="454"
            //            }
            //        }
            //    },
            //     {
            //        new SearchResult()
            //        {
            //            Type = SearchResultType.Lock,
            //            TotalWeight = "0.18878584",
            //            Body = new LockViewModel()
            //            {
            //                 Id ="e657a28e-d332144-4f62-b5d8-a64123c2400f",
            //                BuildingId="0cccab2b-bc8d123-44c5-b248-8a9ca6d7e899",
            //                BuildingShortCut="HOFF",
            //                BuildingName ="Head Office",
            //                BuildingDescription ="Head Office, Feringastraße 4, 85774 Unterföhring",
            //                Type ="Cylinder",
            //                Name="WC Damen 3.OG süd",
            //                SerialNumber ="UID-F40C9966",
            //                Floor ="3.OG",
            //                RoomNumber ="WC.DL"
            //            }
            //        }
            //    },
            //    {
            //        new SearchResult()
            //        {
            //            Type = SearchResultType.Group,
            //            TotalWeight = "0.38878584",
            //            Body = new GroupViewModel()
            //            {
            //                Id ="70c886f5-b74e-49f4-8d9f-cf2d6645d4d6",
            //                Name = "<default>",
            //                Description = "Default group where all transponders"
            //            }
            //        }
            //    },
            //    {
            //        new SearchResult()
            //        {
            //            Type = SearchResultType.Medium,
            //            TotalWeight = "0.018878584",
            //            Body = new MediumViewModel()
            //            {
            //                Id ="2c2ec98f-137a-4c49-a47a-814ae18a3364",
            //                GroupId = "70c886f5-b74e-49f4-8d9f-cf2d6645d4d6",
            //                GroupDescription = "Default group where all transponders",
            //                GroupName = "<default>",
            //                Owner = "Gast 1",
            //                SerialNumber ="UID-378D17F6",
            //                Type ="Card"

            //            }
            //        }
            //    }
            //};
            #endregion

            // TODO: Exception handling!
            IEnumerable<SearchResult> result = new List<SearchResult>();

            if (text != null && text.Length > 2)
            {
                result = _solr.Search(text);
            }

            return result;
        }
    }
}
 