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

        // 

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