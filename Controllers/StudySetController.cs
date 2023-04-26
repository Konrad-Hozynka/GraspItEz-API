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
        private readonly IStudySetsService _studySetsService;
        
        public StudySetController(IStudySetsService studySetsService)
        {
            _studySetsService = studySetsService;
        }
        [HttpGet("lastUsed")]
        public ActionResult<IEnumerable<StudySetHeadsDto>> GetToHomePage()
        {
            var studySetsHead = _studySetsService.GetStudySetsHeder();
            return Ok(studySetsHead);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<StudySetHeadsDto>> GetAll()
        {
            var studySetsHead = _studySetsService.GetAllStudySets();
            return Ok(studySetsHead);
        }
        [HttpGet("{id}")]
        public ActionResult<StudySetDto> Get([FromRoute] int id)
        {
            var studySetsDto =  _studySetsService.GetById(id);
            if(studySetsDto == null) return NotFound();
            return Ok(studySetsDto);  
        }
    }
}
