using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for UIDataParser
/// </summary>

namespace CourseValidationSystem
{
    public class UIDataParser
    {

        private RuleEvalEngine evaluationEngine;
        // private strategy thing

        public UIDataParser(RuleEvalEngine evalEngine)
        {
            this.evaluationEngine = evalEngine;

            // Setup strategy here
        }

        public string parseInputDataGiveResponse(string inputJSONString)
        {
            // Parse input data here by using strategy
            CourseList parsedCourseList = new CourseList();

            evaluationEngine.evaluateCourseList(parsedCourseList);

            // Re-Summarize results into return JSON string
            return "Ping";
        }

    }
}