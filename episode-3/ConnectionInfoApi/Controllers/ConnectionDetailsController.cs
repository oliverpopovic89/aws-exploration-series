using Microsoft.AspNetCore.Mvc;
using ConnectionInfoApi.Models;

namespace ConnectionInfoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectionDetailsController : ControllerBase
{
    private readonly ILogger<ConnectionDetailsController> _logger;

    public ConnectionDetailsController(ILogger<ConnectionDetailsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<ConnectionDetailsResponse> Get()
    {
        try
        {
            var clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var clientPort = HttpContext.Connection.RemotePort;
            var serverIpAddress = HttpContext.Connection.LocalIpAddress?.ToString();
            var serverPort = HttpContext.Connection.LocalPort;

            var headers = HttpContext.Request.Headers.ToDictionary(k => k.Key, v => v.Value.ToString());

            var response = new ConnectionDetailsResponse
            {
                ClientIpAddress = clientIpAddress,
                ClientPort = clientPort > 0 ? clientPort : (int?)null,
                ServerIpAddress = serverIpAddress,
                ServerPort = serverPort > 0 ? serverPort : (int?)null,
                Headers = headers
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}. StackTrace: {StackTrace}", ex.Message, ex.StackTrace);

            Exception? innerEx = ex.InnerException;
            while (innerEx != null)
            {
                _logger.LogError(innerEx, "Inner exception: {Message}. StackTrace: {StackTrace}", innerEx.Message, innerEx.StackTrace);
                innerEx = innerEx.InnerException;
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred." });
        }
    }
}