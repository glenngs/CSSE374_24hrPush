using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for CourseListFactory
/// </summary>

namespace CourseValidationSystem
{
    public class CourseListFactory
    {
        public CourseListFactory()
        {
            
        }

        public CourseList parseJsonToCourseList(string inputJsonString)
        {
            List<UIInputDataInterfaceObject> parsedTable;
            try
            {
                parsedTable = JsonConvert.DeserializeObject<List<UIInputDataInterfaceObject>>(inputJsonString);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to decode input data - Input data malformed?");
            }

            try
            {
                CourseList list = new CourseList();

                foreach (UIInputDataInterfaceObject toParse in parsedTable)
                {
                    string courseId = toParse.Class.ToLower();
                    int yearSet = Convert.ToInt32(toParse.TermCode.Substring(0, 4));
                    int termSet = Convert.ToInt32(toParse.TermCode.Substring(4, 2));
                    list.addCourse(new Course(courseId, yearSet, termSet));
                }

                return list;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to parse input data - Input data malformed?");
            }
        }
    }
}