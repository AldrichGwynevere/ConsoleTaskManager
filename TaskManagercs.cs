using System;
using System.Diagnostics;

namespace ConsoleTaskManager
{
    class TaskManager : Process
    {
        public string name = string.Empty; 
        public string info = string.Empty; 
        public bool kill = false; 
        public int ID = -1; 
        Process[] lAll; 
        public Process[] localAll 
        {
            get { return lAll; } 
            set { lAll = value; } 
        }
        public TaskManager()
        {
            try
            {
                localAll = Process.GetProcesses(); 
                info = "Getting process list."; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
            }
        }

        public TaskManager(string _name)
        {
            if (int.TryParse(_name, out ID)) 
            {
                try 
                {
                    Process _p = Process.GetProcessById(ID); 
                    info = $"Giving info about process ID {ID}.";
                    name = Process.GetProcessById(ID).ProcessName; 
                    if (localAll != null) 
                    {
                        Array.Clear(localAll, 0, localAll.Length); 
                        localAll[0] = Process.GetProcessById(ID); 
                    }
                    else
                    {
                        localAll = new Process[] { _p }; 
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                name = _name; 
                info = $"Giving info about all processes with name {name}.";
                ID = -1; 
                try
                {
                    localAll = Process.GetProcessesByName(name);  
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString()); 
                }
            }
        }
      
        private void WriteConsole()
        {
            Console.WriteLine(info); 

            Console.WriteLine("ID:    Name of Process"); 
            int i = 1;

            foreach (var la in localAll) 
            {
                Console.WriteLine(string.Format("{0,6} {1}", la.Id, la.ProcessName)); 
                if (i % 23 == 0)
                {
                    Console.WriteLine("----- CONTINUE - PRESS ANY KEY -----"); 
                    Console.WriteLine("----- QUIT - PRESS \"Q\" -----");
                    ConsoleKeyInfo _c = Console.ReadKey(true);
                    if (_c.Key == ConsoleKey.Q)
                        break;
                }
                i++;
            }
        }
        
        public void Do()
        {
            if (kill) 
            {
                if (localAll == null) 
                    return;
                string _name = name; 
                int _id = ID; 
                foreach (var lP in localAll)
                {
                    try
                    {
                        lP.Kill(); 
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.ToString()); 
                        break; 
                    }
                }
                if (_id == -1) 
                    info = $"Deleted processes with name {_name}.";
                else 
                    info = $"Deleted process ID {_id}"; 
                return;
            }

            WriteConsole(); 
        }
    }
}
