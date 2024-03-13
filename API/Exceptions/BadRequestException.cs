namespace API.Exceptions
{
    public class BadRequestException : ArgumentException
    {
        public IList<KeyValuePair<string, object>> FormValidationError { get; set; }
        public BadRequestException(string message, IList<KeyValuePair<string, object>> formValidationError) : base(message)
        {
            formValidationError = formValidationError ?? new List<KeyValuePair<string, object>>();
        }
    }
}
