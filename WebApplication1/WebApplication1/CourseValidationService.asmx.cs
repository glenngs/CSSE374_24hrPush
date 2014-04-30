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
    [System.Web.Script.Services.ScriptService]
    public class CourseValidationService : System.Web.Services.WebService
    {

        private UIDataParser dataParser;

        public CourseValidationService()
        {
            dataParser = RuleEvalLoader.loadRulesEngine();
            
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void isClassScheduleValid(string input)
        {
            if (input == "")
            {
                string strResponse = "Warning: Empty Input String";
                string strCallback = Context.Request.QueryString["callback"]; // Get callback method name. e.g. jQuery17019982320107502116_1378635607531

                strResponse = strCallback + strResponse; // e.g. jQuery17019982320107502116_1378635607531(....) 

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", strResponse.Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(strResponse);
            }
            
            try
            {
                List<UIOutputDataInterfaceObject> returnable = dataParser.parseInputDataGiveResponse(input);

                string strResponse = JsonConvert.SerializeObject(returnable);
                string strCallback = Context.Request.QueryString["callback"]; // Get callback method name. e.g. jQuery17019982320107502116_1378635607531

                strResponse = strCallback + strResponse; // e.g. jQuery17019982320107502116_1378635607531(....) 

                Context.Response.Clear(); 
                Context.Response.ContentType = "application/json"; 
                Context.Response.AddHeader("content-length", strResponse.Length.ToString()); 
                Context.Response.Flush(); 

                Context.Response.Write(strResponse);

            }
            catch(Exception e)
            {
                string strResponse = "{'FAIL: " + e.Message + "'}";
                string strCallback = Context.Request.QueryString["callback"]; // Get callback method name. e.g. jQuery17019982320107502116_1378635607531

                strResponse = strCallback + strResponse; // e.g. jQuery17019982320107502116_1378635607531(....) 

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", strResponse.Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(strResponse);
            }
        }

    }
}
