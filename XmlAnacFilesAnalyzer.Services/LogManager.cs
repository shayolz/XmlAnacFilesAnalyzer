using System;
using XmlAnacFilesAnalyzer.Domain;

namespace XmlAnacFilesAnalyzer.Services
{
    public static class LogManager
    {
        public static void WriteDebugLog(string text)
        {
            if (Configurations.DEBUG)
            {
                Console.WriteLine(text);
            }
        }
    }
}
