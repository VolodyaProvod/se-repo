using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApp3
{
    class Program
    {
        static void FirstType()
        {
            Console.WriteLine("   {0,-40}{1,-15}{2,-15}{3,-15}   \n", "Name", "   PID", "     CPU", "         RAM");
            foreach (Process proc in Process.GetProcesses())
            {
                using (PerformanceCounter process = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName))
                {
                    process.NextValue();
                    long gpu = proc.PrivateMemorySize64;
                    string cpu = proc.ProcessName;
                    Console.WriteLine("|| {0,-40} | {1,-15} | {2,-15} | {3,-15} ||", proc.ProcessName, proc.Id, Math.Round(process.NextValue()/100,2) + "%", gpu/ 1000000+"Mb");
                    Thread.Sleep(50);
                }
            }
        }
        static void SecondType()
        {
            PerformanceCounter ram = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            Console.Write("[");
            for (int i = 0; i < 50; i++)
            {
                if (i < ((int)ram.NextValue())/2)
                    Console.Write("|");
                else
                    Console.Write(".");
            }
            Console.Write("] (" + Math.Round(ram.NextValue(),2) + "% RAM usage)\n\n");
            PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            for (int i = 0; i < 11; i++)
            {
                Console.Write("|");
                int cpuusage = (int)cpu.NextValue();
                for (int j = 0; j < 50; j++)
                {
                    if ((cpuusage / 2) > j)
                    {
                        Console.Write((char)15);
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
                Console.WriteLine("  " + "CPU usage: " + cpuusage + "%");
                Console.WriteLine("|");
                Thread.Sleep(50);
            }
        }

        static void Main(string[] args)
        {
            long num = long.Parse(args[0]);
            switch (num)
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
                    break;
            }
        }
    }
}