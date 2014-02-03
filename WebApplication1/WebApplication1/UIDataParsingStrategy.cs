using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UIDataParsingStrategy
/// </summary>

namespace CourseValidationSystem
{
    public interface UIDataParsingStrategy
    {

        CourseList parseInputDataString(string inputJsonString);


        /*
        public UIDataParsingStrategy()
        {
            //
            // TODO: Add constructor logic here
            //
        }
         */
    }
}