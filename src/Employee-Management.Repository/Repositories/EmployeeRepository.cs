using Employee_Management.Core.Entities;
using Employee_Management.Core.Interfaces;
using Employee_Management.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {

            var addressDataTable = new DataTable();
            addressDataTable.Columns.Add("City", typeof(string));
            addressDataTable.Columns.Add("Area", typeof(string));
            addressDataTable.Columns.Add("PinCode", typeof(string));

            foreach (var address in employee.Addresses)
            {
                addressDataTable.Rows.Add(address.City, address.Area, address.PinCode);
            }

            var parameters = new[]
        {
            new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = employee.FirstName },
            new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = employee.LastName },
            new SqlParameter("@Designation", SqlDbType.NVarChar) { Value = employee.Designation },
            new SqlParameter("@ReportsToId", SqlDbType.Int) { Value = (object)employee.ReportsToId ?? DBNull.Value },
            new SqlParameter("@AddressTable", SqlDbType.Structured)
            {
                TypeName = "AddressType",
                Value = addressDataTable
            }
        };

            return await _context.Database.ExecuteSqlRawAsync("EXEC AddEmployee @FirstName, @LastName, @Designation, @ReportsToId, @AddressTable", parameters);

        }

        public async Task<int> UpdateAddressAsync(Address address)
        {
            var parameters = new[]
            {
               new SqlParameter("@AddressId", SqlDbType.Int) { Value = address.Id },
               new SqlParameter("@City", SqlDbType.NVarChar) { Value = address.City },
               new SqlParameter("@Area", SqlDbType.NVarChar) { Value = address.Area },
               new SqlParameter("@PinCode", SqlDbType.NVarChar) { Value = address.PinCode }
            };

             int rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                   "EXEC UpdateAddress @AddressId, @City, @Area, @PinCode",
                   parameters
               );

            return rowsAffected;

        }

<<<<<<< HEAD
        public async Task<List<Address>> GetEmployeeAddressesAsync(int employeeId)
        {
            var addressList = new List<Address>();

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetEmployeeAddresses";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            addressList.Add(new Address
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                Area = reader.GetString(reader.GetOrdinal("Area")),
                                PinCode = reader.GetString(reader.GetOrdinal("PinCode")),
                                EmployeeId = employeeId // This can be set directly since it's passed as a parameter
                            });
                        }
                    }
                }
            }

            return addressList;
        }
=======
>>>>>>> a2ad6e61066e0de017e7c58fc38960ec8de31505
    }
}
