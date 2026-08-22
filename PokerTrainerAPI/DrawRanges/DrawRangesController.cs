using Microsoft.AspNetCore.Mvc;

namespace PokerTrainerApi.DrawRanges;

[Route("/api/[controller]")]
[ApiController]
public class DrawRangesController : ControllerBase
{
    private IDrawRangesService _ranges;

    public DrawRangesController(IDrawRangesService ranges)
    {
        _ranges = ranges;
    }

    [HttpGet("rangeSpot")]
    public IActionResult GetRangeSpot() => Ok(_ranges.GetRangeSpot());
}