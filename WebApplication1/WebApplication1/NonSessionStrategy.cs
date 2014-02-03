using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NonSessionStrategy
/// </summary>
namespace CourseValidationSystem
{
    public class NonSessionStrategy : UIDataParsingStrategy
    {
        private CourseListFactory factory;

        public NonSessionStrategy()
        {
            this.factory = new CourseListFactory();
        }

        public CourseList parseInputDataString(string inputJsonString)
        {
            return factory.parseJsonToCourseList(inputJsonString);
        }
    }
}