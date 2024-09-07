namespace Cmd.Application.Models.User.Queries.Common
{
    public class OtpViewModel
    {
        public string  Chanel { get; set; }
        public string TrackingCode { get; set; } = "";
        public int WaitingTime { get; set; }
    }
}
