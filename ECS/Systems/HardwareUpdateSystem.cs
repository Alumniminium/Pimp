using System;
using pimp.ECS.Components;
using pimp.Enums;

namespace pimp.ECS.Systems
{
    public class HardwareUpdateSystem : AbstractSystem
    {
        public override void Update()
        {
            Busy = true;
            foreach (var (index, owner) in Global.HardwareComponents.GetInUse())
            {
                var host = Global.Hosts[owner];
                if (!host.Connected)
                    continue;

                ref var component = ref Global.HardwareComponents[index];

                var cpuCount = GetCpuCount(host);
                var cpuUsage = GetCpuUsage(host);
                int diskUsage = GetDiskUsage(host);
                var (totalMemory, availableMemory) = GetMemoryStats(host);

                component.TotalMemoryMb = totalMemory;
                component.FreeMemoryMb = availableMemory;
                component.CPUs = cpuCount;
                component.CpuUsage = cpuUsage;
                component.DiskUsage = diskUsage;
            }
            Busy = false;
        }

        private static int GetDiskUsage(Host entity)
        {
            var df = entity.ExecuteCommand("df -lh | awk '{if ($6 == \"/\") {print $5}}'");
            return int.Parse(df.Split('%')[0]);
        }

        private static float GetCpuUsage(Host entity)
        {
            var top = entity.ExecuteCommand("top -b -n1 | grep \"Cpu(s)\" | awk '{print $2 + $4}'").Split('\n', StringSplitOptions.RemoveEmptyEntries)[0];
            return (float)float.Parse(top);
        }

        private static int GetCpuCount(Host entity)
        {
            var cat = entity.ExecuteCommand("cat /sys/devices/system/cpu/possible");
            var cat_parts = cat.Split(new[] { '-', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var cpuCount = 1;

            if (cat_parts.Length == 2)
            {
                int low = int.Parse(cat_parts[0]);
                int high = int.Parse(cat_parts[1]);
                cpuCount = (high - low) + 1;
            }

            return cpuCount;
        }
        public static (int totalMem, int availableMem) GetMemoryStats(Host entity)
        {
            var free = entity.ExecuteCommand("free");
            var free_parts = free.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var totalMemory = (int)(long.Parse(free_parts[7]) / 1024);
            var availableMemory = (int)(long.Parse(free_parts[12]) / 1024);
            return (totalMemory, availableMemory);
        }
    }
}