using Employee_Management.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.DTOs
{
    public class AddressResponceDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<GetAddressDto> addresses { get; set; }
    }
}
