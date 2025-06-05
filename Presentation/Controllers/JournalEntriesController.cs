using Application.DTOs;
using Application.Features.JournalEntries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalEntriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JournalEntriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/journalentries
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetJournalEntriesQuery());
            return Ok(result);
        }

        // POST: api/journalentries
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJournalEntryCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // GET: api/journalentries/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Not implemented (could add a GetJournalEntryByIdQuery)
            return NotFound();
        }
    }
}
