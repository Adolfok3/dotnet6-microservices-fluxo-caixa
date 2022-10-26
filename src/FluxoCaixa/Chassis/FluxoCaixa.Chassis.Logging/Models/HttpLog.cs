namespace FluxoCaixa.Chassis.Logging.Models
{
    public class HttpLog
    {
        public string Application { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public DateTime ResponseDate { get; set; }
        public int StatusCode { get; set; }
    }
}
