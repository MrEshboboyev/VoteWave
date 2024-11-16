using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoteWave.Application.Polling.Commands;
using VoteWave.Application.Polling.Queries;
using VoteWave.Shared.Abstractions.Commands;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin")]
public class AdminController(ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : Controller
{
    private readonly ICommandDispatcher _commandDispatcher = commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher = queryDispatcher;

    [HttpGet("polls")]
    public async Task<IActionResult> Index()
    {
        var allPolls = await _queryDispatcher.QueryAsync(new GetAllPolls());
        return View(allPolls);
    }

    [HttpGet]
    public async Task<IActionResult> Details([FromQuery] GetPollResults query)
    {
        var pollWithResults = await _queryDispatcher.QueryAsync(query);
        return View(pollWithResults);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTitle([FromBody] UpdatePollTitle command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return RedirectToAction(nameof(Details), command.PollId);
    }

    [HttpPost]
    public async Task<IActionResult> AddOption(AddOptionToPoll command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return RedirectToAction(nameof(Details), command.PollId);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveOption(RemoveOptionFromPoll command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return RedirectToAction(nameof(Details), command.PollId);
    }

    [HttpPost("UpdateOption")]
    public async Task<IActionResult> UpdateOption(UpdatePollOption command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return RedirectToAction(nameof(Details), command.PollId);
    }
}

