using System;
using System.Diagnostics;

namespace ConsoleTaskManager
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Started working.");
            TaskManager TM = null;
            switch (args.Length)
            {
                case 0:
                    TM = new TaskManager();
                    TM.kill = false;
                    TM.Do();
                    break;
                case 1:
                    TM = new TaskManager(args[0]);
                    TM.kill = false;
                    TM.Do();
                    break;
                case 2:
                    if (args[0] == "KILL" | args[0] == "K")
                    {
                        TM = new TaskManager(args[1]);
                        TM.kill = true;
                        TM.Do();
                        break;
                    }
                    if (args[0] == "START" | args[0] == "S")
                    {
                        try
                        {
                            Process notepad = Process.Start(args[1]);
                            Console.WriteLine($"Started process {notepad.ProcessName}. ID {notepad.Id}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    }
                    Console.WriteLine($"Incorrect first argument. {args[1]}.");
                    break;
                default:
                    Console.WriteLine("Arguments overflow.");
                    break;
            }
            Console.WriteLine("\nCompleted.");
            return 0;
        }
    }
}
