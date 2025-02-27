using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.DTOs
{
    public class UpdateAddressDto
    {
        public int AddressId { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Area is required.")]
        [MaxLength(100)]
        public string Area { get; set; }
        [Required(ErrorMessage = "PinCode is required.")]
        [MaxLength(20)]
        public string PinCode { get; set; }
    }
}
