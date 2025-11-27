using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApp.Helpers
{
    public static class UrlHelper
    {
        //Helper function to extract Id from SwapiUrl could be made more robust
        public static int SwapiIdHelperFunction(string url)
        {
            var parts = url.TrimEnd('/').Split('/');
            return int.Parse(parts[^1]);
        }
    }
}
