using System;
using pimp.ECS.Components;

namespace pimp.ECS
{
    public class Entity
    {
        public int Id;
        public ref HardwareComponent GetHardwareComponent() => ref Global.HardwareComponents.GetFor(Id);
        public ref ServiceComponent GetServiceComponent() => ref Global.ServiceComponents.GetFor(Id);
        public ref ConfigComponent GetConfigComponent() => ref Global.ConfigComponents.GetFor(Id);
        public ref SshComponent GetSshComponent() => ref Global.SshComponents.GetFor(Id);
        public ref RemoteCommandComponent GetRemoteCommandComponent()=>ref Global.RemoteCommandComponents.GetFor(Id);
    }
}