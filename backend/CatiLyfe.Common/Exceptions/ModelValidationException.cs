using System;
using System.Collections.Generic;
using System.Text;

namespace CatiLyfe.Common.Exceptions
{
    public class ModelValidationException : CatiFailureException
    {
        public ModelValidationException(object payload) : base(400, "BAD REQUEST", "Failed to validate")
        {
            this.Payload = payload;
        }
    }
}
