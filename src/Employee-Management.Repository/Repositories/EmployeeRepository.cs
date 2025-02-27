using Employee_Management.Core.DTOs;
using Employee_Management.Core.Entities;
using Employee_Management.Core.Interfaces;
using Employee_Management.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            try
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

                _logger.LogInformation("Executing SQL command to add employee.");
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC AddEmployee @FirstName, @LastName, @Designation, @ReportsToId, @AddressTable", parameters);
                return result;
            }
            catch (DbUpdateException ex)
            {
                 _logger.LogError(ex, "A database error occurred while adding the employee.");

                // Rethrow the exception to be caught by the middleware
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the employee.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee.");
                throw;
            }
        }

        public async Task<int> UpdateAddressAsync(Address address)
        {
            try
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
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the employee.");

                // Rethrow the exception to be caught by the middleware
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the employee.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the employee.");
                throw;
            }

        }


        public async Task<List<GetAddressDto>> GetEmployeeAddressesAsync(int employeeId)
        {
            try
            { 
                 var addressList = new List<GetAddressDto>();

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
                                 addressList.Add(new GetAddressDto
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
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Employee Addresses.");

                // Rethrow the exception to be caught by the middleware
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Employee Addresses.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Employee Addresses.");
                throw;
            }
        }

        public async Task<List<GetEmployeeDto>> GetEmployeesReportingToManagerAsync(int managerId)
        {
            try
            { 
                 var employeeList = new List<GetEmployeeDto>();

                 using (var connection = _context.Database.GetDbConnection())
                 {
                       await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "GetEmployeesReportingToManager";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ManagerId", managerId));

                       using (var reader = await command.ExecuteReaderAsync())
                       {
                           while (await reader.ReadAsync())
                           {
                                employeeList.Add(new GetEmployeeDto
                               {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Designation = reader.GetString(reader.GetOrdinal("Designation"))
                                });
                           }
                       }
                    }
                 }

                return employeeList;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Employees Reporting To Managers.");

                // Rethrow the exception to be caught by the middleware
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Employees Reporting To Managers.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Employees Reporting To Managers.");
                 throw;
            }
        }

        public async Task<List<ManagerDto>> GetManagerForEmployeeAsync(int employeeId)
        {
            try
            { 
                var manager = new List<ManagerDto>();

                using (var connection = _context.Database.GetDbConnection())
                {
                     await connection.OpenAsync();

                   using (var command = connection.CreateCommand())
                   {
                       command.CommandText = "GetManagerForEmployee";
                       command.CommandType = System.Data.CommandType.StoredProcedure;
                       command.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));

                        using (var reader = await command.ExecuteReaderAsync())
                       {
                           if (await reader.ReadAsync())
                           {
                               manager.Add( new ManagerDto
                               {
                                   Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                   FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                   LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                   Designation = reader.GetString(reader.GetOrdinal("Designation"))
                               });
                           }
                        }
                   }
                }

                  return manager;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Manager for an Employee.");

                // Rethrow the exception to be caught by the middleware
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Manager for an Employee.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "A database error occurred getting Manager for an Employee.");
                throw;
            }
        }


    }
}
