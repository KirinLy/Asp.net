using MonitoringLib;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var recorder = Recorder.GetRecorder();

            ////////////////////
            recorder.Start("Timer1");

            int[] array1 = Enumerable.Range(start: 1, count: 10000000).ToArray();
            Console.WriteLine("array1");
            Thread.Sleep(3000);

            NewMethod();

        }

        private static void NewMethod()
        {
            var recorder = Recorder.GetRecorder();
            int[] array2 = Enumerable.Range(start: 1, count: 10000000).ToArray();
            Console.WriteLine("array2!");
            Thread.Sleep(2000);

            recorder.Stop("Timer1");

        }
    }
}