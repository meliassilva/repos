using System.Collections.Generic;
using System.Threading.Tasks;
using Byui.Something.ApplicationCore.Examples.Interactors.GetExample;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byui.Something.Api.Controllers
{
    /// <summary>
    /// ExampleController
    /// </summary>
    [Route("example")]
    [ApiController]
    [AllowAnonymous]
    public class ApplicatioinCoreExampleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicatioinCoreExampleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetCore()
        {
            var data = await _mediator.Send(new GetExampleRequest());
            return Ok(data);
        }
    }
}
