using GraspItEz.Models;
using GraspItEz.Services;
using Microsoft.AspNetCore.Mvc;

namespace GraspItEz.Controllers
{
    [ApiController]
    [Route("learn/{id}")]
    public class LearnController : ControllerBase
    {
        private readonly ILearnService _learnService;
        public LearnController(ILearnService learnService)
        {
           _learnService = learnService;
        }
        [HttpGet("question-set")]
        public ActionResult<IEnumerable<QuestionDto>> LearnStart([FromRoute] int id)
        {
            var activeQueststion = _learnService.StartLearn(id);
            return Ok(activeQueststion);
        }
        [HttpPost("round-end")]
        public ActionResult SaveProgress([FromRoute] int id, [FromBody] List<QuestionAnswer> answer)
        {
            _learnService.EndOfRound(id, answer);
            return Ok();
        }
        [HttpGet("restart")]
        public ActionResult Restart([FromRoute] int id)
        {
            _learnService.Reset(id);
            return Ok();
        }
    }
}
