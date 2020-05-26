using pimp.Enums;
using Renci.SshNet;

namespace pimp.ECS.Components
{
    public struct SshComponent
    {
        public string Hostname;
        public string SshUsername;
        public ushort SshPort;
        public ConnectionInfo SshConnection;
        public SshClient SshClient;
        public SshConnectionState ConnectionState;
    }
}
