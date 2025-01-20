namespace Cms.Endpoints.Site.Proxy.Asnad
{
    public class DetailsRoot
    {
        public Details data { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string messages { get; set; }
        public object errors { get; set; }
    }
}
