using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api")]
public class PushController : ControllerBase
{
    private static List<string> tokens = new List<string>();
    private readonly FirebaseService _firebaseService;

    public PushController()
    {
        _firebaseService = new FirebaseService();
    }

    [HttpPost("save-token")]
    public IActionResult SaveToken([FromBody] TokenRequest request)
    {
        if (!tokens.Contains(request.Token))
        {
            tokens.Add(request.Token);
        }
        return Ok(new { message = "Token đã lưu" });
    }

    [HttpPost("send-notification")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        foreach (var token in tokens)
        {
            await _firebaseService.SendNotification(token, request.Title, request.Body);
        }
        return Ok(new { message = "Thông báo đã gửi" });
    }
}

public class TokenRequest
{
    public string Token { get; set; }
}

public class NotificationRequest
{
    public string Title { get; set; }
    public string Body { get; set; }
}
