using LifestyleCoach.Data;
using LifestyleCoach.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController(OllamaService ollamaService) : ControllerBase
{
    private readonly OllamaService _ollamaService = ollamaService;

    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var result = await _ollamaService.SendPromptAsync(request.Prompt);
        return Ok(result);
    }
}

