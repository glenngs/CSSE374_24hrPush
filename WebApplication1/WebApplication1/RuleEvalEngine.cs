using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RuleEvalEngine
/// </summary>

namespace CourseValidationSystem
{
    public class RuleEvalEngine
    {

        private Dictionary<Course, Course> prerequisites;
        private Dictionary<Course, Course> corequisites;

        public RuleEvalEngine()
        {
            // Setup DB connection here
        }

        public CourseList evaluateCourseList(CourseList inputCourseList)
        {
            return new CourseList();
        }

    }
}