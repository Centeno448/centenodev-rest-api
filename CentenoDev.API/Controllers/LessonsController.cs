using AutoMapper;
using CentenoDev.API.Models;
using CentenoDev.API.Services.Lesson;
using CentenoDev.API.Services.Project;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("projects/{projectGuid}/lessons")]
    [EnableCors("AllowOrigin")]
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

        /// <summary>
        /// Get all lessons from a project
        /// </summary>
        /// <param name="projectGuid"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Get a lesson from a project
        /// </summary>
        /// <param name="projectGuid"></param>
        /// <param name="lessonGuid"></param>
        /// <returns></returns>
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

    }
}
