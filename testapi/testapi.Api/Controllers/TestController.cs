using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Byui.StudentList.Api.Helpers;
using Byui.StudentList.Business.Business;
using Byui.StudentList.Business.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Byui.testapi.Api.Controllers
{
    /// <summary>
    /// ExampleController
    /// </summary>
    [Route("controller")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        private readonly TestBusiness _test;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="example"></param>
        public TestController(TestBusiness test)
        {
            _test = test;
        }

        /// <summary>
        /// Returns example values
        /// </summary>
        /// <returns></returns>
        [HttpGet("{}")]
        public ActionResult<List<string>> Get()
        {
            var data = await _test.GetValues();
            return Ok(data);
        }

    }
}
