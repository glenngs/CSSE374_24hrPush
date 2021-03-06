﻿using System;
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


            /*
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

            */




            /*
            foreach (KeyValuePair<string, string> kvp in masterIDDescriptionTable){
                Console.WriteLine("ID: " + kvp.Key);
                Console.WriteLine("Description: " + kvp.Value);
            }
            */

            Console.WriteLine("==== Started ====");

            processCSVPrereqData();

            Console.ReadLine();
        }

        public static List<CourseEntry> processCSVPrereqData()
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo("CsvCourseData");

                string path = "";

                foreach (FileInfo fileInfo in info.GetFiles())
                {
                    Console.WriteLine(fileInfo.Name);
                    if (fileInfo.Name == "SCRRTST.csv")
                    {
                        path = fileInfo.FullName;
                    }
                }

                if (path == "")
                {
                    Console.WriteLine("Failed to read file");
                    return null;
                }
                else
                {
                    Console.WriteLine("File Found: " + path);
                }

                using (CsvReader currentReader = new CsvReader(new StreamReader(path), false))
                {

                    int fieldCount = currentReader.FieldCount;

                    if (fieldCount != 15)
                    {
                        throw new Exception("Incorrect Field Count");
                    }

                    Console.WriteLine(currentReader.ReadNextRecord());

                    List<CourseEntry> coursePrereqEntries = new List<CourseEntry>();

                    // 3 is SEQNUM - DROP
                    // 4 is useless
                    // 5 is useless
                    // 6 - 7 are the course prereq name
                    // 8-9 are useless
                    // 10 indicates concurrency
                    // 11 is connector (and/or) 
                    // 12 is Left Paren
                    // 13 is Right Paren
                    // 14 is useless

                    // ===========
                    //  MAIN LOOP
                    // ===========

                    while (currentReader.ReadNextRecord())
                    {

                        string courseName = currentReader[0] + currentReader[1];

                        int termcode = Int32.Parse(currentReader[2]);
                        
                        Console.WriteLine("Read in " + courseName + " at term " + termcode);

                        if (containsClassByName(courseName, coursePrereqEntries))
                        {
                            CourseEntry foundEntry = coursePrereqEntries.Find(delegate(CourseEntry entry) { return entry.courseName == courseName; });

                            if (foundEntry.termCode == termcode)
                            {
                                // Add it

                                addData(currentReader, foundEntry);
                            }
                            else if (foundEntry.termCode < termcode)
                            {
                                // We've found something newer, remove the old one and add a new one
                                coursePrereqEntries.Remove(foundEntry);

                                CourseEntry replacementEntry = new CourseEntry(courseName, termcode);
                                coursePrereqEntries.Add(replacementEntry);

                                Console.WriteLine("Removed entry for " + courseName + " at term " + foundEntry.termCode);

                                // MAGIC
                                addData(currentReader, replacementEntry);


                            }
                            else
                            {
                                // The one already in there is newer, ignore all this
                            }
                            
                        }
                        else
                        {
                            // this class isn't in there yet
                            CourseEntry newEntry = new CourseEntry(courseName, termcode);
                            coursePrereqEntries.Add(newEntry);

                            Console.WriteLine("Found new entry for " + courseName + " at term " + termcode);

                            // ALSO MAGIC
                            addData(currentReader, newEntry);

                        }
                    }



                    // ===============
                    //  END MAIN LOOP
                    // ===============
                    /*
                    foreach (CourseEntry ent in coursePrereqEntries)
                    {
                        Console.WriteLine(ent.ToString());
                    }
                    */

                    return coursePrereqEntries;



//                    CourseEntry manualEntry = coursePrereqEntries.Find(delegate(CourseEntry entry) { return entry.courseName == "CSSE461"; });

//                    Console.WriteLine(manualEntry.ToString());

                }
            }

            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("AHHHH " + e.Message);
            }
        }


        // Add all the line's data to the course entry data structure
        public static void addData(CsvReader reader, CourseEntry entry)
        {

            if (entry.BADENTRY)
            {
                Console.WriteLine("Ignored entry due to bad course entry at " + entry.courseName + entry.termCode);
                return;
            }

            // 3 is SEQNUM - DROP
            // 4 is useless
            // 5 is useless
            // 6 - 7 are the course prereq name
            // 8-9 are useless
            // 10 indicates concurrency
            // 11 is connector (and/or) 
            // 12 is Left Paren
            // 13 is Right Paren
            // 14 is useless

            string prereqName = reader[6] + reader[7];

            bool isCoreq = parseStringToBool(reader[10]);

            Connector conn = parseStringToConnector(reader[11]);

            bool hasLeftParen = parseStringToBool(reader[12]);
            bool hasRightParen = parseStringToBool(reader[13]);

            

            EvaluatableGroup overallGroup = entry.prereqStatement;

            EvaluatableAtom atomToAdd = new EvaluatableAtom(prereqName, isCoreq);

            if (hasLeftParen && hasRightParen)
            {
                overallGroup.addItem(atomToAdd, conn);
            }

            

            if (hasLeftParen)
            {
                // if i have a left paren, create a new group, unless a group is already started
                // in which case this is a bad statement
                if (entry.currentlyParsingGroup == null)
                {
                    // create a new group and start adding to it
                    entry.currentlyParsingGroup = new EvaluatableGroup();

                    overallGroup.addItem(entry.currentlyParsingGroup, conn);
                    entry.currentlyParsingGroup.addItem(atomToAdd, Connector.NoConnector);
                }
                else
                {
                    Console.WriteLine("Bad Entry Detected at " + entry.courseName + " " + entry.termCode);
                    entry.BADENTRY = true;
                    return;
                }

            }
            
            if (hasRightParen)
            {
                if (entry.currentlyParsingGroup == null)
                {
                    Console.WriteLine("Bad Entry Detected at " + entry.courseName + " " + entry.termCode);
                    entry.BADENTRY = true;
                    return;
                }

                entry.currentlyParsingGroup.addItem(atomToAdd, conn);

                entry.currentlyParsingGroup = null;
            }
            
            if (!hasLeftParen && !hasRightParen)
            {
                if (entry.currentlyParsingGroup != null)
                {
                    entry.currentlyParsingGroup.addItem(atomToAdd, conn);
                }
                else
                {
                    overallGroup.addItem(atomToAdd, conn);
                }
            }

        }

        public abstract class EvaluatableStatement
        {
            public bool evaluate()
            {
                return true;
            }
        }

        public class EvaluatableGroup : EvaluatableStatement
        {
            public Queue<EvaluatableStatement> statementQueue;
            public Queue<Connector> connectorQueue;


            public EvaluatableGroup()
            {
                this.statementQueue = new Queue<EvaluatableStatement>();
                this.connectorQueue = new Queue<Connector>();
            }

            public void addItem(EvaluatableStatement statement, Connector conn)
            {
                this.statementQueue.Enqueue(statement);
                this.connectorQueue.Enqueue(conn);
            }

            public override string ToString()
            {
                EvaluatableStatement[] statementArray = this.statementQueue.ToArray<EvaluatableStatement>();
                Connector[] connArray = this.connectorQueue.ToArray<Connector>();

                string finalString = "( ";

                for (int i = 0; i < statementArray.Length; i++)
                {
                    finalString = finalString + connectorToString(connArray[i]);
                    finalString = finalString + statementArray[i].ToString();
                }

                finalString = finalString + " )";
                return finalString;
            }
        }

        public static string connectorToString(Connector conn)
        {
            if (conn == Connector.NoConnector)
            {
                return "";
            }
            if (conn == Connector.AndConnector)
            {
                return "A";
            }
            if (conn == Connector.OrConnector)
            {
                return "O";
            }
            else
            {
                return " WTF? THIS CANT EVEN HAPPEN ";
            }
        }

        public class EvaluatableAtom : EvaluatableStatement
        {
            public string courseName;
            public bool isCoreq;

            public EvaluatableAtom(string courseName, bool allowConcurrency)
            {
                this.courseName = courseName;
                this.isCoreq = allowConcurrency;
            }
            public override string ToString()
            {
                return " " + courseName + " ";
            }
        }

        public class CourseEntry
        {
            public string courseName;
            public int termCode;

            public bool BADENTRY;

            public EvaluatableGroup prereqStatement;

            public EvaluatableGroup currentlyParsingGroup;

            public CourseEntry(string courseName, int termCode)
            {
                this.courseName = courseName;
                this.termCode = termCode;

                this.BADENTRY = false;
                this.currentlyParsingGroup = null;

                this.prereqStatement = new EvaluatableGroup();
            }

            public override string ToString()
            {
                
                if (this.BADENTRY)
                {
                    return this.courseName + " " + this.termCode + " IS VERY VERY VERY BAD";
                }
                
                return this.courseName + " " + this.termCode + " ====== " + 
                    prereqStatement.connectorQueue.Count + " ===== " + this.prereqStatement.ToString();
            }


        }

        public enum Connector
        {
            NoConnector,
            AndConnector,
            OrConnector
        }

        public static Connector parseStringToConnector(string str)
        {
            if (str == "A")
            {
                return Connector.AndConnector;
            }
            else if (str == "O")
            {
                return Connector.OrConnector;
            }
            else
            {
                return Connector.NoConnector;
            }

        }

        public static bool parseStringToBool(string str)
        {
            if (str == "")
            {
                return false;
            }
            return true;
        }

        public static bool containsClassByName(string name, List<CourseEntry> entries){

            foreach(CourseEntry entry in entries)
            {
                if (entry.courseName == name)
                {
                    return true;
                }
            }
            return false;
        }













































/*
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


 */
    }
}
