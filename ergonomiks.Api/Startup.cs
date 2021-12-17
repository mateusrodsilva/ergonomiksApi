
using ergonomiks.Common.Services;
using ergonomiks.Common.Settings;
using ergonomiks.Domain.Handler.Commands.Alerts;
using ergonomiks.Domain.Handler.Commands.Companies;
using ergonomiks.Domain.Handler.Commands.Employee;
using ergonomiks.Domain.Handler.Commands.Equipment;
using ergonomiks.Domain.Handler.Commands.Manager;
using ergonomiks.Domain.Handler.Commands.User;
using ergonomiks.Domain.Handler.Commands.Users;
using ergonomiks.Domain.Handler.Queries.Alert;
using ergonomiks.Domain.Handler.Queries.Company;
using ergonomiks.Domain.Handler.Queries.Employee;
using ergonomiks.Domain.Handler.Queries.Manager;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using ergonomiks.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMailService, MailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //Correção do erro object cycle
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //Remover propriedades nulas
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            // Swagger =========
            // Adiciona o servi?o do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ergonomiks.Api", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            // ========= Swagger

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();





            services.AddDbContext<ErgonomiksContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #region JWT Bearer Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "ergonomiks",
                        ValidAudience = "ergonomiks",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ergonomiks-authentication")),

                    };
                });
            #endregion

            #region User dependency injection
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<CreateUserHandler, CreateUserHandler>();
            services.AddTransient<SignInHandler, SignInHandler>();
            services.AddTransient<UsersListHandler, UsersListHandler>();
            services.AddTransient<DeleteUserHandler, DeleteUserHandler>();
            services.AddTransient<UpdatePasswordHandler, UpdatePasswordHandler>();

            #endregion

            #region Company dependency injection
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<CreateCompanyHandler, CreateCompanyHandler>();
            services.AddTransient<CompanyListHandler, CompanyListHandler>();
            services.AddTransient<CompanyByIdUserHandler, CompanyByIdUserHandler>();
            services.AddTransient<DeleteCompanyHandler, DeleteCompanyHandler>();
            services.AddTransient<UpdateCompanyHandler, UpdateCompanyHandler>();
            #endregion

            #region Manager dependency injection
            services.AddTransient<IManagerRepository, ManagerRepository>();
            services.AddTransient<CreateManagerHandler, CreateManagerHandler>();
            services.AddTransient<ManagersListByIdCompanyHandler, ManagersListByIdCompanyHandler>();
            services.AddTransient<ManagerByIdUserHandler, ManagerByIdUserHandler>();
            services.AddTransient<DeleteManagerHandler, DeleteManagerHandler>();
            services.AddTransient<UpdateManagerHandler, UpdateManagerHandler>();
            services.AddTransient<UpdateImageManagerHandler, UpdateImageManagerHandler>();
            #endregion

            #region Employee dependency injection
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<CreateEmployeeHandler, CreateEmployeeHandler>();
            services.AddTransient<EmployeesListByIdManagerHandler, EmployeesListByIdManagerHandler>();
            services.AddTransient<EmployeesListByIdCompanyHandler, EmployeesListByIdCompanyHandler>();
            services.AddTransient<EmployeeByIdUserHandler, EmployeeByIdUserHandler>();
            services.AddTransient<UpdateEmployeeHandler, UpdateEmployeeHandler>();
            services.AddTransient<UpdateImageEmployeeHandler, UpdateImageEmployeeHandler>();
            services.AddTransient<DeleteEmployeeHandler, DeleteEmployeeHandler>();
            services.AddTransient<EmployeeByIdManagerHandler, EmployeeByIdManagerHandler>();
            #endregion

            #region Equipment dependency injection
            services.AddTransient<IEquipmentRepository, EquipmentRepository>();
            services.AddTransient<CreateEquipmentHandler, CreateEquipmentHandler>();
            services.AddTransient<ListEquipmentHandler, ListEquipmentHandler>();
            #endregion

            #region Alert dependency injection
            services.AddTransient<IAlertRepository, AlertRepository>();
            services.AddTransient<CreateAlertHandler, CreateAlertHandler>();
            services.AddTransient<AlertByIdEmployeeHandler, AlertByIdEmployeeHandler>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ergonomiks.Api");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();                        

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = "/Resources"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
