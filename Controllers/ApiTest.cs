using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesURLController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Response from Version-1";
        }
    }
}
namespace ECAdmin.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesURLController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Response from Version-2";
        }
    }
}