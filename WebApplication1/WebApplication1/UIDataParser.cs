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
        //private strategy thing;
        private UIDataParsingStrategy strategy;

        public UIDataParser(RuleEvalEngine evalEngine)
        {
            this.evaluationEngine = evalEngine;

            // Setup strategy here
            strategy = new NonSessionStrategy();
        }

        public string parseInputDataGiveResponse(string inputJSONString)
        {
            // Parse input data here by using strategy
            CourseList parsedCourseList = strategy.parseInputDataString(inputJSONString);

            return evaluationEngine.evaluateCourseList(parsedCourseList);

            // Re-Summarize results into return JSON string
            
        }

    }
}