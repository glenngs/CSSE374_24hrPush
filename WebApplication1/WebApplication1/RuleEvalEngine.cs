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
        Invalid = 0x0,
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
        private DBConnection databaseConnection;

        private static CourseOfferingEnum defaultTermOffered = (CourseOfferingEnum.AllFall | CourseOfferingEnum.AllSpring | CourseOfferingEnum.AllWinter);

        List<PrereqCSVParser.CourseEntry> coursePrereqEntries;

        public RuleEvalEngine()
        {

            // Setup DB connection here
            this.databaseConnection = new DBConnection();

            // Get data from database
            DBConnection.DBAccessReturnValues returnvals = this.databaseConnection.getPrereqCoreqTermsDataFromDatabase();
            this.prerequisites = returnvals.prerequisites;
            this.corequisites = returnvals.corequisites;
            this.termOffered = returnvals.termOffered;

            // AND IT BEGINS
            this.coursePrereqEntries = PrereqCSVParser.processCSVPrereqData();
            

        }

        public PrereqCSVParser.CourseEntry findEntryForCourse(string courseName)
        {
            foreach (PrereqCSVParser.CourseEntry entry in this.coursePrereqEntries)
            {
                if (entry.courseName == courseName)
                {
                    return entry;
                }
            }
            return null;
        }

        public List<UIOutputDataInterfaceObject> evaluateCourseList(CourseList inputCourseList)
        {
            List<UIOutputDataInterfaceObject> outputData = new List<UIOutputDataInterfaceObject>();

            if (this.coursePrereqEntries != null)
            {
                //outputData.Add(new UIOutputDataInterfaceObject(this.coursePrereqEntries.Count.ToString(), "TEST", 0));
            }
            else
            {
                outputData.Add(new UIOutputDataInterfaceObject("NULL CSV", "ERROR", 0));
            }

            // For every course in their schedule
            foreach (Course currentCourse in inputCourseList.courseList)
            {
                // Check if this course has a set of prereqs in our list of evaluators
                PrereqCSVParser.CourseEntry entry = this.findEntryForCourse(currentCourse.courseId);
                if (entry == null)
                {
                    // No prereqs for this course - mark it valid and continue
                    continue;
                }

                EvaluationError err = entry.prereqStatement.evaluate(inputCourseList, currentCourse); 

                
                if (!err.isValid)
                {
                    outputData.Add(new UIOutputDataInterfaceObject(err.stringError, currentCourse.courseId, err.errorCode));
                }
            }


            /*
            // For every course in their schedule
            foreach (Course currentCourse in inputCourseList.courseList)
            {
                // Failure Accumulation Variables
                bool preAndCoRequisitesValid = true;
                List<string> missingCourses = new List<string>();
                List<string> outOfOrderCourses = new List<string>();

                // Retrieve Prereqs/Coreqs
                List<string> coursePrereqs;

                if (!this.prerequisites.ContainsKey(currentCourse.courseId))
                {
                    coursePrereqs = new List<string>();
                }
                else
                {
                    coursePrereqs = prerequisites[currentCourse.courseId];
                }

                List<string> courseCoreqs;

                if (!this.corequisites.ContainsKey(currentCourse.courseId))
                {
                    courseCoreqs = new List<string>();
                }
                else
                {
                    courseCoreqs = corequisites[currentCourse.courseId];
                }
                



                //
                // Check all prerequisites here
                //

                preAndCoRequisitesValid = checkPrerequisites(inputCourseList, currentCourse, coursePrereqs, preAndCoRequisitesValid, missingCourses, outOfOrderCourses);

                // 
                // Check all Corequisites here
                // 

                preAndCoRequisitesValid = checkCorequisites(inputCourseList, currentCourse, courseCoreqs, preAndCoRequisitesValid, missingCourses, outOfOrderCourses);


                if (preAndCoRequisitesValid)
                {
                    checkTermOffered(outputData, currentCourse);
                }
                else
                {
                    
                    // This code can be replaced by an actual error management/system response system
                    string missingClassesError = "Missing Prerequisite Classes: " + string.Join(", ", missingCourses.ToArray());
                    string classesOutOfOrder = " Out of Order Classes: " + string.Join(", ", outOfOrderCourses.ToArray());
                    if (missingCourses.Count != 0)
                    {
                        if (outOfOrderCourses.Count != 0)
                        {
                            outputData.Add(new UIOutputDataInterfaceObject((missingClassesError + classesOutOfOrder).ToUpper(), (currentCourse.courseId).ToUpper(), 11));
                        }

                        outputData.Add(new UIOutputDataInterfaceObject((missingClassesError).ToUpper(), (currentCourse.courseId).ToUpper(), 11));

                    }
                    else if (outOfOrderCourses.Count != 0)
                    {
                        outputData.Add(new UIOutputDataInterfaceObject((classesOutOfOrder).ToUpper(), (currentCourse.courseId).ToUpper(), 10));
                    }
                    
                    
                }
            }
            */
            return outputData;
        }

        private void checkTermOffered(List<UIOutputDataInterfaceObject> outputData, Course currentCourse)
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
                outputData.Add(new UIOutputDataInterfaceObject(("Class not offered on desired term " + thisCourseOffered + " ... " + termClassOffered).ToUpper(), (currentCourse.courseId).ToUpper(), 20));
            }
        }

        private static bool checkCorequisites(CourseList inputCourseList, Course currentCourse, List<string> courseCoreqs, bool preAndCoRequisitesValid, List<string> missingCourses, List<string> outOfOrderCourses)
        {
            // For each corequisite
            foreach (string coreqToCheckFor in courseCoreqs)
            {
                // If they didn't put the course on their plan at all
                if (!inputCourseList.containsCourseId(coreqToCheckFor))
                {
                    preAndCoRequisitesValid = false;
                    missingCourses.Add(coreqToCheckFor);
                }
                if (preAndCoRequisitesValid)
                {
                    // If they did put it on their schedule, is it before this class?
                    Course coReqCourse = inputCourseList.courseList.Find(i => i.Equals(coreqToCheckFor));


                    // If the year is after this, fail
                    if (coReqCourse.year > currentCourse.year)
                    {
                        preAndCoRequisitesValid = false;
                        outOfOrderCourses.Add(coreqToCheckFor);
                    }
                    else if (coReqCourse.year == currentCourse.year)
                    {
                        if (coReqCourse.term > currentCourse.term)
                        {
                            preAndCoRequisitesValid = false;
                            outOfOrderCourses.Add(coreqToCheckFor);
                        }
                    }
                    // If we made it to here without any strings being added, awesome!
                }
            }
            return preAndCoRequisitesValid;
        }

        private static bool checkPrerequisites(CourseList inputCourseList, Course currentCourse, List<string> coursePrereqs, bool preAndCoRequisitesValid, List<string> missingCourses, List<string> outOfOrderCourses)
        {
            foreach (string prereqToCheckFor in coursePrereqs)
            {
                // If they didn't put the course on their plan at all
                if (!inputCourseList.containsCourseId(prereqToCheckFor))
                {
                    preAndCoRequisitesValid = false;
                    missingCourses.Add(prereqToCheckFor);
                }
                if (preAndCoRequisitesValid)
                {
                    // If they did put it on their schedule, is it before this class?
                    Course preReqCourse = inputCourseList.courseList.Find(i => i.Equals(prereqToCheckFor));

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
            return preAndCoRequisitesValid;
        }

        private CourseOfferingEnum parseYearTermToEnum(int year, int inputNum)
        {
            CourseOfferingEnum currentEnum = CourseOfferingEnum.Invalid;
            
            if (inputNum == 10)
            {
                currentEnum |= CourseOfferingEnum.AllFall;
            }
            if (inputNum == 20)
            {
                currentEnum |= CourseOfferingEnum.AllWinter;
            }
            if (inputNum == 30)
            {
                currentEnum |= CourseOfferingEnum.AllSpring;
            }
                        
            if (year % 2 == 0)
            {
                // Even year

                if (inputNum == 10)
                {
                    currentEnum |= CourseOfferingEnum.EvenFall;
                }
                if (inputNum == 20)
                {
                    currentEnum |= CourseOfferingEnum.EvenWinter;
                }
                if (inputNum == 30)
                {
                    currentEnum |= CourseOfferingEnum.EvenSpring;
                }
            }

            else
            {
                // Odd year

                if (inputNum == 10)
                {
                    currentEnum |= CourseOfferingEnum.OddFall;
                }
                if (inputNum == 20)
                {
                    currentEnum |= CourseOfferingEnum.OddWinter;
                }
                if (inputNum == 30)
                {
                    currentEnum |= CourseOfferingEnum.OddSpring;
                }
            }
            
            return currentEnum;
        }
    }
}