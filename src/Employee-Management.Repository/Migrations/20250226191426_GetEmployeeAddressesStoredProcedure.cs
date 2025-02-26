using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class GetEmployeeAddressesStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetEmployeeAddresses
                @EmployeeId INT
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT 
                    Id,
                    City,
                    Area,
                    PinCode
                FROM 
                    Addresses
                WHERE 
                    EmployeeId = @EmployeeId;
            END;
           ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE GetEmployeeAddresses;");
        }
    }
}
