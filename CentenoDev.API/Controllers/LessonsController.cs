using CentenoDev.API.Models.Lesson;
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
    [Route("projects/{projectGuid}/[controller]")]
    [Authorize]
    public class LessonsController : ControllerBase
    {
        //[AllowAnonymous]
        //[HttpGet]
        //[Consumes("application/json")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons([FromRoute] Guid projectGuid)
        //{

        //}
    }
}
