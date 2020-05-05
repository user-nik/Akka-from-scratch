using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming
{
    public static class ColorConsole
    {
        private static void PrintColoredMessage(ConsoleColor color, string message)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineGreen(string message)
        {
            PrintColoredMessage(ConsoleColor.Green, message);
        }

        internal static void WriteLineCyan(string message)
        {
            PrintColoredMessage(ConsoleColor.Cyan, message);
        }

        internal static void WriteLineGray(string message)
        {
            PrintColoredMessage(ConsoleColor.Gray, message);
        }

        public static void WriteLineYellow(string message)
        {
            PrintColoredMessage(ConsoleColor.Yellow, message);
        }

        internal static void WriteLineRed(string message)
        {
            PrintColoredMessage(ConsoleColor.Red, message);

        }
    }
}
