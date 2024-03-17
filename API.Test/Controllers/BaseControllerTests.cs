namespace API.Test.Controllers;

using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API;
using API.Exceptions;
using System.Net;
using API.Controllers;
using API.Exceptions;
using System.Security.Authentication;
using API.DTOs.Response;
using Domain.Models.Enums;
using Domain.Models;

public class BaseControllerTests
{
    private readonly Mock<ILogger<BaseControllerTests>> _loggerMock;
    private readonly BaseController<BaseControllerTests> _controller;

    public BaseControllerTests()
    {
        _loggerMock = new Mock<ILogger<BaseControllerTests>>();
        _controller = new BaseController<BaseControllerTests>();
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_OkResult_When_No_Exceptions_Are_Thrown()
    {
        // Arrange
        Func<Task<IActionResult>> function = async () => await Task.FromResult(new OkResult());

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_BadRequestException_Is_Thrown()
    {
        // Arrange
        var formValidationError = new List<KeyValuePair<string, object>>();
        Func<Task<IActionResult>> function = async () => throw new BadRequestException("Test exception", formValidationError);

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
    }


    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_HttpRequestException_Is_Thrown()
    {
        // Arrange
        Func<Task<IActionResult>> function = async () => throw new HttpRequestException();

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_DuplicateException_Is_Thrown()
    {
        // Arrange
        var formValidationError = new List<KeyValuePair<string, object>>();
        Func<Task<IActionResult>> function = async () => throw new DuplicateException("Test exception", formValidationError);

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.Conflict, objectResult.StatusCode);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_FormValidationException_Is_Thrown()
    {
        // Arrange
        var formValidationError = new List<KeyValuePair<string, object>>();
        Func<Task<IActionResult>> function = async () => throw new FormValidationException("Test exception", formValidationError);

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_AuthenticationException_Is_Thrown()
    {
        // Arrange
        Func<Task<IActionResult>> function = async () => throw new AuthenticationException();

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_Exception_Is_Thrown()
    {
        // Arrange
        Func<Task<IActionResult>> function = async () => throw new Exception();

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_With_Correct_Content_When_Exception_Is_Thrown()
    {
        // Arrange
        var exceptionMessage = "Test exception";
        Func<Task<IActionResult>> function = async () => throw new Exception(exceptionMessage);

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        var response = Assert.IsType<BaseResponseDto<Error>>(objectResult.Value);
        Assert.Equal(ErrorType.UnknownError, response.Error.Type);
        Assert.Equal(exceptionMessage, response.Error.ErrorMessage);
    }

    [Fact]
    public async Task TryExecuteAsync_Should_Return_ErrorResponse_When_ValidationException_Is_Thrown()
    {
        // Arrange
        var formValidationError = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("Field", "Error") };
        Func<Task<IActionResult>> function = async () => throw new FormValidationException("Test exception", formValidationError);

        // Act
        var result = await _controller.TryExecuteAsync(function, _loggerMock.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        var response = Assert.IsType<BaseResponseDto<Error>>(objectResult.Value);
        Assert.Equal(ErrorType.ValidationError, response.Error.Type);
        Assert.Equal("Test exception", response.Error.ErrorMessage);
        Assert.Null(response.Error.FormValidationError); // Expect a null value
    }
}

