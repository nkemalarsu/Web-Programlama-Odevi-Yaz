using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace WebProgramlamaOdevi.Controllers.API
{
    [Route("api/[Controller]")]
    public class LanguageController : Controller
    {
        private readonly IStringLocalizer<LanguageController> _localizer;
        public LanguageController(IStringLocalizer<LanguageController> localizer)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var guid = Guid.NewGuid();
            return Ok(_localizer["RandomGUID", guid.ToString()].Value);
        }
    }
}
