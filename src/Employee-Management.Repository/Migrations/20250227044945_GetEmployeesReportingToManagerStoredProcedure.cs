using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class GetEmployeesReportingToManagerStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
           CREATE PROCEDURE GetEmployeesReportingToManager
                @ManagerId INT
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT 
                    Id,
                    FirstName,
                    LastName,
                    Designation
                FROM 
                    Employees
                WHERE 
                    ReportsToId = @ManagerId;
            END;
           ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            DROP PROCEDURE IF EXISTS GetEmployeesReportingToManager;
        ";
            migrationBuilder.Sql(sql);
        }
    }
}
