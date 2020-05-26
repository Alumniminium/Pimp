using System;

namespace pimp
{
    public static class UI
    {
        public static void PrintHosts()
        {
            foreach (var host in Global.Hosts)
                Console.WriteLine(host.Value);
        }
        public static void PrintSelectedHost() => PrintHost(Global.SelectedHostIndex);

        public static void PrintHost(int index)
        {
            if (index < 0 || index >= Global.Hosts.Count)
                return;
            Console.WriteLine(Global.Hosts[index]);
        }

        internal static void Clear()
        {
            Console.Clear();
        }
    }
}
