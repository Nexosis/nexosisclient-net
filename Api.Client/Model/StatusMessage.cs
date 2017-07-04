namespace Nexosis.Api.Client.Model
{
    public class StatusMessage
    {
        public MessageSeverity Severity { get; set; } = MessageSeverity.Informational;
        public string Message { get; set; }
    }
}