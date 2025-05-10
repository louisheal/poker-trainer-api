using Microsoft.AspNetCore.Mvc;

namespace PokerTrainerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokerController : Controller
{
    [HttpGet]
    public IActionResult GetPokerHand()
    {
        return Ok(new { hand = "AKs " });
    }

    [HttpPost]
    public IActionResult PostPokerAction()
    {
        return Ok(new { result = "Action processed" });
    }
}