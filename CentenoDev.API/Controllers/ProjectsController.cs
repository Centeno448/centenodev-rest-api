using AutoMapper;
using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Project;
using CentenoDev.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
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
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectService.GetProjects();
            var result = _mapper.Map<IEnumerable<Project>>(projects);

            return Ok(result);
        }

        /// <summary>
        /// Get Project by Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AllowAnonymous]
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

        /// <summary>
        /// Create a Project
        /// </summary>
        /// <param name="projectForCreation"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Project>> AddProject([FromBody] ProjectForCreation projectForCreation)
        {
            var entity = _mapper.Map<ProjectEntity>(projectForCreation);

            _projectService.AddProject(entity);

            await _projectService.SaveChangesAsync();

            var createdProject = _mapper.Map<Project>(entity);

            return CreatedAtRoute("GetProject", new { guid = createdProject.Guid }, createdProject);
        }

        /// <summary>
        /// Delete a Project
        /// </summary>
        /// <param name="guid">Guid of the project to delete</param>
        /// <returns></returns>
        [HttpDelete("{guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid guid)
        {
            if (!await _projectService.ProjectExists(guid))
                return NotFound();

            _projectService.DeleteProject(guid);

            await _projectService.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Completely replace the data of an existing project
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="projectForUpdate"></param>
        /// <returns></returns>
        [HttpPut("{guid}")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid guid, [FromBody] ProjectForUpdate projectForUpdate)
        {
            if (!await _projectService.ProjectExists(guid))
                return NotFound();

            var projectEntity = await _projectService.GetProjectByGuid(guid);

            _mapper.Map(projectForUpdate, projectEntity);

            await _projectService.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Partially update a Project using JSON Patch
        /// </summary>
        /// <param name="guid">Guid of the project</param>
        /// <param name="patchDocument">JSON Patch document</param>
        /// <returns></returns>
        /// <remarks>
        /// [JSON Patch Documentation](http://jsonpatch.com/)   
        ///     
        /// JSON Patch format example:   
        /// ```
        /// [
        ///     { "op": "replace", "path": "/propertyName", "value": ["newValue"] }
        /// ]
        /// ```  
        ///   
        /// Example Request:   
        /// ```
        /// [
        ///     {"op": "replace", "path": "/name", "value": "new Name"}
        /// ]
        /// ```
        /// </remarks>
        [HttpPatch("{guid}")]
        [Consumes("application/json", "application/json-patch+json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PartiallyUpdateProject([FromRoute] Guid guid, 
            [FromBody] JsonPatchDocument<ProjectForUpdate> patchDocument)
        {
            if (!await _projectService.ProjectExists(guid))
                return NotFound();

            var projectEntity = await _projectService.GetProjectByGuid(guid);

            var projectToPatch = _mapper.Map<ProjectForUpdate>(projectEntity);

            patchDocument.ApplyTo(projectToPatch, ModelState);

            if (!TryValidateModel(projectToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(projectToPatch, projectEntity);

            await _projectService.SaveChangesAsync();

            return NoContent();
        }


        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
