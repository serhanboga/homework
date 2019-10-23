using homework.webspa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Model;

namespace homework.webspa.Services
{
    public interface ISearchService
    {
        IEnumerable<Building> SearchBuildings(string text);
        IEnumerable<Lock> SearchLocks(string text);
        IEnumerable<Group> SearchGroups(string text);
        IEnumerable<Medium> SearchMedia(string text);
        IEnumerable<SearchResult> Search(string text);
        
    }
}
