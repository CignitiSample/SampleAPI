using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Test.Automation.Common
{
   public static class APICommonFunctionality
    {
        public static string PrepareJsonFilePath(string applicationTestData, string env, string application, string methodName)
        {
            return applicationTestData + env + "\\" + application + "\\" + methodName;
        }
    }
}
