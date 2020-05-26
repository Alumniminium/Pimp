using pimp.ECS;
using pimp.ECS.Components;
using pimp.Enums;
using Renci.SshNet;

namespace pimp
{
    public class SshSystem : AbstractSystem
    {
        public override void Update()
        {
            Busy=true;
            foreach (var (index, _) in Global.SshComponents.GetInUse())
            {
                ref var component = ref Global.SshComponents[index];

                if (component.ConnectionState == SshConnectionState.Disconnected)
                    Connect(ref component);
            }
            Busy=false;
        }
        public void Connect(ref SshComponent sshComponent)
        {
            sshComponent.ConnectionState = SshConnectionState.Connecting;
            sshComponent.SshConnection = new ConnectionInfo(sshComponent.Hostname, sshComponent.SshPort, sshComponent.SshUsername, new PrivateKeyAuthenticationMethod(sshComponent.SshUsername, new PrivateKeyFile("/home/alumni/.ssh/id_rsa")));
            sshComponent.SshClient = new SshClient(sshComponent.SshConnection);
            sshComponent.SshClient.Connect();

            var echo = sshComponent.SshClient.CreateCommand("echo 'connection test'");
            var respone = echo.Execute().Trim();

            if (respone == "connection test")
                sshComponent.ConnectionState = SshConnectionState.Connected;
            else
                sshComponent.ConnectionState = SshConnectionState.Disconnected;
        }
    }
}
