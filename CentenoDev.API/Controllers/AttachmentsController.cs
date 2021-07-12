using AutoMapper;
using CentenoDev.API.Models;
using CentenoDev.API.Services.Attachment;
using CentenoDev.API.Services.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("projects/{projectGuid}/attachments")]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        private readonly IProjectService _projectService;

        private readonly IMapper _mapper;

        public AttachmentsController(IAttachmentService attachmentService, IProjectService projectService, IMapper mapper)
        {
            _attachmentService = attachmentService;
            _mapper = mapper;
            _projectService = projectService;
        }

        /// <summary>
        /// Get all attachments from a project
        /// </summary>
        /// <param name="projectGuid"></param>
        /// <returns></returns>
        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Attachment>>> GetLessons([FromRoute] Guid projectGuid)
        {
            if (!await _projectService.ProjectExists(projectGuid))
                return NotFound();

            var lessons = await _attachmentService.GetAllAttachments(projectGuid);

            var result = _mapper.Map<IEnumerable<Attachment>>(lessons);

            return Ok(result);
        }


        /// <summary>
        /// Get an attachment from a project
        /// </summary>
        /// <param name="projectGuid"></param>
        /// <param name="attachmentGuid"></param>
        /// <returns></returns>
        [HttpGet("{attachmentGuid}", Name = "GetAttachment")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Attachment>> GetLesson([FromRoute] Guid projectGuid, [FromRoute] Guid attachmentGuid)
        {
            if (!await _projectService.ProjectExists(projectGuid))
                return NotFound();

            if (!await _attachmentService.AttachmentExists(projectGuid, attachmentGuid))
                return NotFound();

            var lesson = await _attachmentService.GetAttachmentByGuid(projectGuid, attachmentGuid);

            var result = _mapper.Map<Attachment>(lesson);

            return Ok(result);
        }
    }
}
