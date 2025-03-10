﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Core.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Area is required.")]
        [MaxLength(100)]
        public string Area { get; set; }

        [Required(ErrorMessage = "PinCode is required.")]
        [MaxLength(20)]
        public string PinCode { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
