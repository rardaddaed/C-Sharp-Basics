using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    /// <summary>
    /// Class to display log messages
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Method to display a sucess message
        /// </summary>
        public void Success(string logText, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss:fff")}] Success: {logText}", args);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Method to display an information message
        /// </summary>
        public void Information(string logText, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss:fff")}] {logText}", args);
        }

        /// <summary>
        /// Method to display a warning message
        /// </summary>
        public void Warning(string logText, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss:fff")}] Warning: {logText}", args);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Method to display an error message
        /// </summary>
        public void Error(string logText, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss:fff")}] ERROR: {logText}", args);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
