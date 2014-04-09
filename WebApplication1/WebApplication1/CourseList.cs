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

        private class NewEqComp : IEqualityComparer<Course>
        {

            public bool Equals(Course x, Course y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(Course obj)
            {
                throw new NotImplementedException();
            }
        }

        // ALL these lines can be replaced by using an equality comparer
        // ==================================

        // contains course
        // this.courseList.Contains<IEquatable<string>>(courseName)

        // find course
        // this.courseList.Find(i => i.Equals(courseName))

        public Course findCourseInList(string courseName)
        {
            return this.courseList.Find(i => i.Equals(courseName));
        }

        public bool containsCourseId(string courseName)
        {
            foreach (Course crs in courseList)
            {
                if (crs.courseId.ToUpper() == courseName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        // ==================================

    }
}