
namespace Candidates.Backend.Application.Exceptions
{
    public class BaseAppException : Exception
    {
        public BaseAppException() : base() { }
        public BaseAppException(string message) : base(message) { }
        public BaseAppException(string message, Exception innerException) : base(message, innerException) { }
        public BaseAppException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
