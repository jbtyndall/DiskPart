using System;

namespace Tyndall.DiskPartTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // List Disks, any Partitions, and Disk Details

            Console.WriteLine($"LIST DISK");

            var disks = DiskPart.Commands.ListDisk();

            foreach (var disk in disks)
            {
                var partitions = DiskPart.Commands.ListPartition(disk.Index);

                Console.WriteLine($"Disk Index:'{disk.Index}'; Status:'{disk.Status}'; Size:'{disk.Size}'; Free:'{disk.Free}'; Dyn:'{disk.Dyn}'; Gpt:'{disk.Gpt}'");

                Console.WriteLine();

                Console.WriteLine($"LIST PARTITION (Disk {disk.Index})");

                foreach(var partition in partitions)
                {
                    Console.WriteLine($"Partition Index:'{partition.Index}'; Type:'{partition.Type}'; Size:'{partition.Size}'; Offset:'{partition.Offset}'");
                }

                Console.WriteLine();

                Console.WriteLine($"DETAIL DISK (Disk {disk.Index})");

                var diskDetails = DiskPart.Commands.DetailDisk(disk.Index);

                Console.WriteLine($"DisplayName:'{diskDetails.DisplayName}'; DiskId:'{diskDetails.DiskId}'; Type:'{diskDetails.Type}'; Status:'{diskDetails.Status}'; Path:'{diskDetails.Path}'; Target:'{diskDetails.Target}'; LunId:'{diskDetails.LunId}'; LocationPath:'{diskDetails.LocationPath}'; CurrentReadOnlyState:'{diskDetails.CurrentReadOnlyState}'; ReadOnly:'{diskDetails.ReadOnly}'; BootDisk:'{diskDetails.BootDisk}'; PagefileDisk:'{diskDetails.PagefileDisk}'; HibernationFileDisk:'{diskDetails.HibernationFileDisk}'; CrashdumpDisk:'{diskDetails.CrashdumpDisk}'; ClusteredDisk:'{diskDetails.ClusteredDisk}'");

                Console.WriteLine();

                Console.WriteLine("Disk Volumes:");

                foreach(var volume in diskDetails.Volumes)
                {
                    Console.WriteLine($"Volume Index:'{volume.Index}'; Ltr:'{volume.Ltr}'; Label:'{volume.Label}'; Fs:'{volume.Fs}'; Type:'{volume.Type}'; Size:'{volume.Size}'; Status:'{volume.Status}'; Info:'{volume.Info}'");
                }

                Console.WriteLine();
            }

            // List Volumes

            Console.WriteLine($"LIST VOLUME");

            var volumes = DiskPart.Commands.ListVolume();

            foreach(var volume in volumes)
            {
                Console.WriteLine($"Volume Index:'{volume.Index}'; Ltr:'{volume.Ltr}'; Label:'{volume.Label}'; Fs:'{volume.Fs}'; Type:'{volume.Type}'; Size:'{volume.Size}'; Status:'{volume.Status}'; Info:'{volume.Info}'");
            }

            // That's all, folks!

            Console.WriteLine();

            Console.WriteLine("Press any key to continue...");

            Console.ReadLine();
        }
    }
}
