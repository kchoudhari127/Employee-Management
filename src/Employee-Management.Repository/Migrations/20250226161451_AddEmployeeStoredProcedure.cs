using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE AddEmployee
                @FirstName NVARCHAR(50),
                @LastName NVARCHAR(50),
                @Designation NVARCHAR(50),
                @ReportsToId INT = NULL,
                @AddressTable AddressType READONLY
            AS
            BEGIN
                SET NOCOUNT ON;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    DECLARE @EmployeeId INT;

                    -- Validate ReportsToId
                    IF @ReportsToId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Employees WHERE Id = @ReportsToId)
                    BEGIN
                        THROW 50001, 'Invalid ReportsToId. The specified ReportsToId does not exist.', 1;
                    END

                    INSERT INTO Employees (FirstName, LastName, Designation, ReportsToId)
                    VALUES (@FirstName, @LastName, @Designation, @ReportsToId);

                    SET @EmployeeId = SCOPE_IDENTITY();

                    INSERT INTO Addresses (City, Area, PinCode, EmployeeId)
                    SELECT City, Area, PinCode, @EmployeeId
                    FROM @AddressTable;

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                    -- Rethrow the error
                    THROW;
                END CATCH
            END;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE AddEmployee;");
        }
    }
}
