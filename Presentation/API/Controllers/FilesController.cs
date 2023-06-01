using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : Controller
{
    private readonly IConfiguration _configuration;

    public FilesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("[action]")]
    public IActionResult GetBaseUrl()
    {
        return Ok(_configuration["BaseStorageUrl"]);
    }
}