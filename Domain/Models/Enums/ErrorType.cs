using System.ComponentModel;

namespace Domain.Models.Enums
{
    public enum ErrorType
    {
        [Description("Unknown Error")]
        UnknownError,

        [Description("HTTP Client Error")]
        HttpClientError,

        [Description("Duplicate Error")]
        DuplicateError,

        [Description("Argument Error")]
        ArgumentError,

        [Description("Validation Error")]
        ValidationError,

        [Description("Authentication Failed")]
        AuthenticationFailed
    }
}
