using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ergonomiks.Infra.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorporateName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Cep = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    Country = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Phone = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Managers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Phone = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdManager = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Managers_IdManager",
                        column: x => x.IdManager,
                        principalTable: "Managers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Temperature = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    LightLevel = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Moisture = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    IdEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Employees_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Message = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    IdEquipment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Equipment_IdEquipment",
                        column: x => x.IdEquipment,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_IdEquipment",
                table: "Alerts",
                column: "IdEquipment");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Cnpj",
                table: "Companies",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CorporateName",
                table: "Companies",
                column: "CorporateName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IdUser",
                table: "Companies",
                column: "IdUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCompany",
                table: "Employees",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdManager",
                table: "Employees",
                column: "IdManager");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdUser",
                table: "Employees",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Phone",
                table: "Employees",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_IdEmployee",
                table: "Equipment",
                column: "IdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdCompany",
                table: "Managers",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdUser",
                table: "Managers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_Phone",
                table: "Managers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
