namespace API.Exceptions
{
    public class DuplicateException : ArgumentException
    {
        public IList<KeyValuePair<string, object>> FormValidationError { get; set; }
        public DuplicateException(string message, IList<KeyValuePair<string, object>> formValidationError) : base(message)
        {
            formValidationError = formValidationError ?? new List<KeyValuePair<string, object>>();
        }
    }
}
