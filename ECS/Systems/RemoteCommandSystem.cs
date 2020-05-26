using System;
using pimp.ECS;
using pimp.Enums;

namespace pimp
{
    public class RemoteCommandSystem : AbstractSystem
    {
        public override void Update()
        {
            Busy = true;
            foreach (var (index, owner) in Global.RemoteCommandComponents.GetInUse())
            {
                ref var component = ref Global.RemoteCommandComponents[index];
                var entity = Global.Hosts[owner];

                ref var ssh = ref entity.GetSshComponent();
                if (ssh.ConnectionState == SshConnectionState.Connected)
                {
                    Console.WriteLine(ssh.SshClient.RunCommand(component.Command).Result.Trim());
                    Global.RemoteCommandComponents.ReturnFor(owner);
                }
            }
            Busy = false;
        }
    }
}
