using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace PCLoad
{
    class ProgressBar
    {
        public void HorizontalPB()
        {
            PerformanceCounter ram = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            Console.Write("[");
            for (int i = 0; i < 50; i++)
            {
                if (i < ((int)ram.NextValue()) / 2)
                    Console.Write("|");
                else
                    Console.Write(".");
            }
            Console.Write("] (" + Math.Round(ram.NextValue(), 2) + "% RAM usage)\n\n");
        }
        public void VerticalPB()
        {
            PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Time", "_Total"); 
            for (int i = 0; i < 9; i++)
            {
                Console.Write("|");
                int cpuusage = (int)cpu.NextValue();
                for (int j = 0; j < 50; j++)
                {
                    if ((cpuusage / 2) > j)
                    {
                        Console.Write("■");
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
        public void Table ()
        {
            Console.WriteLine("   {0,-40}{1,-15}{2,-15}{3,-15}   \n", "Name", "   PID", "     CPU", "         RAM");
            foreach (Process proc in Process.GetProcesses())
            {
                PerformanceCounter process = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName);
                long gpu = proc.PrivateMemorySize64;
                string cpu = proc.ProcessName;
                process.NextValue();
                Console.WriteLine("|| {0,-40} | {1,-15} | {2,-15} | {3,-15} ||", cpu, proc.Id, Math.Round(process.NextValue()/100, 2) + "%", gpu / 1000000 + "Mb");
            }
        }
    }
    class Monitors
    {
        public void FirstMonitor()
        {
            ProgressBar drawing = new ProgressBar();
            drawing.Table();
        }
        public void SecondMonitor()
        {         
            ProgressBar drawing = new ProgressBar();
            drawing.HorizontalPB();
            drawing.VerticalPB();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Monitors actions = new Monitors();
            long num = long.Parse(args[0]);
            switch (num)
            {
                case 1:
                    while (true)
                    {
                        Console.Clear();
                        actions.FirstMonitor();
                        Thread.Sleep(100);
                    }
                case 2:
                    while (true)
                    {
                        Console.Clear();
                        actions.SecondMonitor();
                        Thread.Sleep(100);
                    }
                case 3:
                    while (true)
                    {
                        Console.Clear();
                        actions.FirstMonitor();
                        actions.SecondMonitor();
                        Thread.Sleep(10000);
                    }
                default:
                    break;
            }
        }
    }
}
