using System.Collections.Generic;
using Byui.WSO2Checker.Business.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byui.WSO2Checker.Api.Controllers
{
    /// <summary>
    /// ExampleController
    /// </summary>
    [Route("example")]
    [ApiController]
    [AllowAnonymous]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleBusiness _example;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="example"></param>
        public ExampleController(ExampleBusiness example)
        {
            _example = example;
        }

        /// <summary>
        /// Returns example values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            var data = _example.GetValues();
            return Ok(data);
        }

    }
}
