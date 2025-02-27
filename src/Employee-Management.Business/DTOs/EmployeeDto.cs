using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.DTOs
{
    public class EmployeeDto
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Designation is required.")]
        [MaxLength(100)]
        public string Designation { get; set; }
        public int? ReportsToId { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
