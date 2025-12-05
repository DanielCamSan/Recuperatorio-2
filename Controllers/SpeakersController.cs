using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpeakersController : ControllerBase
    {
        //TODO  pista: usa speaker y talk service
        private readonly ITalkService _talkService;
        private readonly ISpeakerService _speakerService;
        public SpeakersController(ISpeakerService speakerService, ITalkService talks)
        {
            _speakerService = speakerService;
            _talkService = talks;
        }
        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            //TODO
            var id = await _speakerService.CreateAsync(dto);
            return Created($"api/v1/speakers/{id}", new {id});
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
            var data= await _speakerService.GetScheduleAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            await _talkService.AddTalkAsync(dto);
            return Ok();
        }
    }
}
