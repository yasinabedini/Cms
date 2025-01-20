using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cms.Endpoints.Site.Proxy.Asnad
{
    public class Root<T>
    {
        public T data { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string messages { get; set; }
        public object errors { get; set; }
    }
}
