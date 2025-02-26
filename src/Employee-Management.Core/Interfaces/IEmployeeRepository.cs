using Employee_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployeeAsync(Employee employee);
        Task<int> UpdateAddressAsync(Address address);
<<<<<<< HEAD
        Task<List<Address>> GetEmployeeAddressesAsync(int employeeId);
=======
>>>>>>> a2ad6e61066e0de017e7c58fc38960ec8de31505
    }
}
