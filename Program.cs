using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApp2
{
    class Program
    {
        static void FirstType()
        {
            Console.WriteLine("{0,-25}{1,-15}{2,-15}{3,-15}\n\n", "Name", "PID", "CPU", "RAM");
            foreach (Process proc in Process.GetProcesses())
            {
                using (PerformanceCounter process = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName))
                {
                    process.NextValue();
                    long gpu = proc.PrivateMemorySize64;
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("{0,-25}{1,-15}{2,-15}{3,-15}", proc.ProcessName, proc.Id, process.NextValue(), gpu);
                }
            }
        }
        static void SecondType()
        {
            PerformanceCounter ram = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            Console.Write("[");
            for (int i = 0; i < 100; i++)
            {
                if (i < (int)ram.NextValue())
                    Console.Write("|");
                else
                    Console.Write(".");
            }
            Console.Write("] ("+ram.NextValue()+"% RAM usage)\n");

            PerformanceCounter process = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            for (int i = 0; i < 100; i++)
            {
                if (i < (int)process.NextValue())
                    Console.Write("|");
                else
                    Console.Write(".");
            }
            Console.Write("% CPU usage\n");

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Select display mode:\n1. Table\n2. Progress bar\n3. Both types\n");
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    Console.Clear();
                    FirstType();
                    break;
                case 2:
                    Console.Clear();
                    SecondType();
                    break;
                case 3:
                    Console.Clear();
                    FirstType();
                    SecondType();
                    break;
                default:
                    Console.WriteLine("Incorrect data entered");
                    break;
            }
        }
    }
}