using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseValidationSystem
{
    public class EvaluationError
    {
        public string stringError;
        public bool isValid;
        public int errorCode;

        public EvaluationError(bool isValid, string stringError, int errorCode)
        {
            this.isValid = isValid;
            this.stringError = stringError;
            this.errorCode = errorCode;
        }
    }
}