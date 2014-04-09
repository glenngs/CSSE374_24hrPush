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

        private CourseListFactory factory;

        public UIDataParser(RuleEvalEngine evalEngine)
        {
            this.evaluationEngine = evalEngine;

            // Setup strategy here
            this.factory = new CourseListFactory();
        }

        public List<UIOutputDataInterfaceObject> parseInputDataGiveResponse(string inputJSONString)
        {
            // Parse input data here by using strategy
            CourseList parsedCourseList = factory.parseJsonToCourseList(inputJSONString);

            return evaluationEngine.evaluateCourseList(parsedCourseList);

            // Re-Summarize results into return JSON string
            
        }

    }
}