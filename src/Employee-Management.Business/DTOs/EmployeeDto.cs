using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.DTOs
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public int? ReportsToId { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
