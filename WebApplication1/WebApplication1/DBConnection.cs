using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBConnection
/// </summary>
/// 
namespace CourseValidationSystem
{
    public class DBConnection
    {
        public class DBAccessReturnValues
        {
            public Dictionary<string, List<string>> prerequisites;
            public Dictionary<string, List<string>> corequisites;
            public Dictionary<string, CourseOfferingEnum> termOffered;

            public DBAccessReturnValues(
                Dictionary<string, List<string>> prerequisites, Dictionary<string, List<string>> corequisites, Dictionary<string, CourseOfferingEnum> termOffered)
            {
                this.prerequisites = prerequisites;
                this.corequisites = corequisites;
                this.termOffered = termOffered;
            }
        }

        public DBAccessReturnValues getPrereqCoreqTermsDataFromDatabase()
        {
            Dictionary<string, List<string>> prerequisites = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> corequisites = new Dictionary<string, List<string>>();
            
            List<string> tempList = new List<string>();
            prerequisites.Add("csse120", new List<string>(tempList));
            prerequisites.Add("csse132", new List<string>(tempList));
            prerequisites.Add("csse221", new List<string>(tempList));
            tempList.Add("csse120");
            prerequisites.Add("csse220", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            prerequisites.Add("csse230", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse120");
            tempList.Add("csse132");
            prerequisites.Add("csse232", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            prerequisites.Add("csse241", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma275");
            prerequisites.Add("csse304", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("ma212");
            prerequisites.Add("csse325", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("csse232");
            prerequisites.Add("csse332", new List<string>(tempList));
            tempList.Clear();            
            tempList.Add("ma275");
            prerequisites.Add("csse333", new List<string>(tempList));
            tempList.Clear();          
            tempList.Add("ma212");
            prerequisites.Add("csse3335", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("ma212");
            prerequisites.Add("csse351", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("rh330");
            prerequisites.Add("csse371", new List<string>(tempList));
            tempList.Clear();            
            prerequisites.Add("csse372", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma275");
            prerequisites.Add("csse373", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse371");            
            prerequisites.Add("csse374", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse374");          
            prerequisites.Add("csse375", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");            
            prerequisites.Add("csse376", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse332");           
            prerequisites.Add("csse402", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse304");            
            prerequisites.Add("csse403", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse304");
            tempList.Add("csse232");
            tempList.Add("csse474");
            prerequisites.Add("csse404", new List<string>((tempList)));
            tempList.Clear();           
            tempList.Add("csse230");        
            prerequisites.Add("csse413", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            prerequisites.Add("csse432", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse333");
            prerequisites.Add("csse433", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse332");
            tempList.Add("ma275");
            prerequisites.Add("csse442", new List<string>(tempList));         
            tempList.Clear();
            tempList.Add("csse351");          
            prerequisites.Add("csse451", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse413");            
            prerequisites.Add("csse453", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse120");
            tempList.Add("ma212");
            prerequisites.Add("csse461", new List<string>(tempList));
            tempList.Clear();            
            tempList.Add("ma212");
            prerequisites.Add("csse463", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma375");
            prerequisites.Add("csse473", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            tempList.Add("ma375");
            prerequisites.Add("csse474", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse374");
            prerequisites.Add("csse477", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse220");
            tempList.Add("ma275");
            prerequisites.Add("csse479", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse230");
            prerequisites.Add("csse481", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("rh330");
            prerequisites.Add("csse487", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse487");
            prerequisites.Add("csse488", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse488");
            prerequisites.Add("csse489", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse371");
            prerequisites.Add("csse497", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse497");
            tempList.Add("csse374");
            prerequisites.Add("csse498", new List<string>(tempList));
            tempList.Clear();
            tempList.Add("csse498");
            prerequisites.Add("csse499", new List<string>(tempList));
            tempList.Clear();

            corequisites.Add("csse120", new List<string>());
            corequisites.Add("csse132", new List<string>());
            corequisites.Add("csse220", new List<string>());
            corequisites.Add("csse221", new List<string>());
            corequisites.Add("csse230", new List<string>());
            corequisites.Add("csse232", new List<string>());
            corequisites.Add("csse241", new List<string>());
            corequisites.Add("csse290", new List<string>());
            corequisites.Add("csse304", new List<string>());
            corequisites.Add("csse325", new List<string>());
            corequisites.Add("csse332", new List<string>());
            tempList.Add("csse230");
            corequisites.Add("csse333", new List<string>(tempList));
            corequisites.Add("csse335", new List<string>());
            corequisites.Add("csse351", new List<string>());
            corequisites.Add("csse371", new List<string>());
            corequisites.Add("csse372", new List<string>());
            corequisites.Add("csse373", new List<string>());
            corequisites.Add("csse374", new List<string>());
            corequisites.Add("csse375", new List<string>());
            corequisites.Add("csse376", new List<string>());
            corequisites.Add("csse402", new List<string>());
            corequisites.Add("csse403", new List<string>());
            corequisites.Add("csse404", new List<string>());
            corequisites.Add("csse413", new List<string>());
            corequisites.Add("csse432", new List<string>());
            corequisites.Add("csse433", new List<string>());
            corequisites.Add("csse442", new List<string>());
            corequisites.Add("csse451", new List<string>());
            corequisites.Add("csse453", new List<string>());
            corequisites.Add("csse461", new List<string>());
            corequisites.Add("csse463", new List<string>());
            corequisites.Add("csse473", new List<string>());
            corequisites.Add("csse474", new List<string>());
            corequisites.Add("csse477", new List<string>());
            corequisites.Add("csse479", new List<string>());
            corequisites.Add("csse481", new List<string>());
            corequisites.Add("csse487", new List<string>());
            corequisites.Add("csse488", new List<string>());
            corequisites.Add("csse489", new List<string>());
            corequisites.Add("csse497", new List<string>());
            corequisites.Add("csse498", new List<string>());
            corequisites.Add("csse499", new List<string>());

            // Setup all courses offered all terms
            Dictionary<string, CourseOfferingEnum> termOffered = new Dictionary<string, CourseOfferingEnum>();
            foreach (string courseID in corequisites.Keys)
            {
                termOffered.Add(courseID, (CourseOfferingEnum.Fall | CourseOfferingEnum.Spring | CourseOfferingEnum.Winter));
            }

            return new DBAccessReturnValues(prerequisites, corequisites, termOffered);
        }
    }
}