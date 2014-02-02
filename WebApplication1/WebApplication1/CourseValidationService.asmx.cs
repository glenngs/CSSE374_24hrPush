using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using CourseValidationSystem;

namespace CourseValidationSystem
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CourseValidationService : System.Web.Services.WebService
    {

        private UIDataParser dataParser;

        public CourseValidationService()
        {
            dataParser = RuleEvalLoader.loadRulesEngine();
            
        }


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string isClassScheduleValid(string input)
        {
            return dataParser.parseInputDataGiveResponse(input);
        }

    }
}
