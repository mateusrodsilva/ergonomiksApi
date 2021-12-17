using ergonomiks.Common.Commands;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Equipment;
using ergonomiks.Domain.Handler.Commands.Equipment;
using ergonomiks.Domain.Queries.Equipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ergonomiks.Api.Controllers
{
    [Route("v1/equipment")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Register a Equipment
        /// </summary>
        /// <param name="command">Requisition body</param>
        /// <param name="handler">Execution of create methods</param>
        /// <returns>Status code Ok</returns>
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] CreateEquipmentCommand command, [FromServices] CreateEquipmentHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// List all equipments
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns>List all equipments</returns>
        [HttpGet]
        public IActionResult GetAll([FromServices] ListEquipmentHandler handler)
        {
            EquipmentsListQuery query = new EquipmentsListQuery();
            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }
    }
}
