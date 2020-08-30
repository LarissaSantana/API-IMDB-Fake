using IMDb.Domain.Utility;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDb.Data.Migrations
{
    public partial class insert_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql(@$"insert into IMDb.Role (RoleId, Name)
                        values('{RoleIdentify.Administrator.Value}', 'Administrator'),
                              ('{RoleIdentify.Common.Value}', 'Common')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
               .Sql(@$"delete from IMDb.Role where RoleId in ('{RoleIdentify.Administrator.Value}', '{RoleIdentify.Common.Value}')");
        }
    }
}
