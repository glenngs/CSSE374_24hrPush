using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Course
/// </summary>

namespace CourseValidationSystem
{
    public class Course : IEquatable<Course>
    {
        public string courseId;

        public Course(string courseId)
        {
            this.courseId = courseId;
        }

        public bool Equals(Course other)
        {
            return (other.courseId == this.courseId);
        }
    }
}