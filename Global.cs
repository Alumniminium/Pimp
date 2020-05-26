using System.Collections.Generic;
using pimp.ECS;
using pimp.ECS.Components;

namespace pimp
{
    public static class Global
    {
        public static Dictionary<int,Host> Hosts = new Dictionary<int,Host>();
        public static PooledComponentCollection<HardwareComponent> HardwareComponents = new PooledComponentCollection<HardwareComponent>(32);
        public static PooledComponentCollection<ServiceComponent> ServiceComponents = new PooledComponentCollection<ServiceComponent>(64);
        public static PooledComponentCollection<ConfigComponent> ConfigComponents = new PooledComponentCollection<ConfigComponent>(32);
        public static PooledComponentCollection<SshComponent> SshComponents = new PooledComponentCollection<SshComponent>(32);
        public static PooledComponentCollection<RemoteCommandComponent> RemoteCommandComponents = new PooledComponentCollection<RemoteCommandComponent>(128);
        public static int SelectedHostIndex=-1;
    }
}
