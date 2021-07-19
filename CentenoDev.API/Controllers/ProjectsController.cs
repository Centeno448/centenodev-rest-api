using AutoMapper;
using CentenoDev.API.Models;
using CentenoDev.API.Services.Project;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("projects")]
    [EnableCors("AllowOrigin")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService service , IMapper mapper)
        {
            _logger = logger;
            _projectService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectService.GetProjects();
            var result = _mapper.Map<IEnumerable<Project>>(projects);

            return Ok(result);
        }

        /// <summary>
        /// Get project by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("{guid}", Name = "GetProject")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Project>> GetProject([FromRoute] Guid guid)
        {
            var project = await _projectService.GetProjectByGuid(guid);

            if (project == null)
                return NotFound();
            
            var result = _mapper.Map<Project>(project);

            return Ok(result);
        }

    }
}
