namespace homework.webspa.ViewModel
{
    public class SearchResult
    {
        // TODO: not sure using enum 
        public SearchResultType Type { get; set; }
        public ISearchResult Body { get; set; }
        public float TotalWeight { get; set; }
    }
}
