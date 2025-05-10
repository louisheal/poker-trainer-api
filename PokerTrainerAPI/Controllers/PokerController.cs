using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PokerTrainerAPI.Services;

namespace PokerTrainerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokerController : Controller
{
    private readonly IPokerService _service;
    private readonly IMemoryCache _cache;
    
    public PokerController(
        IPokerService service,
        IMemoryCache cache
    )
    {
        _service = service;
        _cache = cache;
    }
    
    [HttpGet]
    public IActionResult GetPokerHand()
    {
        var id = Guid.NewGuid();
        var hand = _service.GenerateRandomHand();
        
        _cache.Set(id, hand);
        
        return Ok(new { Id = id, Hand = hand });
    }

    [HttpPost]
    public IActionResult PostPokerAction()
    {
        return Ok(new { result = "Action processed" });
    }
}