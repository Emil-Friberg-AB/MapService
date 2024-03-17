using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using Domain.Models.Enums;
using API.Exceptions;
using Domain.Models;
using API.DTOs.Response;

namespace API.Controllers
{
    public class BaseController<T> : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> TryExecuteAsync<TResult>(Func<Task<TResult>> function, ILogger<T> logger) where TResult : IActionResult
        {
            try
            {
                return await function();
            }
            catch (HttpRequestException ex)
            {
                logger.LogError("Exception in " + typeof(T).Name + ".", ex);
                return ErrorResponse(ex, ErrorType.HttpClientError, HttpStatusCode.InternalServerError, new List<KeyValuePair<string, object>>());
            }
            catch (DuplicateException ex2)
            {
                logger.LogInformation("Duplicate exception in {name}. {ex}", typeof(T).Name, ex2.ToString());
                return ErrorResponse(ex2, ErrorType.DuplicateError, HttpStatusCode.Conflict, ex2.FormValidationError);
            }
            catch (BadRequestException ex3)
            {
                logger.LogInformation("Bad request exception in {name}. {ex}", typeof(T).Name, ex3.ToString());
                return ErrorResponse(ex3, ErrorType.ArgumentError, HttpStatusCode.BadRequest, ex3.FormValidationError);
            }
            catch (FormValidationException ex4)
            {
                logger.LogInformation("Bad request exception in {name}. {ex}", typeof(T).Name, ex4.ToString());
                return ErrorResponse(ex4, ErrorType.ValidationError, HttpStatusCode.BadRequest, ex4.FormValidationError);
            }
            catch (ValidationException ex5)
            {
                logger.LogInformation("Validation exception in {name}. {ex}", typeof(T).Name, ex5.ToString());
                return ErrorResponse(ex5, ErrorType.ValidationError, HttpStatusCode.BadRequest, new List<KeyValuePair<string, object>>());
            }
            catch (AuthenticationException ex6)
            {
                logger.LogInformation("Authentication exception in {name}. {ex}", typeof(T).Name, ex6.ToString());
                return ErrorResponse(ex6, ErrorType.AuthenticationFailed, HttpStatusCode.InternalServerError, new List<KeyValuePair<string, object>>());
            }
            catch (Exception ex7)
            {
                logger.LogError("Exception in " + typeof(T).Name + ".", ex7);
                return ErrorResponse(ex7, ErrorType.UnknownError, HttpStatusCode.InternalServerError, new List<KeyValuePair<string, object>>());
            }
        }

        private IActionResult ErrorResponse(Exception ex, ErrorType errorType, HttpStatusCode statusCode, IList<KeyValuePair<string, object>> formValidationError)
        {
            return StatusCode((int)statusCode, new BaseResponseDto<Error>
            {
                Error = new Error
                {
                    Type = errorType,
                    ErrorMessage = ex.Message,
                    FormValidationError = formValidationError
                }
            });
        }
    }
}
