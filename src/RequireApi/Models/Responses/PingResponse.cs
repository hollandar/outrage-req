namespace RequireApi.Models.Responses
{
    public class PingResponse
    {
        public DateTimeOffset DateTime { get { return DateTimeOffset.UtcNow; } }
    }
}
