namespace homework.webspa.ViewModel
{
    public class LockViewModel : ISearchResult
    {
        public string Id { get; set; }
        public string BuildingId { get; set; }
        public string BuildingShortCut { get; set; }
        public string BuildingName { get; set; }
        public string BuildingDescription { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Floor { get; set; }
        public string RoomNumber { get; set; }
    }
}
