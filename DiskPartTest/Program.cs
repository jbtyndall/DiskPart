using System;

namespace Tyndall.DiskPartTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // List Disks and Partitions

            Console.WriteLine($"Listing disks and partitions...");

            var disks = DiskPart.Commands.ListDisk();

            foreach (var disk in disks)
            {
                var partitions = DiskPart.Commands.ListPartition(disk.Index);

                Console.WriteLine($"Found disk. {{Index:'{disk.Index}'; Status:'{disk.Status}'; Size:'{disk.Size}'; Free:'{disk.Free}'; Dyn:'{disk.Dyn}'; Gpt:'{disk.Gpt}'}}");

                foreach(var partition in partitions)
                {
                    Console.WriteLine($"Found partition on disk {disk.Index}. {{Index:'{partition.Index}'; Type:'{partition.Type}'; Size:'{partition.Size}'; Offset:'{partition.Offset}'}}");
                }
            }

            // List Volumes

            Console.WriteLine($"Listing volumes...");

            var volumes = DiskPart.Commands.ListVolume();

            foreach(var volume in volumes)
            {
                Console.WriteLine($"Found volume. {{Index:'{volume.Index}'; Ltr:'{volume.Ltr}'; Label:'{volume.Label}'; Fs:'{volume.Fs}'; Type:'{volume.Type}'; Size:'{volume.Size}'; Status:'{volume.Status}'; Info:'{volume.Info}'}}");
            }

            // That's all, folks!

            Console.WriteLine("Press any key to continue...");

            Console.ReadLine();
        }
    }
}
