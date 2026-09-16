using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PokerTrainerApi.DTOs;
using PokerTrainerApi.Models;
using PokerTrainerApi.Services;

namespace PokerTrainerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokerController : Controller
{
    private record CachedHand(Hand Hand, int RangeId);

    private readonly IMemoryCache _cache;
    private readonly List<RangeService> _ranges;

    public PokerController(
        IMemoryCache cache,
        List<RangeService> ranges
    )
    {
        _cache = cache;
        _ranges = ranges;
    }

    [HttpGet("ranges")]
    public IActionResult GetRanges()
    {
        return Ok(new { data = _ranges.Select((range, id) => new { label = range.ToString(), rangeId = id }) });
    }

    [HttpGet("board")]
    public IActionResult GetBoard([FromQuery] int rangeId)
    {
        return Ok(new { data = _ranges[rangeId].GetBoard() });
    }

    [HttpGet("hand")]
    public IActionResult GetPokerHand([FromQuery] int rangeId)
    {
        var id = Guid.NewGuid();
        var hand = _ranges[rangeId].GenerateRandomHand();

        _cache.Set(id, new CachedHand(hand, rangeId));

        return Ok(new { Id = id, Hand = hand });
    }

    [HttpPost("action")]
    public IActionResult PostPokerAction([FromBody] ActionRequest request)
    {
        if (request.Id == Guid.Empty)
        {
            return BadRequest("A correlation ID is required.");
        }

        if (!_cache.TryGetValue(request.Id, out CachedHand? cachedHand))
        {
            return BadRequest("This hand has expired or does not exist.");
        }

        _cache.Remove(request.Id);

        if (cachedHand == null)
        {
            throw new InvalidOperationException("Retrieved a null object from the cache.");
        }

        var hand = cachedHand.Hand;
        var rangeId = cachedHand.RangeId;

        return Ok(new { correct = _ranges[rangeId].IsCorrect(hand, request.Action) });
    }

    [HttpPost("reset")]
    public IActionResult ResetBoard([FromQuery] int rangeId)
    {
        _ranges[rangeId].Reset();
        return Ok();
    }
}
