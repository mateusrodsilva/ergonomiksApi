using ergonomiks.Common.Commands;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Manager;
using ergonomiks.Domain.Handler.Commands.Manager;
using ergonomiks.Domain.Queries.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using ergonomiks.Domain.Handler.Queries.Manager;

namespace ergonomiks.Api.Controllers
{
    [Route("v1/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Register a manager
        /// </summary>
        /// <param name="command">Requisition body(manager data, email and password)</param>
        /// <param name="handler">Validations and executions of create methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPost]
        [Authorize(Roles = "company")]
        public IActionResult Register([FromForm] CreateManagerCommand command, [FromServices] CreateManagerHandler handler)
        {

            if (Request.Form.Files.Count == 0)
            {
                return BadRequest(new GenericCommandResult(false, "Image field cannot be empty", ""));

            }

            var result = (GenericCommandResult)handler.Handle(command, Request.Form.Files[0]);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// List registered managers by idCompany
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns>List of registered managers</returns>
        [HttpGet("id/company")]
        [Authorize (Roles = "company")]
        public IActionResult GetAllByIdCompany([FromServices] ManagersListByIdCompanyHandler handler)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid id = Guid.Parse(identity.FindFirst("idCompany").Value);

            ManagersListQuery query = new ManagersListQuery();

            query.Id = id;

            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));

        }

        /// <summary>
        /// Search a manager by idUser
        /// </summary>
        /// <param name="handler">Execution of search method</param>
        /// <returns>Status code Ok</returns>
        [HttpGet("id/user")]
        [Authorize(Roles = "manager")]
        public IActionResult GetAllByIdUser([FromServices] ManagerByIdUserHandler handler)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid id = Guid.Parse(identity.FindFirst("jti").Value);

            ManagersListQuery query = new ManagersListQuery();

            query.Id = id;

            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));

        }


        /// <summary>
        /// Delete a manager
        /// </summary>
        /// <param name="command">Requisition body (Manager Id that will be deleted)</param>
        /// <param name="handle">Executions of delete method</param>
        /// <returns>Status code Ok</returns>
        [HttpDelete]
        [Authorize(Roles = "company")]
        public IActionResult Delete([FromBody] DeleteManagerCommand command, [FromServices] DeleteManagerHandler handle)
        {
            var result = (GenericCommandResult)handle.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Update a manager data
        /// </summary>
        /// <param name="command">Requisition body (Manager data)</param>
        /// <param name="handler">Validations and executions of update methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPut]
        [Authorize(Roles = "company")]
        public IActionResult Update([FromBody] UpdateManagerCommand command, [FromServices] UpdateManagerHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Update a manager data
        /// </summary>
        /// <param name="command">Requisition body (Manager data)</param>
        /// <param name="handler">Validations and executions of update methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPut("image")]
        [Authorize(Roles = "company")]
        public IActionResult UpdateImageManager([FromForm] UpdateImageManagerCommand command, [FromServices] UpdateImageManagerHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command, Request.Form.Files[0]);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

    }
}
