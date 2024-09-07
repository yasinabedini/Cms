namespace Cms.Endpoints.Site.Proxy.Asnad
{
    public class DatailsItem
    {
        public int id { get; set; }
        public string text { get; set; }
        public object value { get; set; }
    }

    public class Details
    {
        public int id { get; set; }
        public int mediaAccessType { get; set; }
        public string title { get; set; }
        public string numberOfRowsAndArchive { get; set; }
        public object accessNumber { get; set; }
        public string dateOfDocumentDescription { get; set; }
        public object actors { get; set; }
        public object author { get; set; }
        public object cameraMan { get; set; }
        public object cameraModel { get; set; }
        public object compositor { get; set; }
        public string body { get; set; }
        public string details { get; set; }
        public object director { get; set; }
        public object editor { get; set; }
        public string manufactureDate { get; set; }
        public object orator { get; set; }
        public object photographer { get; set; }
        public object producer { get; set; }
        public int rate { get; set; }
        public object studio { get; set; }
        public int countPage { get; set; }
        public object relatedMedia { get; set; }
        public object relatedMediaId { get; set; }
        public string mediaSource { get; set; }
        public int mediaSourceId { get; set; }
        public object waterMarkImage { get; set; }
        public object waterMarkImageId { get; set; }
        public object mediaStructure { get; set; }
        public object mediaStructureId { get; set; }
        public object mediaLanguage { get; set; }
        public object mediaLanguageId { get; set; }
        public object createdByUser { get; set; }
        public string tags { get; set; }
        public object eventId { get; set; }
        public string @event { get; set; }
        public int historicalPeriodId { get; set; }
        public string historicalPeriod { get; set; }
        public int typeWritingLineId { get; set; }
        public string typeWritingLine { get; set; }
        public bool isFavorite { get; set; }
        public object favoriteDescription { get; set; }
        public string attachmentGroup { get; set; }
        public int attachmentCount { get; set; }
        public bool isActive { get; set; }
        public bool commentIsActive { get; set; }
        public int clicks { get; set; }
        public string imageUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public object waterMarkImageUrl { get; set; }
        public object personnel { get; set; }
        public List<DatailsItem> personnels { get; set; }
        public object personnelIDs { get; set; }
        public List<DatailsItem> locations { get; set; }
        public List<DatailsItem> thematicCategories { get; set; }
        public List<DatailsItem> appendicesMedia { get; set; }
        public List<DatailsItem> mediaTypes { get; set; }
        public List<DatailsItem> meidaFields { get; set; }
        public object lastModifiedOnDate { get; set; }
        public string createdOnDate { get; set; }
    }

}
