using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SampleADO.NET
{
    internal partial class Program
    {
        static void Connection_StateChange(object sender, StateChangeEventArgs e)
        {
            var previousColor =  Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine($"State change from {e.OriginalState} to {e.CurrentState}.");
            Console.ForegroundColor = previousColor;
        }

        static void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Info: {e.Message}.");
            foreach(SqlError error in e.Errors)
            {
                Console.WriteLine($" Error: {error.Message}.");
            }
            Console.ForegroundColor = previousColor;
        }
    }
}
