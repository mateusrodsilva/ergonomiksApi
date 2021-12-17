using ergonomiks.Common.Commands;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Alerts;
using ergonomiks.Domain.Handler.Commands.Alerts;
using ergonomiks.Domain.Handler.Queries.Alert;
using ergonomiks.Domain.Queries.Alert;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ergonomiks.Api.Controllers
{
    [Route("v1/alert")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        [HttpPost]
        public IActionResult Register([FromBody] CreateAlertCommand command, [FromServices] CreateAlertHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);
            if (result.Success == true)
            {
                return Ok((new GenericCommandResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }


        [HttpPost("employee/id")]
        public IActionResult GetByIdEmployee([FromBody] AlertByIdEmployeeQuery command, [FromServices] AlertByIdEmployeeHandler handler)
        {
            var result = (GenericQueryResult)handler.Handle(command);
            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }
    }
}
