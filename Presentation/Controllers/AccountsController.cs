using Application.DTOs;
using Application.Features.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAccountsQuery());
            return Ok(result);
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Not implemented (could add a GetAccountByIdQuery)
            return NotFound();
        }
    }
}
