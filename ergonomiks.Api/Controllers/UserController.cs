using ergonomiks.Common.Commands;
using ergonomiks.Common.Enum;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Authentication;
using ergonomiks.Domain.Commands.User;
using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Handler.Commands.User;
using ergonomiks.Domain.Handler.Commands.Users;
using ergonomiks.Domain.Handler.Queries.User;
using ergonomiks.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Api.Controllers
{
    [Route("v1/account")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        private readonly ICompanyRepository _companyRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UserController(ICompanyRepository companyRepository, IManagerRepository managerRepository, IEmployeeRepository employeeRepository)
        {
            _companyRepository = companyRepository;
            _managerRepository = managerRepository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// Register a separate user
        /// </summary>
        /// <param name="command">Requisition body(email and password)</param>
        /// <param name="handler">Validations and executions of create methods</param>
        /// <returns>Status Code Ok</returns>
        [Route("signup")]
        [HttpPost]
        public IActionResult Register([FromBody] CreateAccountCommand command, [FromServices] CreateUserHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Log a user into the application
        /// </summary>
        /// <param name="command">Requisition body (email and password)</param>
        /// <param name="handler">Validations and executions of Sign In method</param>
        /// <returns>Status Code Ok and Authentication Token</returns>
        [Route("signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInCommand command,[FromServices] SignInHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                var token = GenerateJSONWebToken((User)result.Data);
                return Ok(new GenericCommandResult(result.Success, result.Message, new { Token = token }));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// List registered users
        /// </summary>
        /// <param name="handler">Execution of List method</param>
        /// <returns>List of registered users</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll([FromServices] UsersListHandler handler)
        {
            UsersListQuery query = new UsersListQuery();
            var result = (GenericQueryResult)handler.Handle(query);

            if (result.Success == true)
            {
                return Ok((new GenericQueryResult(result.Success, result.Message, result.Data)));
            }

            return BadRequest(new GenericQueryResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="command">Requisition body (User Id that will be deleted)</param>
        /// <param name="handler">Executions of delete method</param>
        /// <returns>Status code Ok</returns>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete([FromBody] DeleteUserCommand command, [FromServices] DeleteUserHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        /// <summary>
        /// Update a user's password
        /// </summary>
        /// <param name="command">Requisition body</param>
        /// <param name="handler">Execution of update methods</param>
        /// <returns>Status code Ok</returns>
        [HttpPatch("password")]
        [Authorize]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordCommand command, [FromServices] UpdatePasswordHandler handler)
        {
            var result = (GenericCommandResult)handler.Handle(command);

            if (result.Success == true)
            {
                return Ok(new GenericCommandResult(result.Success, result.Message, result.Data));
            }

            return BadRequest(new GenericCommandResult(result.Success, result.Message, result.Data));
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ergonomiks-authentication"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userType = userInfo.UserType;

            Company company = new Company();
            bool isCompany = userType == EnUserType.company;
            if (isCompany) company = _companyRepository.GetByIdUser(userInfo.Id);

            Manager manager = new Manager();
            bool isManager = userType == EnUserType.manager;
            if(isManager) manager = _managerRepository.GetByIdUser(userInfo.Id);

            Employee employee = new Employee();
            bool isEmployee = userType == EnUserType.employee;
            if (isEmployee) employee = _employeeRepository.GetByIdUser(userInfo.Id);

            var claims = Array.Empty<Claim>();

            switch (userType)
            {
                case EnUserType.company:
                    claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                        new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
                        new Claim("role", userInfo.UserType.ToString()),
                        new Claim("idCompany", company.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
                    };
                    break;
                case EnUserType.manager:
                    claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                        new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
                        new Claim("role", userInfo.UserType.ToString()),
                        new Claim("idManager", manager.Id.ToString()),
                        new Claim("image", manager.Image),
                        new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
                    };
                    break;
                case EnUserType.employee:
                    claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                        new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
                        new Claim("role", userInfo.UserType.ToString()),
                        new Claim("idEmployee", employee.Id.ToString()),
                        new Claim("image", employee.Image),
                        new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
                    };
                    break;
                default:
                    claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                        new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
                        new Claim("role", userInfo.UserType.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
                    };
                    break;
            }


            // We configure our token and lifetime
            var token = new JwtSecurityToken
                (
                    "ergonomiks",
                    "ergonomiks",
                    claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
