using AutoMapper;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Lesson;
using CentenoDev.API.Services;
using CentenoDev.API.Services.Lesson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("projects/{projectGuid}/lessons")]
    [Authorize]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        private readonly IProjectService _projectService;

        private readonly IMapper _mapper;

        public LessonsController(ILessonService lessonService, IProjectService projectService, IMapper mapper)
        {
            _lessonService = lessonService;
            _mapper = mapper;
            _projectService = projectService;
        }


        [AllowAnonymous]
        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons([FromRoute] Guid projectGuid)
        {
            if(! await _projectService.ProjectExists(projectGuid))
                return NotFound();

            var lessons = await _lessonService.GetAllLessons(projectGuid);

            var result = _mapper.Map<IEnumerable<Lesson>>(lessons);

            return Ok(result);
        }


        [AllowAnonymous]
        [HttpGet("{lessonGuid}", Name = "GetLesson")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Lesson>> GetLesson([FromRoute] Guid projectGuid, [FromRoute] Guid lessonGuid)
        {
            if (! await _projectService.ProjectExists(projectGuid))
                return NotFound();

            if (!await _lessonService.LessonExists(projectGuid, lessonGuid))
                return NotFound();

            var lesson = await _lessonService.GetLessonByGuid(projectGuid, lessonGuid);

            var result = _mapper.Map<Lesson>(lesson);

            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult<Lesson>> AddLesson([FromRoute] Guid projectGuid, [FromBody] LessonForCreation newLesson)
        {
            if (!await _projectService.ProjectExists(projectGuid))
                return NotFound();
            
            var newLessonEntity = _mapper.Map<LessonEntity>(newLesson);

            await _lessonService.AddLesson(newLessonEntity);

            await _lessonService.SaveChangesAsync();

            var createdLesson = _mapper.Map<Lesson>(newLessonEntity);

            return CreatedAtRoute("GetLesson", new { projectGuid, lessonGuid = createdLesson.Guid }, createdLesson);
        }


        [HttpDelete("{lessonGuid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteLesson([FromRoute] Guid projectGuid, [FromRoute] Guid lessonGuid)
        {
            if (!await _projectService.ProjectExists(projectGuid))
                return NotFound();

            if (!await _lessonService.LessonExists(projectGuid, lessonGuid))
                return NotFound();

            await _lessonService.DeleteLesson(projectGuid, lessonGuid);

            await _lessonService.SaveChangesAsync();

            return NoContent();
        }
    }
}
