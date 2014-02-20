using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Course
/// </summary>

namespace CourseValidationSystem
{
    public class Course : IEquatable<Course>, IEquatable<string>
    {
        public string courseId;
        public int year;
        public int term;

        public Course(string courseId, int year, int term)
        {
            this.courseId = courseId;
            this.year = year;
            this.term = term;
        }

        public bool Equals(Course other)
        {
            return (other.courseId == this.courseId);
        }

        public bool Equals(string other)
        {
            return (other.ToUpper() == this.courseId.ToUpper());
        }
    }
}