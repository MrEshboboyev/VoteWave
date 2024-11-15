using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VoteWave.Application.Polling.Commands;
using VoteWave.Application.Polling.Queries;
using VoteWave.Presentation.Services.IServices;
using VoteWave.Presentation.ViewModels;
using VoteWave.Shared.Abstractions.Commands;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Presentation.Controllers
{
    public class PollController(ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher = commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher = queryDispatcher;

        public async Task<IActionResult> Index()
        {
            var polls = await _queryDispatcher.QueryAsync(new GetAllPolls());
            return View(polls);
        }

        // GET: Polls/Details/{pollId}
        public async Task<IActionResult> Details(GetPollResults query)
        {
            var poll = await _queryDispatcher.QueryAsync(query);
            return poll != null ? View(poll) : NotFound();
        }

        // GET: Polls/Create
        public IActionResult Create()
        {
            // Initialize the model with an empty OptionTexts list
            var model = new CreatePollViewModel
            {
                OptionTexts = [""] // Render one input field by default
            };
            return View(model);
        }

        // POST: Polls/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreatePollViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreatePoll(model.Title, model.OptionTexts);
                await _commandDispatcher.DispatchAsync(command);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOption(CreatePollViewModel model)
        {
            // Add a new empty option to the list
            model.OptionTexts.Add("");
            return View("Create", model); // Return to the Create view with updated model
        }

        [HttpPost]
        public IActionResult RemoveOption(CreatePollViewModel model, int removeIndex)
        {
            if (removeIndex >= 0 && removeIndex < model.OptionTexts.Count)
            {
                // Remove the option at the specified index
                model.OptionTexts.RemoveAt(removeIndex);
            }
            return View("Create", model); // Return to the Create view with updated model
        }

        // POST: Polls/Vote
        [HttpPost]
        public async Task<IActionResult> Vote(Guid pollId, Guid optionId)
        {
            var command = new CastVote(pollId, optionId, GetUserId());
            await _commandDispatcher.DispatchAsync(command);
            return RedirectToAction(nameof(Details), new { pollId });
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId!);
        }
    }
}
