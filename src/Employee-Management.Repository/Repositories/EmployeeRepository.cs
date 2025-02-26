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
    }
}
