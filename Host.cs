using System;
using pimp.ECS;
using pimp.Enums;
using pimp.Helpers;

namespace pimp
{
    public class Host : Entity
    {
        public bool Connected => GetSshComponent().ConnectionState == SshConnectionState.Connected;
        public Host()
        {
            Id = Global.Hosts.Count;
            GetSshComponent();
            GetHardwareComponent();
            GetServiceComponent();
            GetConfigComponent();
        }

        public string ExecuteCommand(string command)
        {
            ref var ssh = ref GetSshComponent();
            if (ssh.ConnectionState == SshConnectionState.Connected)
                return ssh.SshClient.RunCommand(command).Result;
            return "not connected";
        }

        public override string ToString()
        {
            ref var ssh = ref GetSshComponent();
            ref var hw = ref GetHardwareComponent();
            return $"[{Id}] {ssh.Hostname.ToLength(20)} " +
            $"CPUs: {hw.CPUs.ToString().ToLength(3, true)} " +
            $"Usage: {hw.CpuUsage.ToString().ToLength(3, true)}%, " +
            $"RAM: {(hw.TotalMemoryMb - hw.FreeMemoryMb + "/" + hw.TotalMemoryMb + "MB (" + ((hw.TotalMemoryMb - hw.FreeMemoryMb) * 100 / Math.Max(1, hw.TotalMemoryMb)).ToString().ToLength(2, true) + "%)").ToLength(18, true)}, " +
            $"Disk: {hw.DiskUsage.ToString().ToLength(2, true)}%, " +
            "Connection State: " + ssh.ConnectionState;
        }
    }
}