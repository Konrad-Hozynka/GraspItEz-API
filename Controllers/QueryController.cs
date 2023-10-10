using GraspItEz.Models;
using GraspItEz.Services;
using Microsoft.AspNetCore.Mvc;

namespace GraspItEz.Controllers
{
    [ApiController]
    [Route("{studySetId}/query")]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;
        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }
        [HttpPost("add")]
        public ActionResult AddQuery([FromRoute] int studySetId, QueryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = _queryService.NewQuery(studySetId, dto);
            return Created($"/{studySetId}/{id}", null);
        }
        [HttpPut("edit")]
        public ActionResult EditQuery([FromRoute] int studySetId, QueryWithIdDto dto)
        {
            if (!ModelState.IsValid)

            {
                return BadRequest(ModelState);
            }
            if (!_queryService.UpdateQuery(studySetId, dto)) return BadRequest();
            return Ok();
        }
        [HttpDelete("delete/{queryId}")]
        public ActionResult DeleteQuery([FromRoute] int studySetId, [FromRoute] int queryId)
        {
            bool isDeleted = _queryService.DeleteQuery(studySetId, queryId);
            if (isDeleted) return NoContent();
            return NotFound();
        }
    }
}
