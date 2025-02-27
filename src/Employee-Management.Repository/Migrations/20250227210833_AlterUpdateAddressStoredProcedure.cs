using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AlterUpdateAddressStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER PROCEDURE UpdateAddress
                         @AddressId INT,
                         @City NVARCHAR(100),
                         @Area NVARCHAR(100),
                         @PinCode NVARCHAR(10)
            AS
            BEGIN
                  -- Remove SET NOCOUNT ON; to get the correct number of rows affected
                  -- SET NOCOUNT ON;

                DECLARE @RowsAffected INT = 0;

               BEGIN TRY
               BEGIN TRANSACTION;

             UPDATE Addresses
             SET 
               City = @City,
               Area = @Area,
               PinCode = @PinCode
               WHERE Id = @AddressId;

               SET @RowsAffected = @@ROWCOUNT;

               COMMIT TRANSACTION;
            END TRY
            BEGIN CATCH
              ROLLBACK TRANSACTION;
              -- Rethrow unexpected errors
               THROW;
            END CATCH

               -- Return the number of rows affected
             SELECT @RowsAffected AS RowsAffected;
            END;
          ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE UpdateAddress;");
        }
    }
}
