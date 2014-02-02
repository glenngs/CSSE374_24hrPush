using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace CSSE374CourseParser
{
    class Program
    {
        static void Main(string[] args)
        {

            // =========================
            // START Kimono Data Parsing
            // =========================

            // Example of data parsing is here
            // http://kimonify.kimonolabs.com/kimload?url=http%3A%2F%2Fwww.rose-hulman.edu%2Fcourse-catalog%2Fcourse-catalog-2013-2014%2Fcourse-descriptions%2Fcomputer-science-software-engineering.aspx


            Dictionary<string, string> masterIDDescriptionTable = new Dictionary<string,string>();

            try
            {
                DirectoryInfo info = new DirectoryInfo("CsvCourseData");

                foreach (FileInfo fileInfo in info.GetFiles())
                {
                    Console.WriteLine(fileInfo.Name);

                    using (CsvReader currentReader = new CsvReader(new StreamReader(fileInfo.FullName), false))
                    {

                        int fieldCount = currentReader.FieldCount;

                        if (fieldCount != 2)
                        {
                            throw new Exception("Incorrect Field Count");
                        }

                        while (currentReader.ReadNextRecord())
                        {
                            string courseInfo = currentReader[0];
                            string courseDescription = currentReader[1];

                            char[] separator = { ' ' };
                            string[] totalSplitLine = courseInfo.Split(separator);

                            string firstWordOfInfo = totalSplitLine[0];

                            if (containedInAllCourseIds(firstWordOfInfo))
                            {
                                masterIDDescriptionTable.Add(firstWordOfInfo, returnCourseDescription(currentReader[0], currentReader[1]));
                            }
                            else if (containedInAllCourseIds(totalSplitLine[0] + totalSplitLine[1]))
                            {
                                masterIDDescriptionTable.Add(totalSplitLine[0] + totalSplitLine[1], returnCourseDescription(currentReader[0], currentReader[1]));
                            }
                            else
                            {
                                Console.WriteLine("Failed to find course ID " + totalSplitLine[0] + totalSplitLine[1]);
                            }

                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Unhandled");
                Console.WriteLine(e.Message);
            }

            // =======================
            // END Kimono Data Parsing
            // =======================







            foreach (KeyValuePair<string, string> kvp in masterIDDescriptionTable){
                Console.WriteLine("ID: " + kvp.Key);
                Console.WriteLine("Description: " + kvp.Value);
            }

            Console.ReadLine();
        }

        static string returnCourseDescription(string courseData, string courseDataAndDescription)
        {
            if (!courseDataAndDescription.StartsWith(courseData)){
                Console.WriteLine("Data Description Mismatch");
                Console.WriteLine(courseData);
                return "Failed Data";
            }

            return courseDataAndDescription.Remove(0, courseData.Length);
        }


        static bool containedInAllCourseIds(string firstWord)
        {
            string[] chemCourses = { "CHE110",
                "CHE200",
                "CHE201",
                "CHE202",
                "CHE300",
                "CHE301",
                "CHE303",
                "CHE304",
                "CHE310",
                "CHE315",
                "CHE320",
                "CHE321",
                "CHE404",
                "CHE405",
                "CHE409",
                "CHE411",
                "CHE412",
                "CHE413",
                "CHE416",
                "CHE417",
                "CHE418",
                "CHE419",
                "CHE420",
                "CHE440",
                "CHE441",
                "CHE450",
                "CHE461",
                "CHE465",
                "CHE470",
                "CHE490",
                "CHE499",
                "CHE502",
                "CHE503",
                "CHE504",
                "CHE505",
                "CHE512",
                "CHE513",
                "CHE519",
                "CHE521",
                "CHE540",
                "CHE545",
                "CHE546",
                "CHE590",
                "CHE597",
                "CHE598",
                "CHE599" };
            string[] csseCourses = { "CSSE120",
                "CSSE132",
                "CSSE220",
                "CSSE221",
                "CSSE230",
                "CSSE232",
                "CSSE241",
                "CSSE290",
                "CSSE304",
                "CSSE325",
                "CSSE332",
                "CSSE333",
                "CSSE335",
                "CSSE351",
                "CSSE371",
                "CSSE372",
                "CSSE373",
                "CSSE374",
                "CSSE375",
                "CSSE376",
                "CSSE402",
                "CSSE403",
                "CSSE404",
                "CSSE413",
                "CSSE432",
                "CSSE433",
                "CSSE442",
                "CSSE451",
                "CSSE453",
                "CSSE461",
                "CSSE463",
                "CSSE473",
                "CSSE474",
                "CSSE477",
                "CSSE479",
                "CSSE481",
                "CSSE487",
                "CSSE488",
                "CSSE489",
                "CSSE490",
                "CSSE491",
                "CSSE492",
                "CSSE493",
                "CSSE494",
                "CSSE495",
                "CSSE496",
                "CSSE497",
                "CSSE498",
                "CSSE499" };

            List<string> allCourses = csseCourses.ToList<string>();

            List<string> chemCoursesList = chemCourses.ToList<string>();
            allCourses.AddRange(chemCoursesList);

            

            if (allCourses.Contains(firstWord))
            {
                return true;
            }


            return false;
        }


    }
}
