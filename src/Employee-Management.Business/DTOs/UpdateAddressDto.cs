using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.DTOs
{
    public class UpdateAddressDto
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string PinCode { get; set; }
    }
}
