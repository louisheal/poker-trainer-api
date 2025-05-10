using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PokerTrainerAPI.DTOs;
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
    
    [HttpGet("hand")]
    public IActionResult GetPokerHand()
    {
        var id = Guid.NewGuid();
        var hand = _service.GenerateRandomHand();
        
        _cache.Set(id, hand);
        
        return Ok(new { Id = id, Hand = hand });
    }

    [HttpPost("action")]
    public IActionResult PostPokerAction([FromBody] ActionRequest request)
    {
        if (request.Id == Guid.Empty)
        {
            return BadRequest("A correlation ID is required.");
        }

        if (!_cache.TryGetValue(request.Id, out var hand))
        {
            return BadRequest("This hand has expired or does not exist.");
        }
        
        return Ok(new { result = "Action processed." });
    }
}