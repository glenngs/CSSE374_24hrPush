﻿using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CourseValidationSystem
{
    public class PrereqCSVParser
    {
        public static Dictionary<String, CourseOfferingEnum> processCSVScheduleData()
        {
            try
            {
                string path;
                try
                {
                    path = HttpContext.Current.Server.MapPath("/CsvCourseData/CourseSchedule.csv");
                }
                catch (Exception e)
                {
                    path = "/CsvCourseData/CourseSchedule.csv";
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

                Dictionary<String, CourseOfferingEnum> courseOfferings = new Dictionary<string, CourseOfferingEnum>();

                using (CsvReader currentReader = new CsvReader(new StreamReader(path), false))
                {
                    int fieldCount = currentReader.FieldCount;

                    if (fieldCount != 14)
                    {
                        Console.WriteLine("Incorrect Field Count");
                        throw new Exception("Incorrect Field Count");
                    }

                    currentReader.ReadNextRecord();

                    // 0 is useless
                    // 1 is the name
                    // 2 - 4 are useless
                    // 5 is the term
                    // 6 is the year
                    // 7 - 13 are useless

                    // ===========
                    //  MAIN LOOP
                    // ===========

                    while (currentReader.ReadNextRecord())
                    {
                        String courseName = currentReader[1];

                        int term = Int32.Parse(currentReader[5]);
                        int year = Int32.Parse(currentReader[6]);

                        if (courseOfferings.ContainsKey(courseName))
                        {
                            courseOfferings[courseName] = courseOfferings[courseName] |
                                                          PrereqCSVParser.parseYearTermToEnum(year, term);
                        }
                        else
                        {
                            courseOfferings.Add(courseName, PrereqCSVParser.parseYearTermToEnum(year, term));
                        }
                    }
                }

                return courseOfferings;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Exception with message: " + e.Message);
                return null;
            }
        }

        public static CourseOfferingEnum parseYearTermToEnum(int year, int inputNum)
        {
            CourseOfferingEnum currentEnum = CourseOfferingEnum.Invalid;

            if (inputNum == 10)
            {
                currentEnum |= CourseOfferingEnum.Fall;
            }
            if (inputNum == 20)
            {
                currentEnum |= CourseOfferingEnum.Winter;
            }
            if (inputNum == 30)
            {
                currentEnum |= CourseOfferingEnum.Spring;
            }
            if (inputNum == 40)
            {
                currentEnum |= CourseOfferingEnum.Summer;
            }

            if (year % 2 == 0)
            {
                // Even year

                if (inputNum == 10)
                {
                    currentEnum |= CourseOfferingEnum.EvenFall;
                }
                if (inputNum == 20)
                {
                    currentEnum |= CourseOfferingEnum.EvenWinter;
                }
                if (inputNum == 30)
                {
                    currentEnum |= CourseOfferingEnum.EvenSpring;
                }
                if (inputNum == 40)
                {
                    currentEnum |= CourseOfferingEnum.EvenSummer;
                }
            }

            else
            {
                // Odd year

                if (inputNum == 10)
                {
                    currentEnum |= CourseOfferingEnum.OddFall;
                }
                if (inputNum == 20)
                {
                    currentEnum |= CourseOfferingEnum.OddWinter;
                }
                if (inputNum == 30)
                {
                    currentEnum |= CourseOfferingEnum.OddSpring;
                }
                if (inputNum == 40)
                {
                    currentEnum |= CourseOfferingEnum.OddSummer;
                }
            }

            return currentEnum;
        }

        public static List<CourseEntry> processCSVPrereqAndOfferingData()
        {
            Dictionary<String, CourseOfferingEnum> courseOfferings = PrereqCSVParser.processCSVScheduleData();

            if (courseOfferings == null)
            {
                return null;
            }

            return processCSVPrereqData(courseOfferings);
        }

        public static List<CourseEntry> processCSVPrereqData(Dictionary<String, CourseOfferingEnum> courseOfferings)
        {
            try
            {
                string path;
                try
                {
                    path = HttpContext.Current.Server.MapPath("/CsvCourseData/SCRRTST.csv");
                }
                catch (Exception e)
                {
                    path = "/CsvCourseData/SCRRTST.csv";
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

                    //Console.WriteLine(currentReader.ReadNextRecord());
                    currentReader.ReadNextRecord();

                    List<CourseEntry> coursePrereqEntries = new List<CourseEntry>();

                    // 0 - 1 are the course name
                    // 2 is the term this set of prerequisites are for - we choose the most recent
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

                        CourseOfferingEnum termsOffered;

                        if (courseOfferings.ContainsKey(courseName))
                        {
                            termsOffered = courseOfferings[courseName];
                        }
                        else
                        {
                            termsOffered = CourseOfferingEnum.Fall | CourseOfferingEnum.Spring |
                                           CourseOfferingEnum.Summer | CourseOfferingEnum.Winter;
                        }
                        int termcode = Int32.Parse(currentReader[2]);

                        //Console.WriteLine("Read in " + courseName + " at term " + termcode);

                        if (containsClassByName(courseName, coursePrereqEntries))
                        {
                            CourseEntry foundEntry = coursePrereqEntries.Find(delegate(CourseEntry entry) { return entry.courseName == courseName; });

                            if (foundEntry.termCode == termcode)
                            {
                                // Add it

                                addData(currentReader, foundEntry, termsOffered);
                            }
                            else if (foundEntry.termCode < termcode)
                            {
                                // We've found something newer, remove the old one and add a new one
                                coursePrereqEntries.Remove(foundEntry);

                                CourseEntry replacementEntry = new CourseEntry(courseName, termcode, termsOffered);
                                coursePrereqEntries.Add(replacementEntry);

                                //Console.WriteLine("Removed entry for " + courseName + " at term " + foundEntry.termCode);

                                // MAGIC :D
                                addData(currentReader, replacementEntry, termsOffered);


                            }
                            else
                            {
                                // The one already in there is newer, ignore all this
                            }

                        }
                        else
                        {
                            // this class isn't in there yet
                            CourseEntry newEntry = new CourseEntry(courseName, termcode, termsOffered);
                            coursePrereqEntries.Add(newEntry);

                            //Console.WriteLine("Found new entry for " + courseName + " at term " + termcode);

                            // ALSO MAGIC :D
                            addData(currentReader, newEntry, termsOffered);

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

                    //CourseEntry manualEntry = coursePrereqEntries.Find(delegate(CourseEntry entry) { return entry.courseName == "CSSE461"; });

                    //Console.WriteLine(manualEntry.ToString());

                }
            }

            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Exception with message: " + e.Message);
                return null;
            }
        }


        // Add all the line's data to the course entry data structure
        public static void addData(CsvReader reader, CourseEntry entry, CourseOfferingEnum termsOffered)
        {

            if (entry.BADENTRY)
            {
                //Console.WriteLine("Ignored entry due to bad course entry at " + entry.courseName + entry.termCode);
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

            EvaluatableCourse courseToAdd = new EvaluatableCourse(prereqName, isCoreq);

            if (hasLeftParen && hasRightParen)
            {
                overallGroup.addItem(courseToAdd, conn);
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
                    entry.currentlyParsingGroup.addItem(courseToAdd, Connector.NoConnector);
                }
                else
                {
                    //Console.WriteLine("Bad Entry Detected at " + entry.courseName + " " + entry.termCode);
                    entry.BADENTRY = true;
                    return;
                }

            }

            if (hasRightParen)
            {
                if (entry.currentlyParsingGroup == null)
                {
                    //Console.WriteLine("Bad Entry Detected at " + entry.courseName + " " + entry.termCode);
                    entry.BADENTRY = true;
                    return;
                }

                entry.currentlyParsingGroup.addItem(courseToAdd, conn);

                entry.currentlyParsingGroup = null;
            }

            if (!hasLeftParen && !hasRightParen)
            {
                if (entry.currentlyParsingGroup != null)
                {
                    entry.currentlyParsingGroup.addItem(courseToAdd, conn);
                }
                else
                {
                    overallGroup.addItem(courseToAdd, conn);
                }
            }

        }

        public abstract class EvaluatableStatement
        {
            public abstract EvaluationError evaluate(CourseList list, Course courseToCheck);
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

            public override EvaluationError evaluate(CourseList list, Course courseToCheck)
            {
                Queue<EvaluatableStatement> tempStatementQueue = new Queue<EvaluatableStatement>(statementQueue);
                Queue<Connector> tempConnectorQueue = new Queue<Connector>(connectorQueue);

                bool statementValid = true;

                bool firstError = false;

                EvaluationError finalError = new EvaluationError(false, "(", 0);

                while (tempStatementQueue.Count > 0)
                {
                    EvaluatableStatement currentStatement = tempStatementQueue.Dequeue();
                    Connector prevConnector = tempConnectorQueue.Dequeue();

                    EvaluationError err = currentStatement.evaluate(list, courseToCheck);

                    if (prevConnector == Connector.NoConnector)
                    {
                        statementValid = err.isValid; //currentStatement.evaluate(list, courseToCheck);
                        if (!err.isValid)
                        {
                            // Add this statement's contents to the total error string
                            finalError.stringError += err.stringError;
                            
                            // Set the error code to the higher severity
                            finalError.errorCode = returnHigherErrorCode(finalError.errorCode, err.errorCode);

                            firstError = true;
                        }
                    }
                    else if (prevConnector == Connector.AndConnector)
                    {
                        statementValid = statementValid && err.isValid;
                        if (!err.isValid)
                        {
                            if (firstError)
                            {
                                finalError.stringError += " And ";
                            }

                            // Add this statement's contents to the total error string
                            finalError.stringError += err.stringError;

                            // Set the error code to the higher severity
                            finalError.errorCode = returnHigherErrorCode(finalError.errorCode, err.errorCode);

                            firstError = true;
                        }
                    }
                    else if (prevConnector == Connector.OrConnector)
                    {
                        statementValid = statementValid || err.isValid;
                        if (!err.isValid)
                        {

                            if (firstError)
                            {
                                finalError.stringError += " Or ";
                            }
                            // Add this statement's contents to the total error string
                            finalError.stringError += err.stringError;

                            // Set the error code to the higher severity
                            finalError.errorCode = returnHigherErrorCode(finalError.errorCode, err.errorCode);

                            firstError = true;
                        }
                    }
                }

                //
                finalError.stringError += ")";
                finalError.isValid = statementValid;
                return finalError;
            }
        }

        public static int returnHigherErrorCode(int errorCodeBefore, int newErrorCode)
        {
            if (errorCodeBefore == 0)
            {
                // No error before, just take the new one
                return newErrorCode;
            }
            else if (errorCodeBefore == 10)
            {
                return 10;
            }
            else if (errorCodeBefore == 11)
            {
                return 10;
            }
            else
            {
                // the error code before was 20
                if (newErrorCode == 20)
                {
                    return 20;
                }
                else if (newErrorCode == 0)
                {
                    return 20;
                }
                else
                {
                    return 10;
                }
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
                return " ERROR ";
            }
        }

        public class EvaluatableCourse : EvaluatableStatement
        {
            public string courseName;
            public bool isCoreq;

            public EvaluatableCourse(string courseName, bool allowConcurrency)
            {
                this.courseName = courseName;
                this.isCoreq = allowConcurrency;
            }
            public override string ToString()
            {
                return " " + courseName + " ";
            }
            public override EvaluationError evaluate(CourseList list, Course courseToCheck)
            {

                // Ensure this class is in their schedule
                if (!list.containsCourseId(this.courseName))
                {
                    // Not in their schedule at all!
                    // BUILD ERROR MESSAGES HERE

                    return new EvaluationError(false, "Missing: " + this.courseName, 10);
                }
                else
                {
                    // It's in their schedule, is it before this class?
                    if (this.isCoreq == false)
                    {
                        // We cannot take it simultaneously
                        // so ensure the checked class is after this class
                        Course thisCourseInList = list.findCourseInList(this.courseName);
                        if (!(yearTermOneAfterYearTermTwo(courseToCheck.year, courseToCheck.term, thisCourseInList.year,
                            thisCourseInList.term)))
                        {
                            return new EvaluationError(false, "Out of Order Prereq: " + this.courseName, 11);
                        }
                        else
                        {
                            return new EvaluationError(true, "", 20);
                        }
                    }
                    else
                    {
                        // We can take it simultaneously
                        Course thisCourseInList = list.findCourseInList(this.courseName);

                        if (!(yearTermOneAfterYearTermTwo(courseToCheck.year, courseToCheck.term, thisCourseInList.year,
                            thisCourseInList.term)
                             ||
                             yearTermsEqual(courseToCheck.year, courseToCheck.term, thisCourseInList.year,
                                 thisCourseInList.term)))
                        {
                            return new EvaluationError(false, "Out of Order Coreq: " + this.courseName, 11);
                        }
                        else
                        {
                            return new EvaluationError(true, "", 20);
                        }


                    }
                }
            }




        }

        public static string CourseOfferingEnumToString(CourseOfferingEnum courseOffering)
        {
            String fall = courseOffering.HasFlag(CourseOfferingEnum.Fall) ? " Fall" : "";
            String winter = courseOffering.HasFlag(CourseOfferingEnum.Winter) ? " Winter" : "";
            String spring = courseOffering.HasFlag(CourseOfferingEnum.Spring) ? " Spring" : "";
            String summer = courseOffering.HasFlag(CourseOfferingEnum.Summer) ? " Summer" : "";
            return fall + winter + spring + summer;
        }

        public static bool yearTermOneAfterYearTermTwo(int year1, int term1, int year2, int term2)
        {
            if (year1 > year2)
            {
                return true;
            }
            else if (year2 > year1)
            {
                return false;
            }
            else
            {
                // Same year
                return (term1 > term2);
            }

        }

        public static bool yearTermsEqual(int year1, int term1, int year2, int term2)
        {
            return ((year1 == year2) && (term1 == term2));
        }

        public class CourseEntry
        {
            public string courseName;
            public int termCode;
            public CourseOfferingEnum termsOffered;

            public bool BADENTRY;

            public EvaluatableGroup prereqStatement;

            public EvaluatableGroup currentlyParsingGroup;

            public CourseEntry(string courseName, int termCode, CourseOfferingEnum termsOffered)
            {
                this.courseName = courseName;
                this.termCode = termCode;

                this.BADENTRY = false;
                this.currentlyParsingGroup = null;

                this.termsOffered = termsOffered;

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

            private EvaluationError checkTermOffered(CourseOfferingEnum termScheduled)
            {
                // First, check if it's valid
                if ((termScheduled & this.termsOffered) > 0)
                {
                    return new EvaluationError(true, "", 20);
                }
                // If it's not valid, send an error message
                else
                {
                    return new EvaluationError(false,
                        "Attempted To Take:" + CourseOfferingEnumToString(termScheduled) + " Offered:" +
                        CourseOfferingEnumToString(termsOffered) + " ", 20);
                }
            }

            public EvaluationError evaluate(CourseList list, Course courseToCheck, CourseOfferingEnum scheduledTerm)
            {
                EvaluationError error = this.prereqStatement.evaluate(list, courseToCheck);

                if (error.isValid)
                {
                    // We're valid so far - now we need to check the term they tried to take
                    return checkTermOffered(scheduledTerm);
                }
                
                return error;
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

        public static bool containsClassByName(string name, List<CourseEntry> entries)
        {

            foreach (CourseEntry entry in entries)
            {
                if (entry.courseName == name)
                {
                    return true;
                }
            }
            return false;
        }

    }
}