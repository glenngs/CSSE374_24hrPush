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
    }
}