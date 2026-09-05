using Microsoft.AspNetCore.Mvc;
using PokerTrainerApi.DrawRanges.Repository;

namespace PokerTrainerApi.DrawRanges;

[Route("/api/[controller]")]
[ApiController]
public class DrawRangesController : ControllerBase
{
    private IRangeRepository _repository;

    public DrawRangesController(IRangeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("rangeSpot")]
    public async Task<IActionResult> GetRangeSpot(string spotKey) => Ok(await _repository.GetRange(spotKey));

    [HttpPost("range")]
    public async Task<IActionResult> PostRange(string spotKey, [FromBody] PokerRange range)
    {
        await _repository.UpdateRange(spotKey, range);
        return Ok();
    }
}