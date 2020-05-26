using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using pimp.ECS;
using pimp.ECS.Systems;
using pimp.IO;

namespace pimp
{
    public static class Program
    {
        public static HashSet<AbstractSystem> Systems = new HashSet<AbstractSystem>();
        public static void Main()
        {
            Console.Title = "Pimp - The Transgender Manager";
            Systems.Add(new SshSystem());
            //Systems.Add(new RemoteCommandSystem());
            Systems.Add(new HardwareUpdateSystem());
            Database.LoadHosts();

            foreach (var system in Systems)
                system.UpdateAsync();

            while (true)
            {
                var command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                    continue;

                foreach (var system in Systems)
                    system.UpdateAsync();

                Span<string> split = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                ProcessCommand(split);
            }
        }

        private static void ProcessCommand(Span<string> parts)
        {
            var command = parts[0];
            var args = parts.Slice(1);

            switch (command)
            {
                case "clear":
                {
                    UI.Clear();
                    break;
                }
                case "ls":
                    {
                        UI.PrintHosts();
                        break;
                    }
                case "cat":
                    {
                        if (Global.SelectedHostIndex != -1)
                        {
                            UI.PrintSelectedHost();
                        }
                        else
                        {
                            if (int.TryParse(args[0], out var index))
                            {
                                UI.PrintHost(index);
                            }
                            else
                            {
                                Console.WriteLine("please select a transgender first. (`pimp ls` and `pimp select [index]`)");
                            }
                        }
                        break;
                    }
                case "select":
                    {
                        if (args.Length == 0)
                            break;

                        int index = int.Parse(args[0]);
                        Global.SelectedHostIndex = index;
                        Console.WriteLine($"Selected {Global.Hosts[index].GetSshComponent().Hostname}!");
                        break;
                    }
            }
        }
    }
}
