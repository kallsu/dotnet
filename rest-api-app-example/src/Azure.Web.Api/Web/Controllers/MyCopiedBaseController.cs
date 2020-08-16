using System.Collections.Generic;
using Azure.Web.Api.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace src.Azure.Serverless.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(MyError), 400)]
    [ProducesResponseType(typeof(ICollection<MyError>), 401)]
    [ProducesResponseType(typeof(MyError), 500)]
    public class MyCopiedBaseController : ControllerBase
    {
        private readonly ILogger<MyCopiedBaseController> _logger;

        public MyCopiedBaseController(ILogger<MyCopiedBaseController> logger)
        {
            _logger = logger;
        }
    }
}