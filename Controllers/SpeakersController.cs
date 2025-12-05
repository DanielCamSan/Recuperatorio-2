using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpeakersController : ControllerBase
    {
        //TODO  pista: usa speaker y talk service : terminado
        private readonly ISpeakerService _sp;
        private readonly ITalkService _talk;
        public SpeakersController(ISpeakerService sp, ITalkService talk)
        {
            _sp = sp;
            _talk = talk;
        }

        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            //TODO : terminado
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var speaker = await _sp.CreateAsync(dto);
            return Ok();
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            //TODO
        }
    }
}
