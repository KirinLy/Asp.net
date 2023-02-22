using System.Collections.Concurrent;
using System.Diagnostics;

namespace MonitoringLib
{
    public class Recorder
    {
        private static Recorder _recorder = null;
        private readonly ConcurrentDictionary<string, Stopwatch> _timers = new ConcurrentDictionary<string, Stopwatch>();
        private readonly ConcurrentDictionary<string, long> _bytesPhysicalBefore = new ConcurrentDictionary<string, long>();
        private readonly ConcurrentDictionary<string, long> _bytesVirtualBefore = new ConcurrentDictionary<string, long>();

        private Recorder() {
        }
        public static Recorder GetRecorder() {
            if (_recorder == null) {
                _recorder = new Recorder();
                ReleaseMemory();
            }
            
            return _recorder;
        }

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="timerName"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start(string timerName)
        {
            if (_timers.ContainsKey(timerName) || String.IsNullOrWhiteSpace(timerName))
            {
                throw new InvalidOperationException($"Timer with name {timerName} has been started or invalid.");
            }

            _timers[timerName] = new Stopwatch();
            _timers[timerName].Start();

            // Store the current physical and virtual memory use 
            _bytesPhysicalBefore[timerName] = Process.GetCurrentProcess().WorkingSet64;
            _bytesVirtualBefore[timerName] = Process.GetCurrentProcess().VirtualMemorySize64;

            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("************** Start timer {0} **************", timerName);
            Console.WriteLine("*********************************************");
            Console.WriteLine();
            Console.ForegroundColor = previousColor;
        }

        /// <summary>
        /// Stops the timer with the specified name.
        /// </summary>
        /// <param name="timerName"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Stop(string timerName)
        {
            if (!_timers.ContainsKey(timerName))
            {
                throw new InvalidOperationException($"No timer with name {timerName} has been started.");
            }

            // Get the memory usage before and after the Stopwatch was running
            long bytesPhysicalAfter = Process.GetCurrentProcess().WorkingSet64;
            long bytesVirtualAfter = Process.GetCurrentProcess().VirtualMemorySize64;

            _timers[timerName].Stop();

            // Output the results
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("*************** Stop timer {0} ***************", timerName);
            Console.WriteLine("{0:N3} physical MBytes used.", (bytesPhysicalAfter - _bytesPhysicalBefore[timerName]) / (double) 1048576);
            Console.WriteLine("{0:N3} virtual MBytes used.", (bytesVirtualAfter - _bytesVirtualBefore[timerName]) / (double) 1048576);
            Console.WriteLine("{0} second elapsed.", _timers[timerName].Elapsed);
            Console.WriteLine("*********************************************");
            Console.WriteLine();
            Console.ForegroundColor = previousColor;

            _timers.Remove(timerName, out var _);
            _bytesPhysicalBefore.Remove(timerName, out var _);
            _bytesVirtualBefore.Remove(timerName, out var _);
        }

        /// <summary>
        /// Releases the memory.
        /// </summary>
        private static void ReleaseMemory()
        {
            // Force some garbage collections to release memory that is
            // no longer referenced but has not been released yet
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}