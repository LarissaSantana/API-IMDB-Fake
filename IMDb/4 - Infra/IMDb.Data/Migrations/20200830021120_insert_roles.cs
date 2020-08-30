using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IMDb.Data.Migrations
{
    public partial class insert_roles : Migration
    {
        private Guid administratorId = Guid.Parse("e33a5da4-4c46-4f0e-8ef7-8d01a12f9884");
        private Guid commonUserId = Guid.Parse("04f1d204-4121-44ec-88bd-4b3d433fbaf6");

        protected override void Up(MigrationBuilder migrationBuilder)
        {    
            migrationBuilder
                .Sql(@$"insert into IMDb.Role (RoleId, Name)
                        values('{administratorId}', 'Administrator'),
                              ('{commonUserId}', 'Common')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
               .Sql(@$"delete from IMDb.Role where RoleId in ('{administratorId}', '{commonUserId}')");
        }
    }
}
