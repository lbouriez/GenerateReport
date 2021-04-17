using System;

namespace GenerateReport.Helper
{
    public static class Logger
    {
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR.....: {message}");
            RestoreDefault();
        }
        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"LOG.......: {message}");
            RestoreDefault();
        }
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"WARNING...: {message}");
            RestoreDefault();
        }

        private static void RestoreDefault()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}