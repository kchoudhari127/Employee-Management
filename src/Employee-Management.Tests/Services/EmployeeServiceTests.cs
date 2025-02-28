using Moq;
using Employee_Management.API.Controllers;
using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
using Employee_Management.Business.Services;
using Employee_Management.Core.DTOs;
using Employee_Management.Core.Entities;
using Employee_Management.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Tests.Services
{
    public class EmployeeServiceTests
    {

        private readonly IEmployeeService _employeeService;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<ILogger<EmployeeService>> _loggerMock;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _loggerMock = new Mock<ILogger<EmployeeService>>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task EmployeeService_CallsRepositoryAddEmployeeAsync()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Designation = "Software Engineer",
                ReportsToId = 2,
                Addresses = new List<Address>
               {
                 new Address
                 {
                    City = "Pune",
                    Area = "123 Main St",
                    PinCode = "411001"
                 },
                  new Address
                 {
                     City = "Mumbai",
                     Area = "456 Another St",
                     PinCode = "400001"
                  }
               }
            };
           
            // Act
            await _employeeService.AddEmployeeAsync(employee);
            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddEmployeeAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task EmployeeService_CallsRepositoryUpdateMethod()
        {
            // Arrange

            var addresses = new Address
            {

                Id = 1,
                City = "Pune",
                Area = "123 Main St",
                PinCode = "411001"
                
            };


            // Act
            await _employeeService.UpdateAddressAsync(addresses);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.UpdateAddressAsync(It.IsAny<Address>()), Times.Once);
        }

    }
}
