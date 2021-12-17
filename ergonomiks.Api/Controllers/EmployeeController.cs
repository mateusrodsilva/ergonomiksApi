using ergonomiks.Common.Commands;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Employee;
using ergonomiks.Domain.Handler.Commands.Employee;
using ergonomiks.Domain.Handler.Queries.Employee;
using ergonomiks.Domain.Queries.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;

namespace ergonomiks.Api.Controllers
{
    [Route("v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Register a employee
        /// </summary>
        /// <param name="command">Requisition body(employee data, email and password)</param>
        /// <param name="handler">Validations and executions of create methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPost]
        [Authorize (Roles = "company")]
        public IActionResult Register([FromForm] CreateEmployeeCommand command, [FromServices] CreateEmployeeHandler handler)
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
        /// List registered employees
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns>List of registered employees</returns>
        [HttpGet("id/manager")]
        [Authorize(Roles = "manager")]
        public IActionResult GetAllByIdManager([FromServices] EmployeesListByIdManagerHandler handler)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid id = Guid.Parse(identity.FindFirst("idManager").Value);

            EmployeesListQuery query = new EmployeesListQuery();

            query.Id = id;

            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }




        /// <summary>
        /// Verify if a manager has related employees
        /// </summary>
        /// <param name="handler">Execution of verification method</param>
        /// <returns>List of registered employees</returns>
        [HttpPost("manager-has-employees")]
        [Authorize(Roles = "company")]
        public IActionResult GetEmployeeByIdManager([FromBody] EmployeesListQuery query, [FromServices] EmployeeByIdManagerHandler handler)
        {
            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// List registered employees
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns>List of registered employees</returns>
        [HttpGet("id/company")]
        [Authorize (Roles = "company")]
        public IActionResult GetAllByIdCompany([FromServices] EmployeesListByIdCompanyHandler handler)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid id = Guid.Parse(identity.FindFirst("idCompany").Value);

            EmployeesListQuery query = new EmployeesListQuery();

            query.Id = id;

            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Get employee by idUser
        /// </summary>
        /// <param name="handler">Execute list method</param>
        /// <returns>Employee object</returns>
        [HttpGet("id/user")]
        [Authorize(Roles = "employee")]
        public IActionResult GetByIdUser([FromServices] EmployeeByIdUserHandler handler)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid id = Guid.Parse(identity.FindFirst("jti").Value);

            EmployeesListQuery query = new EmployeesListQuery();

            query.Id = id;

            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Get employee by idUser
        /// </summary>
        /// <param name="query">Requisition body</param>
        /// <param name="handler">Execute list method</param>
        /// <returns>Employee object</returns>
        [HttpPost("id/user")]
        [Authorize(Roles = "manager")]
        public IActionResult GetByIdUser([FromBody] EmployeesListQuery query, [FromServices] EmployeeByIdUserHandler handler)
        {
            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Delete a employee
        /// </summary>
        /// <param name="command">Requisition body (employee Id that will be deleted)</param>
        /// <param name="handler">Executions of delete method</param>
        /// <returns>Status code Ok</returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteEmployeeCommand command, [FromServices] DeleteEmployeeHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Update a employee data
        /// </summary>
        /// <param name="command">Requisition body (Manager data)</param>
        /// <param name="handler">Validations and executions of update methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateEmployeeCommand command, [FromServices] UpdateEmployeeHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }
        
        /// <summary>
        /// Update a employee image
        /// </summary>
        /// <param name="command">Requisition body (Manager data)</param>
        /// <param name="handler">Validations and executions of update methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPut("image")]
        public IActionResult UpdateImageEmployee([FromForm] UpdateImageEmployeeCommand command, [FromServices] UpdateImageEmployeeHandler handler)
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
