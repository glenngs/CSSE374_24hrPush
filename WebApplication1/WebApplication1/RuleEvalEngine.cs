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
    [Flags]
    public enum CourseOfferingEnum : int
    {
        AllFall = 0x1,
        AllWinter = 0x1 << 1,
        AllSpring = 0x1 << 2,
        AllSummer = 0x1 << 3,
        EvenFall = 0x1 << 4,
        EvenWinter = 0x1 << 5,
        EvenSpring = 0x1 << 6,
        EvenSummer = 0x1 << 7,
        OddFall = 0x1 << 8,
        OddWinter = 0x1 << 9,
        OddSpring = 0x1 << 10,
        OddSummer = 0x1 << 11,
        Unknown = 0x1 << 12
    }

    public class RuleEvalEngine
    {

        private Dictionary<string, List<string>> prerequisites;
        private Dictionary<string, List<string>> corequisites;
        private Dictionary<string, CourseOfferingEnum> termOffered;

        private static CourseOfferingEnum defaultTermOffered = (CourseOfferingEnum.AllFall | CourseOfferingEnum.AllSpring | CourseOfferingEnum.AllWinter);


        public RuleEvalEngine()
        {

            prerequisites = new Dictionary<string, List<string>>();
            corequisites = new Dictionary<string, List<string>>();

            // Setup DB connection here
            
            List<string> tempList = new List<string>();
            prerequisites.Add("csse120", new List<string>(tempList));
            prerequisites.Add("csse132", new List<string>(tempList));
            prerequisites.Add("csse221", new List<string>(tempList));
            tempList.Add("csse120");
            prerequisites.Add("csse220", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            prerequisites.Add("csse230", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse120");
            tempList.Add("csse132");
            prerequisites.Add("csse232", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            prerequisites.Add("csse241", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma275");
            prerequisites.Add("csse304", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("ma212");
            prerequisites.Add("csse325", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("csse232");
            prerequisites.Add("csse332", new List<string>(tempList));
            tempList.Clear();            
            tempList.Add("ma275");
            prerequisites.Add("csse333", new List<string>(tempList));
            tempList.Clear();          
            tempList.Add("ma212");
            prerequisites.Add("csse3335", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("ma212");
            prerequisites.Add("csse351", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("rh330");
            prerequisites.Add("csse371", new List<string>(tempList));
            tempList.Clear();            
            prerequisites.Add("csse372", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma275");
            prerequisites.Add("csse373", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse371");            
            prerequisites.Add("csse374", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse374");          
            prerequisites.Add("csse375", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");            
            prerequisites.Add("csse376", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse332");           
            prerequisites.Add("csse402", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse304");            
            prerequisites.Add("csse403", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse304");
            tempList.Add("csse232");
            tempList.Add("csse474");
            prerequisites.Add("csse404", new List<string>((tempList)));
            tempList.Clear();           
            tempList.Add("csse230");        
            prerequisites.Add("csse413", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            prerequisites.Add("csse432", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse333");
            prerequisites.Add("csse433", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse332");
            tempList.Add("ma275");
            prerequisites.Add("csse442", new List<string>(tempList));         
            tempList.Clear();
            tempList.Add("csse351");          
            prerequisites.Add("csse451", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse413");            
            prerequisites.Add("csse453", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse120");
            tempList.Add("ma212");
            prerequisites.Add("csse461", new List<string>(tempList));
            tempList.Clear();            
            tempList.Add("ma212");
            prerequisites.Add("csse463", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma375");
            prerequisites.Add("csse473", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma375");
            prerequisites.Add("csse474", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse374");
            prerequisites.Add("csse477", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("ma275");
            prerequisites.Add("csse479", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            prerequisites.Add("csse481", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("rh330");
            prerequisites.Add("csse487", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse487");
            prerequisites.Add("csse488", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse488");
            prerequisites.Add("csse489", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse371");
            prerequisites.Add("csse497", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse497");
            tempList.Add("csse374");
            prerequisites.Add("csse498", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse498");
            prerequisites.Add("csse499", new List<string>(tempList));
            tempList.Clear();

            corequisites.Add("csse120", new List<string>());
            corequisites.Add("csse132", new List<string>());
            corequisites.Add("csse220", new List<string>());
            corequisites.Add("csse221", new List<string>());
            corequisites.Add("csse230", new List<string>());
            corequisites.Add("csse232", new List<string>());
            corequisites.Add("csse241", new List<string>());
            corequisites.Add("csse290", new List<string>());
            corequisites.Add("csse304", new List<string>());
            corequisites.Add("csse325", new List<string>());
            corequisites.Add("csse332", new List<string>());
            tempList.Add("csse230");
            corequisites.Add("csse333", new List<string>(tempList));
            corequisites.Add("csse335", new List<string>());
            corequisites.Add("csse351", new List<string>());
            corequisites.Add("csse371", new List<string>());
            corequisites.Add("csse372", new List<string>());
            corequisites.Add("csse373", new List<string>());
            corequisites.Add("csse374", new List<string>());
            corequisites.Add("csse375", new List<string>());
            corequisites.Add("csse376", new List<string>());
            corequisites.Add("csse402", new List<string>());
            corequisites.Add("csse403", new List<string>());
            corequisites.Add("csse404", new List<string>());
            corequisites.Add("csse413", new List<string>());
            corequisites.Add("csse432", new List<string>());
            corequisites.Add("csse433", new List<string>());
            corequisites.Add("csse442", new List<string>());
            corequisites.Add("csse451", new List<string>());
            corequisites.Add("csse453", new List<string>());
            corequisites.Add("csse461", new List<string>());
            corequisites.Add("csse463", new List<string>());
            corequisites.Add("csse473", new List<string>());
            corequisites.Add("csse474", new List<string>());
            corequisites.Add("csse477", new List<string>());
            corequisites.Add("csse479", new List<string>());
            corequisites.Add("csse481", new List<string>());
            corequisites.Add("csse487", new List<string>());
            corequisites.Add("csse488", new List<string>());
            corequisites.Add("csse489", new List<string>());
            corequisites.Add("csse497", new List<string>());
            corequisites.Add("csse498", new List<string>());
            corequisites.Add("csse499", new List<string>());

            // Setup all courses offered all terms
            this.termOffered = new Dictionary<string, CourseOfferingEnum>();
            foreach (string courseID in corequisites.Keys)
            {
                termOffered.Add(courseID, (CourseOfferingEnum.AllFall | CourseOfferingEnum.AllSpring | CourseOfferingEnum.AllWinter));
            }


            

        }

        public List<UIOutputDataInterfaceObject> evaluateCourseList(CourseList inputCourseList)
        {
            List<UIOutputDataInterfaceObject> outputData = new List<UIOutputDataInterfaceObject>();

            foreach (Course currentCourse in inputCourseList.courseList)
            {
                // Check Prereqs/Coreqs
                List<string> coursePrereqs;

                if (!prerequisites.ContainsKey(currentCourse.courseId))
                {
                    coursePrereqs = new List<string>();
                }
                else
                {
                    coursePrereqs = prerequisites[currentCourse.courseId];
                }
                

                // Failure Variables
                bool preAndCoRequisitesValid = true;
                List<string> missingCourses = new List<string>();
                List<string> outOfOrderCourses = new List<string>();

                // For each prerequisite
                foreach (string prereqToCheckFor in coursePrereqs)
                {
                    // If they didn't put the course on their plan at all
                    if(!inputCourseList.containsString(prereqToCheckFor))
                    {
                        preAndCoRequisitesValid = false;
                        missingCourses.Add(prereqToCheckFor);
                    }
                    if (preAndCoRequisitesValid)
                    {
                        // If they did put it on their schedule, is it before this class?
                        Course preReqCourse = inputCourseList.findCourse(prereqToCheckFor);

                        // If the year is after this, fail
                        if (preReqCourse.year > currentCourse.year)
                        {
                            preAndCoRequisitesValid = false;
                            outOfOrderCourses.Add(prereqToCheckFor);
                        }
                        else if (preReqCourse.year == currentCourse.year)
                        {
                            if (preReqCourse.term >= currentCourse.term)
                            {
                                preAndCoRequisitesValid = false;
                                outOfOrderCourses.Add(prereqToCheckFor);
                            }
                        }
                        // If we made it to here without any strings being added, awesome!
                    }
                }

                // TODO:
                // Check all Corequisites here
                // 

                if (preAndCoRequisitesValid)
                {
                    // All prereqs on their schedule
                    // Ensure offered on possible term

                    CourseOfferingEnum termClassOffered;
                    if (!termOffered.ContainsKey(currentCourse.courseId))
                    {
                        termClassOffered = RuleEvalEngine.defaultTermOffered;
                        // TODO
                        // Throw Error Message
                    }
                    else
                    {
                        termClassOffered = termOffered[currentCourse.courseId];
                    }


                    CourseOfferingEnum thisCourseOffered = parseYearTermToEnum(currentCourse.year, currentCourse.term);
                    if ((thisCourseOffered & termClassOffered) == 0)
                    {
                        // Failed to find a term when this is offered
                        outputData.Add(new UIOutputDataInterfaceObject(("Class not offered on desired term " + thisCourseOffered + " ... " + termClassOffered).ToUpper(), (currentCourse.courseId).ToUpper()));
                    }
                    else{
                    
                        // Wow it all worked! 
                        //outputData.Add(new UIOutputDataInterfaceObject("OK", currentCourse.courseId));
                    }

                }
                else
                {
                    
                    string missingClassesError = "Missing PrerequisiteClasses: " + string.Join(", ", missingCourses.ToArray());
                    string classesOutOfOrder = " Out of Order Classes: " + string.Join(", ", outOfOrderCourses.ToArray());
                    string totalErrorMessage = (missingClassesError + classesOutOfOrder).ToUpper();
                    outputData.Add(new UIOutputDataInterfaceObject(totalErrorMessage, (currentCourse.courseId).ToUpper()));
                }
            }
            
            return outputData;
        }

        private CourseOfferingEnum parseYearTermToEnum(int year, int inputNum)
        {
            if (inputNum == 10)
            {
                return CourseOfferingEnum.AllFall;
            }
            if (inputNum == 20)
            {
                return CourseOfferingEnum.AllFall;
            }
            if (inputNum == 30)
            {
                return CourseOfferingEnum.AllFall;
            }
            return CourseOfferingEnum.Unknown;
        }






    }
}