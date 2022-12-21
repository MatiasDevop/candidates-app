using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.Backend.Application.Exceptions
{
    public class ValidationException : BaseAppException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }
        public IDictionary<string, string[]> Failures { get; }
    }
    public class AlreadyExistsException : BaseAppException
    {
        public AlreadyExistsException(string name)
            : base($"\"{name}\" already exists.")
        {
        }
    }

}
