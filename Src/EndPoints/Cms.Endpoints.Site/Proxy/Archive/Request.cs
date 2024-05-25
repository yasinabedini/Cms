using Microsoft.AspNet.Identity;

namespace Cms.Endpoints.Site.Proxy.Archive
{
    public class Request
    {
        public int Id { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; } = 0;
        public string Key { get; set; } = "title";
        public string Value { get; set; } = "";
        public string Operator { get; set; } = "OR";

        public Request(int limit, int offset, string key, string value, string @operator)
        {
            Limit = limit;
            Offset = offset;
            Key = key;
            Value = value;
            Operator = @operator;
        }
    }
}
