using Microsoft.AspNetCore.Mvc;

namespace MusicStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Authorization: ControllerBase
{
    [HttpPost("Register")]
    public ActionResult Post()
    {
        return Ok("Registered");
    }
}