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

    [HttpGet("range")]
    public async Task<IActionResult> GetRange(string spotKey)
    {
        var range = await _repository.GetRange(spotKey);
        return range == null ? NotFound() : Ok(range);
    }

}