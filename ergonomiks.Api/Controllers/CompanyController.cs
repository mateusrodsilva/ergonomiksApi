using ergonomiks.Common.Commands;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Company;
using ergonomiks.Domain.Handler.Commands.Companies;
using ergonomiks.Domain.Handler.Queries.Company;
using ergonomiks.Domain.Queries.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;

namespace ergonomiks.Api.Controllers
{
    [Route("v1/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Register a company
        /// </summary>
        /// <param name="command">Requisition body(Company data, email and password)</param>
        /// <param name="handler">Validations and executions of create methods</param>
        /// <returns>Status Code Ok</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Register([FromBody] CreateCompanyCommand command, [FromServices] CreateCompanyHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);
            if (result.Success == true)
            {
                return Ok((new GenericCommandResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Company List
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll([FromServices] CompanyListHandler handler)
        {
            CompaniesListQuery query = new CompaniesListQuery();

            var result = (GenericQueryResult)handler.Handle(query);
            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }


        /// <summary>
        /// Seaech company by idUser
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns></returns>
        [HttpGet("id/user")]
        [Authorize]
        public IActionResult GetByIdUser([FromServices] CompanyByIdUserHandler handler)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid id = Guid.Parse(identity.FindFirst("jti").Value);

            Console.WriteLine(id);
            CompanyListByIdQuery query = new CompanyListByIdQuery();

            query.Id = id;

            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Delete a company
        /// </summary>
        /// <param name="command">Requisition body (Company Id that will be deleted)</param>
        /// <param name="handler">Executions of delete method</param>
        /// <returns>Status Code Ok</returns>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete([FromBody] DeleteCompanyCommand command, [FromServices] DeleteCompanyHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Update a company data
        /// </summary>
        /// <param name="command">Requisition body</param>
        /// <param name="handler">Execution of update methods</param>
        /// <returns>Status code Ok</returns>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateCompanyCommand command, [FromServices] UpdateCompanyHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }


    }
}
