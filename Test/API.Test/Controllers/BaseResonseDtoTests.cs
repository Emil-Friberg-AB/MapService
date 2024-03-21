using API.DTOs.Response;
using Domain.Models;
using Xunit;

namespace API.Test.Controllers;

public class BaseResponseDtoTests
{
    [Fact]
    public void BaseResponseDto_Success_ReturnsTrue_WhenErrorIsNull()
    {
        // Arrange
        var dto = new BaseResponseDto<string>
        {
            Result = "Test",
            Error = null
        };

        // Act
        var success = dto.Success;

        // Assert
        Assert.True(success);
    }

    [Fact]
    public void BaseResponseDto_Success_ReturnsFalse_WhenErrorIsNotNull()
    {
        // Arrange
        var dto = new BaseResponseDto<string>
        {
            Result = "Test",
            Error = new Error()
        };

        // Act
        var success = dto.Success;

        // Assert
        Assert.False(success);
    }
}
