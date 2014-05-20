using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using CourseValidationSystem;

namespace CourseValidationTester
{
    class Program
    {
        static void Main(string[] args)
        {
            CourseValidationService serv = new CourseValidationService();

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("Error Message")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("[{\"TermCode\":\"201420\", \"Class\":\"CSSE120\"},{\"TermCode\":\"201430\", \"Class\":\"CSSE220\"}]")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("[{\"TermCode\":\"201420\", \"Class\":\"CSSE120\"},{\"TermCode\":\"201430\", \"Class\":\"CSSE220\"},{\"TermCode\":\"201410\", \"Class\":\"MA212\"}]")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("[{\"TermCode\":\"000000\",\"Class\":\"MA111\"},{\"TermCode\":\"201210\",\"Class\":\"CLSK100\"},{\"TermCode\":\"201210\",\"Class\":\"MA112\"},{\"TermCode\":\"201210\",\"Class\":\"PH111\"},{\"TermCode\":\"201210\",\"Class\":\"CSSE120\"},{\"TermCode\":\"201210\",\"Class\":\"RH131\"},{\"TermCode\":\"201220\",\"Class\":\"CSSE220\"},{\"TermCode\":\"201220\",\"Class\":\"MA113\"},{\"TermCode\":\"201220\",\"Class\":\"PH112\"},{\"TermCode\":\"201220\",\"Class\":\"SV291\"},{\"TermCode\":\"201230\",\"Class\":\"CHEM111\"},{\"TermCode\":\"201230\",\"Class\":\"CSSE132\"},{\"TermCode\":\"201230\",\"Class\":\"MA212\"},{\"TermCode\":\"201230\",\"Class\":\"SV151\"},{\"TermCode\":\"201310\",\"Class\":\"CSSE232\"},{\"TermCode\":\"201310\",\"Class\":\"CSSE230\"},{\"TermCode\":\"201310\",\"Class\":\"GS399\"},{\"TermCode\":\"201310\",\"Class\":\"MA275\"},{\"TermCode\":\"201320\",\"Class\":\"CSSE333\"},{\"TermCode\":\"201320\",\"Class\":\"MA323\"},{\"TermCode\":\"201320\",\"Class\":\"MA375\"},{\"TermCode\":\"201320\",\"Class\":\"RH330\"},{\"TermCode\":\"201320\",\"Class\":\"AB491\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE304\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE376\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE490\"},{\"TermCode\":\"201330\",\"Class\":\"MA381\"},{\"TermCode\":\"201330\",\"Class\":\"CSSEElective\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE351\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE371\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE372\"},{\"TermCode\":\"201410\",\"Class\":\"MA211\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE332\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE374\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE451\"},{\"TermCode\":\"201420\",\"Class\":\"PH241\"}]")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("[{\"TermCode\":\"000000\",\"Class\":\"SP111\"},{\"TermCode\":\"000000\",\"Class\":\"SP112\"},{\"TermCode\":\"000000\",\"Class\":\"RH131\"},{\"TermCode\":\"201210\",\"Class\":\"CLSK100\"},{\"TermCode\":\"201210\",\"Class\":\"ECE160\"},{\"TermCode\":\"201210\",\"Class\":\"IA436\"},{\"TermCode\":\"201210\",\"Class\":\"MA112\"},{\"TermCode\":\"201210\",\"Class\":\"PH111\"},{\"TermCode\":\"201220\",\"Class\":\"ECE130\"},{\"TermCode\":\"201220\",\"Class\":\"CSSE120\"},{\"TermCode\":\"201220\",\"Class\":\"MA113\"},{\"TermCode\":\"201220\",\"Class\":\"PH112\"},{\"TermCode\":\"201230\",\"Class\":\"CSSE220\"},{\"TermCode\":\"201230\",\"Class\":\"MA211\"},{\"TermCode\":\"201230\",\"Class\":\"PH113\"},{\"TermCode\":\"201230\",\"Class\":\"SP113\"},{\"TermCode\":\"201310\",\"Class\":\"CHEM111\"},{\"TermCode\":\"201310\",\"Class\":\"CSSE232\"},{\"TermCode\":\"201310\",\"Class\":\"ECE203\"},{\"TermCode\":\"201310\",\"Class\":\"MA212\"},{\"TermCode\":\"201320\",\"Class\":\"CSSE332\"},{\"TermCode\":\"201320\",\"Class\":\"ECE204\"},{\"TermCode\":\"201320\",\"Class\":\"MA381\"},{\"TermCode\":\"201320\",\"Class\":\"RH330\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE230\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE490\"},{\"TermCode\":\"201330\",\"Class\":\"ECE332\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE351\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE371\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE372\"},{\"TermCode\":\"201410\",\"Class\":\"MA275\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE290\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE374\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE333\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE451\"},{\"TermCode\":\"201420\",\"Class\":\"MA375\"},{\"TermCode\":\"201430\",\"Class\":\"CSSE304\"},{\"TermCode\":\"201430\",\"Class\":\"CSSE375\"},{\"TermCode\":\"201430\",\"Class\":\"CSSE376\"}]")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("[{\"TermCode\":\"201210\",\"Class\":\"CLSK100\"},{\"TermCode\":\"201210\",\"Class\":\"CSSE120\"},{\"TermCode\":\"201210\",\"Class\":\"MA111\"},{\"TermCode\":\"201210\",\"Class\":\"PH111\"},{\"TermCode\":\"201210\",\"Class\":\"SV382\"},{\"TermCode\":\"201220\",\"Class\":\"CSSE220\"},{\"TermCode\":\"201220\",\"Class\":\"MA112\"},{\"TermCode\":\"201220\",\"Class\":\"PH112\"},{\"TermCode\":\"201230\",\"Class\":\"CHEM111\"},{\"TermCode\":\"201230\",\"Class\":\"CSSE132\"},{\"TermCode\":\"201230\",\"Class\":\"MA113\"},{\"TermCode\":\"201230\",\"Class\":\"SV337\"},{\"TermCode\":\"201310\",\"Class\":\"CSSE230\"},{\"TermCode\":\"201310\",\"Class\":\"MA212\"},{\"TermCode\":\"201310\",\"Class\":\"MA275\"},{\"TermCode\":\"201310\",\"Class\":\"RH131\"},{\"TermCode\":\"201320\",\"Class\":\"CSSE232\"},{\"TermCode\":\"201320\",\"Class\":\"CSSE333\"},{\"TermCode\":\"201320\",\"Class\":\"MA323\"},{\"TermCode\":\"201320\",\"Class\":\"MA375\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE376\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE490\"},{\"TermCode\":\"201330\",\"Class\":\"GS399\"},{\"TermCode\":\"201330\",\"Class\":\"RH330\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE351\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE371\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE413\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE372\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE332\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE374\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE453\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE451\"}]")
            );

