namespace Application.IntegrationEvents
{
    public class ReportRequestedEvent
    {
        public Guid RequestId { get; set; }
        public string UserEmail { get; set; }
        public string ReportType { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
