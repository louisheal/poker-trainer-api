using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PokerTrainerAPI.DTOs;
using PokerTrainerAPI.Models;
using PokerTrainerAPI.Services;

namespace PokerTrainerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokerController : Controller
{
    private readonly IPokerService _service;
    private readonly IMemoryCache _cache;
    private readonly IRangeService _range;
    
    public PokerController(
        IPokerService service,
        IMemoryCache cache,
        IRangeService range
    )
    {
        _service = service;
        _cache = cache;
        _range = range;
    }

    [HttpGet("board")]
    public IActionResult GetBoard()
    {
        return Ok(new { data = _range.GetBoard() });
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

        if (!_cache.TryGetValue(request.Id, out var obj))
        {
            return BadRequest("This hand has expired or does not exist.");
        }
        
        _cache.Remove(request.Id);

        var hand = obj as Hand;
        if (hand == null)
        {
            return BadRequest("Cached item is not a valid Hand.");
        }

        return Ok(new { correct = _range.IsCorrect(hand, request.Action) });
    }
}