            System.Console.WriteLine(
                serv.testIsClassScheduleValid("[{\"TermCode\":\"201210\",\"Class\":\"CLSK100\"},{\"TermCode\":\"201210\",\"Class\":\"MA111\"},{\"TermCode\":\"201210\",\"Class\":\"PH111\"},{\"TermCode\":\"201210\",\"Class\":\"RH131\"},{\"TermCode\":\"201210\",\"Class\":\"SV322\"},{\"TermCode\":\"201220\",\"Class\":\"CSSE232\"},{\"TermCode\":\"201220\",\"Class\":\"IA232\"},{\"TermCode\":\"201220\",\"Class\":\"MA112\"},{\"TermCode\":\"201220\",\"Class\":\"PH112\"},{\"TermCode\":\"201230\",\"Class\":\"AB191\"},{\"TermCode\":\"201230\",\"Class\":\"MA113\"},{\"TermCode\":\"201230\",\"Class\":\"SV337\"},{\"TermCode\":\"201310\",\"Class\":\"GS399\"},{\"TermCode\":\"201310\",\"Class\":\"MA275\"},{\"TermCode\":\"201320\",\"Class\":\"CSSE230\"},{\"TermCode\":\"201320\",\"Class\":\"CSSE333\"},{\"TermCode\":\"201320\",\"Class\":\"MA212\"},{\"TermCode\":\"201320\",\"Class\":\"MA375\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE376\"},{\"TermCode\":\"201330\",\"Class\":\"CSSE490\"},{\"TermCode\":\"201330\",\"Class\":\"RH330\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE351\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE371\"},{\"TermCode\":\"201410\",\"Class\":\"CSSE372\"},{\"TermCode\":\"201420\",\"Class\":\"CHEM111\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE332\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE374\"},{\"TermCode\":\"201420\",\"Class\":\"CSSE451\"}]")
            );

            System.Console.ReadLine();

        }
    }
}
