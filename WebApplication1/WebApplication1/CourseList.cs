using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassList
/// </summary>

namespace CourseValidationSystem
{
    public class CourseList
    {
        public List<Course> courseList;

        public CourseList()
        {
            this.courseList = new List<Course>();
        }

        public void addCourse(Course toAdd)
        {
            this.courseList.Add(toAdd);
        }

        // ALL these lines can be replaced by using an equality comparer
        // ==================================
        public bool containsString(string courseName)
        {
            foreach (Course co in courseList)
            {
                if (co.courseId == courseName)
                {
                    return true;
                }
            }
            return false;
        }

        public Course findCourse(string courseName)
        {
            foreach (Course co in courseList)
            {
                if (co.courseId.ToLower() == courseName.ToLower())
                {
                    return co;
                }
            }
            return null;
        }

        // ==================================
    }
}