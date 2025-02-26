using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE TYPE AddressType AS TABLE
            (
                City NVARCHAR(100),
                Area NVARCHAR(100),
                PinCode NVARCHAR(10)
            );
          ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP TYPE AddressType;
        ");
        }
    }
}
