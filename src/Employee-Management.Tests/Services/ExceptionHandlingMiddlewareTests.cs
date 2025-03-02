using Employee_Management.API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Tests.Services
{
    public class ExceptionHandlingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _mockRequestDelegate;
        private readonly Mock<ILogger<ExceptionHandlingMiddleware>> _mockLogger;
        public ExceptionHandlingMiddlewareTests()
        {
            _mockRequestDelegate = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        }
        [Fact]
        public async Task InvokeAsync_ShouldHandleExceptionAndReturnInternalServerError()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var middleware = new ExceptionHandlingMiddleware(
                next: (innerHttpContext) => Task.FromException(new Exception("Test exception")),
                logger: _mockLogger.Object
            );

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseBody);

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            Assert.False((bool)responseJson.Success);
            Assert.Equal("An unexpected error occurred. Please try again later.", (string)responseJson.Message);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)responseJson.StatusCode);
        }
    }
}
