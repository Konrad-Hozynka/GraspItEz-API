using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using GraspItEz.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Controllers
{
    [ApiController]
    [Route("studySet")]
    public class StudySetController : ControllerBase
    {
        private readonly IGraspItEzService _graspItEzService;
        
        public StudySetController(IGraspItEzService graspItEzService)
        {
            _graspItEzService = graspItEzService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<StudySetHeadsDto>> GetToHomePage()
        {
            var studySetsHead = _graspItEzService.GetStudySetsHeder();
            return Ok(studySetsHead);
        }
        [HttpGet("/all")]
        public ActionResult<IEnumerable<StudySetHeadsDto>> GetAll()
        {
            var studySetsHead = _graspItEzService.GetAllStudySets();
            return Ok(studySetsHead);
        }
        [HttpGet("{id}")]
        public ActionResult<StudySetDto> Get([FromRoute] int id)
        {
            var studySetsDto =  _graspItEzService.GetById(id);
            if(studySetsDto == null) return NotFound();
            return Ok(studySetsDto);  
        }
        [HttpGet("learn/start/{id}")]
        public ActionResult<IEnumerable<QuestionDto>> LearnStart([FromRoute] int id)
        {
            var activeQueststion = _graspItEzService.StartLearn(id);
            return Ok(activeQueststion);
        }
        [HttpPost("/learn/{id}")]
        public ActionResult SaveProgress([FromRoute] int id, [FromBody] List<QuestionAnswer> answer)
        {
           _graspItEzService.EndOfRound(id, answer);
            return Ok();
        }
        [HttpPost("/learn/restart/{id}")]
        public ActionResult Restart([FromRoute] int id)
        {
            _graspItEzService.Reset(id);
            return Ok();
        }
        

    }
}
