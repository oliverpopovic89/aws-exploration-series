namespace ConnectionInfoApi.Models;

public class ConnectionDetailsResponse
{
    public string? ClientIpAddress { get; set; }
    public int? ClientPort { get; set; }
    public string? ServerIpAddress { get; set; }
    public int? ServerPort { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
}