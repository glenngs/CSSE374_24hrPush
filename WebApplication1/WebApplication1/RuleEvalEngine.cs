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

        private Dictionary<string, List<string>> prerequisites;
        private Dictionary<string, List<string>> corequisites;
        private Dictionary<string, int> termOffered;

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