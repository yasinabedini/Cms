namespace Cms.Endpoints.Site.Proxy.FinalView
{
    public class Root
    {
        public FinalView.Data data { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string messages { get; set; }
        public object errors { get; set; }
    }
}
