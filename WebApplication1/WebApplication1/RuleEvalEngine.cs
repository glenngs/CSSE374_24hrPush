using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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

        public string evaluateCourseList(CourseList inputCourseList)
        {
            foreach (Course course in inputCourseList.courseList)
            {
                // Check Prereqs/Coreqs
                List<string> coursePrereqs = prerequisites[course.courseId];

                bool hasAllPrereqs = true;

                foreach (string checkFor in coursePrereqs)
                {
                    // If they didn't put the course on their plan at all
                    List<Course> listOfCourses = inputCourseList.courseList;

                    
                    

                    if (!inputCourseList.courseList.Contains<string>(checkFor))
                    {

                    }
                }


                // Ensure offered on correct term
            }

            List<UIOutputDataInterfaceObject> outputData = new List<UIOutputDataInterfaceObject>();

            return JsonConvert.SerializeObject(outputData);
        }
    }
}