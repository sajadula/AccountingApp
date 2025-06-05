using Application.Features.TrialBalance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TrialBalanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrialBalanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: /api/trialbalance
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var trialBalance = await _mediator.Send(new GetTrialBalanceQuery());
        return Ok(trialBalance);
    }
}
