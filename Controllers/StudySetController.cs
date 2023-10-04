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
    [Route("study-set")]
    public class StudySetController : ControllerBase
    {
        private readonly IStudySetsService _studySetsService;
        
        public StudySetController(IStudySetsService studySetsService)
        {
            _studySetsService = studySetsService;
        }
        [HttpGet("last-used")]
        public ActionResult<IEnumerable<StudySetHeadLineDto>> GetToHomePage()
        {
            var studySets = _studySetsService.GetLastUsedStudySets();
            return Ok(studySets);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<StudySetHeadLineDto>> GetAll()
        {
            var studySets = _studySetsService.GetAllStudySets();
            return Ok(studySets);
        }
        [HttpGet("{id}")]
        public ActionResult<StudySetDto> Get([FromRoute] int id)
        {
            var studySetsDto =  _studySetsService.GetById(id);
            if(studySetsDto == null) return NotFound();
            return Ok(studySetsDto);  
        }
        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateStudySetDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           int id = _studySetsService.CreateStudySet(dto);
            return Created($"/StudySet/{id}", null);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult Delete([FromRoute] int id) 
        {
            bool isDeleted = _studySetsService.DeleteStudySet(id);
            if (isDeleted) return NoContent();
            return NotFound();
        }
        [HttpPut("update")]
        public ActionResult Update([FromBody] UpdateStudySetDto Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ( !_studySetsService.UpdateStudySet(Dto)) return BadRequest();
            
            return Ok();
        }
    }
}
