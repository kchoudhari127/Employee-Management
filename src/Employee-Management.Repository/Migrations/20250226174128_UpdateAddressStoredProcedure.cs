using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddressStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateAddress
                @AddressId INT,
                @City NVARCHAR(100),
                @Area NVARCHAR(100),
                @PinCode NVARCHAR(10)
            AS
            BEGIN
                SET NOCOUNT ON;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    UPDATE Addresses
                    SET 
                        City = @City,
                        Area = @Area,
                        PinCode = @PinCode
                    WHERE Id = @AddressId;

                    IF @@ROWCOUNT = 0
                    BEGIN
                        -- No rows were updated, possibly because AddressId does not exist
                        THROW 50002, 'Address not found.', 1;
                    END

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
            migrationBuilder.Sql("DROP PROCEDURE UpdateAddress;");
        }
    }
}
