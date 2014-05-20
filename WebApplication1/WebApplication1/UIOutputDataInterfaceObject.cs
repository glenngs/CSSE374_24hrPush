using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseValidationSystem
{
    public class UIOutputDataInterfaceObject
    {
        public string ErrorMessage;
        public string Class;

        public int ErrorCode;
        // 10 is missing class
        // 11 is out of order class
        // 20 is not offered on term
        // More generally,
        // Starts with 1 is a critical error (red)
        // Starts with 2 is a warning (yellow)

        public UIOutputDataInterfaceObject(string errorMessage, string classID, int errorCode)
        {
            this.ErrorMessage = errorMessage;
            this.Class = classID;
            this.ErrorCode = errorCode;
        }

        public UIOutputDataInterfaceObject()
        {
        }
    }
}