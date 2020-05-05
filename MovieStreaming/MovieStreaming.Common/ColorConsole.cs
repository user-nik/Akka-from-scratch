using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming.Common
{
    public static class ColorConsole
    {
        static object locker = new object();

        private static void PrintColoredMessage(ConsoleColor color, string message)
        {
            lock (locker)
            {
                var beforeColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ForegroundColor = beforeColor;
            }
        }

        public static void WriteWhite(string message)
        {
            PrintColoredMessage(ConsoleColor.White, message);
        }

        public static void WriteLineGreen(string message)
        {
            PrintColoredMessage(ConsoleColor.Green, message);
        }

        public static void WriteLineCyan(string message)
        {
            PrintColoredMessage(ConsoleColor.Cyan, message);
        }

        public static void WriteMagenta(string message)
        {
            PrintColoredMessage(ConsoleColor.Magenta, message);
        }

        public static void WriteLineGray(string message)
        {
            PrintColoredMessage(ConsoleColor.Gray, message);
        }

        public static void WriteLineYellow(string message)
        {
            PrintColoredMessage(ConsoleColor.Yellow, message);
        }

        public static void WriteLineRed(string message)
        {
            PrintColoredMessage(ConsoleColor.Red, message);

        }
    }
}
