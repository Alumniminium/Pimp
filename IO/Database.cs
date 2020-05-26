using System.IO;
using pimp.ECS.Components;

namespace pimp.IO
{
    public static class Database
    {
        public static void LoadHosts()
        {
            foreach (var line in File.ReadAllLines("hosts.csv"))
            {
                var split = line.Split(",");
                if (split[0].StartsWith('#'))
                    continue;

                var hostname = split[0];
                var sshUsername = split[1];
                var sshPort = ushort.Parse(split[2]);

                var host = new Host
                {
                    Id = Global.Hosts.Count
                };
                ref SshComponent sshComponent = ref host.GetSshComponent();
                sshComponent.Hostname=hostname;
                sshComponent.SshUsername=sshUsername;
                sshComponent.SshPort=sshPort;

                Global.Hosts.Add(host.Id,host);
            }
        }
    }
}
