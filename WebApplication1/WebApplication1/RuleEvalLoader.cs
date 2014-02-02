using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RuleEvalLoader
/// </summary>
/// 

namespace CourseValidationSystem
{
    public static class RuleEvalLoader
    {

        public static UIDataParser loadRulesEngine()
        {
            RuleEvalEngine newEngine = new RuleEvalEngine();
            return new UIDataParser(newEngine);
        }

    }
}