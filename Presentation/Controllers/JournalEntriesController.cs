using Application.Features.JournalEntries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class JournalEntriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public JournalEntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: /api/journalentries
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entries = await _mediator.Send(new GetJournalEntriesQuery());
        return Ok(entries);
    }

    // POST: /api/journalentries
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJournalEntryCommand command)
    {
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    // GET: /api/journalentries/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Not implemented; return 404
        return NotFound();
    }
}
