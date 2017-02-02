using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace RegITProducts.administator.pages
{
    public static class   RegExRequester
    {
        public static bool Check(string line, string pattern)
        {
            if (Regex.IsMatch(line, pattern))
                return true;
            return false;
        }
    }
}