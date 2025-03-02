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

        [Fact]
        public async Task GetEmployeeAddressesAsync_ShouldReturnEmployeeAddresses()
        {
            // Arrange
            var employeeId = 1;
            var addressList = new List<GetAddressDto>
            {
                new GetAddressDto { Id = 1, City = "Pune", Area = "123 Main St", PinCode = "411001",EmployeeId= employeeId }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAddressesAsync(employeeId))
                .ReturnsAsync(addressList);

            // Act
            var result = await _employeeService.GetEmployeeAddressesAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Pune", result[0].City);
            Assert.Equal("123 Main St", result[0].Area);
            Assert.Equal("411001", result[0].PinCode);
            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAddressesAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task GetManagerForEmployeeAsync_ShouldReturnManagerDetails()
        {
            // Arrange
            var employeeId = 1;
            var managerList = new List<ManagerDto>
            {
                new ManagerDto { Id = 1, FirstName = "John", LastName = "Doe", Designation = "Manager" }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetManagerForEmployeeAsync(employeeId))
                .ReturnsAsync(managerList);

            // Act
            var result = await _employeeService.GetManagerForEmployeeAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("John", result[0].FirstName);
            Assert.Equal("Doe", result[0].LastName);
            Assert.Equal("Manager", result[0].Designation);
            _employeeRepositoryMock.Verify(repo => repo.GetManagerForEmployeeAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task GetEmployeesReportingToManagerAsync_ShouldReturnEmplyeesDetails()
        {
            // Arrange
            var employeeId = 1;
            var employeeList = new List<GetEmployeeDto>
            {
                new GetEmployeeDto { Id = 1, FirstName = "Rohit", LastName = "Sharma", Designation = "Manager" }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesReportingToManagerAsync(employeeId))
                .ReturnsAsync(employeeList);

            // Act
            var result = await _employeeService.GetEmployeesReportingToManagerAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Rohit", result[0].FirstName);
            Assert.Equal("Sharma", result[0].LastName);
            Assert.Equal("Manager", result[0].Designation);
            _employeeRepositoryMock.Verify(repo => repo.GetEmployeesReportingToManagerAsync(employeeId), Times.Once);
        }

    }
}
