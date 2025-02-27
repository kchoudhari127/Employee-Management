using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddGetManagerForEmployeeStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            CREATE PROCEDURE GetManagerForEmployee
                @EmployeeId INT
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT 
                    M.Id,
                    M.FirstName,
                    M.LastName,
                    M.Designation
                FROM 
                    Employees E
                INNER JOIN 
                    Employees M ON E.ReportsToId = M.Id
                WHERE 
                    E.Id = @EmployeeId;
            END;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            DROP PROCEDURE IF EXISTS GetManagerForEmployee;";
            migrationBuilder.Sql(sql);
        }
    }
}
