package com.company;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.lang.management.ManagementFactory;
import java.lang.management.OperatingSystemMXBean;
import java.util.Scanner;

public class Main {

    private static OperatingSystemMXBean bean =
            (com.sun.management.OperatingSystemMXBean)ManagementFactory.getOperatingSystemMXBean();
    private static float memoryPercent;
    private static float cpuPercent;

    public static void process() {
        try {
            String line;
            Process p = Runtime.getRuntime().exec
                    (System.getenv("windir") +"\\system32\\"+"tasklist.exe");
            BufferedReader input =
                    new BufferedReader(new InputStreamReader(p.getInputStream()));
            while ((line = input.readLine()) != null) {
                System.out.println(line); //<-- Parse data here.
            }
            input.close();
        } catch (Exception err) {
            err.printStackTrace();
        }
    }

    public static void cpu(){
        while (true) {
            try {
                Thread.sleep(1000);
            }
            catch (InterruptedException ex) {
                System.out.println("Interrupted Exception");
            }
            cpuLoad();
            memoryLoad();

        }
    }
    private static void cpuLoad() {
        cpuPercent = (float)
                ((com.sun.management.OperatingSystemMXBean) bean).getSystemCpuLoad() * 100;
        System.out.println("Cpu Load: " + (int) cpuPercent + "%");
        System.out.print("[");
        for (int i = 0; i < (((int) cpuPercent * 70) / 100); ++i) {
            System.out.print("|");
        }
        System.out.println("]");

    }
    private static void memoryLoad() {
        memoryPercent = ((float)(((com.sun.management.OperatingSystemMXBean) bean).getTotalPhysicalMemorySize() -
                ((com.sun.management.OperatingSystemMXBean) bean).getFreePhysicalMemorySize()) /
                (float)((com.sun.management.OperatingSystemMXBean) bean).getTotalPhysicalMemorySize()) * 100;
        System.out.println("Memory Load: " + (int)memoryPercent + "%");
        System.out.print("[");
        for (int i = 0; i < (((int) memoryPercent * 70) / 100); ++i) {
            System.out.print("|");
        }
        System.out.println("]\n");
    }

    public static void main(String[] args) {
        boolean lool = true;
        while (lool) {
            System.out.println("1 - Показать список процессов \n 2 - Нагрузка на ЦП и на память \n 3 - Вывести все сразу");
            Scanner in = new Scanner(System.in);
            int n = in.nextInt();
            switch (n) {
                case 1:
                    process();
                    break;
                case 2:
                    cpu();
                    break;
                case 3:
                    process();
                    cpu();
                    break;
                default:
                    System.out.println("Ошибка, введите число от 1 до 3");
            }

        }
    }
}
