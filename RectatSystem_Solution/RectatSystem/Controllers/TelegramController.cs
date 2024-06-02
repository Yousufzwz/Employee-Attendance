using Microsoft.AspNetCore.Mvc;
using RectatSystem.Data;
using RectatSystem.Services;
using Telegram.Bot.Types;

namespace RectatSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelegramController : ControllerBase
{
    private readonly TelegramBotService _botService;
    private readonly TelegramBotContext _dbContext;

    public TelegramController(TelegramBotService botService, TelegramBotContext dbContext)
    {
        _botService = botService;
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("webhook")]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        await _botService.HandleUpdateAsync(update, _dbContext);
        return Ok();
    }
}