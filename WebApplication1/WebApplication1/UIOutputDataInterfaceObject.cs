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

        public UIOutputDataInterfaceObject(string errorMessage, string classID)
        {
            this.ErrorMessage = errorMessage;
            this.Class = classID;
        }

        public UIOutputDataInterfaceObject()
        {
        }
    }
}