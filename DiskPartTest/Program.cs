using System;
using System.Collections.Generic;

namespace Tyndall.DiskPartTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // List Disks, any Partitions, and Disk Details

            Console.WriteLine($"LIST DISK");

            List<DiskPart.Disk> disks = DiskPart.Commands.ListDisk();

            foreach (DiskPart.Disk disk in disks)
            {
                List<DiskPart.Partition> partitions = DiskPart.Commands.ListPartition(disk.Index);

                Console.WriteLine($"Disk Index:'{disk.Index}'; Status:'{disk.Status}'; Size:'{disk.Size}'; Free:'{disk.Free}'; Dyn:'{disk.Dyn}'; Gpt:'{disk.Gpt}'");

                Console.WriteLine();

                Console.WriteLine($"LIST PARTITION (Disk {disk.Index})");

                foreach (DiskPart.Partition partition in partitions)
                {
                    Console.WriteLine($"Partition Index:'{partition.Index}'; Type:'{partition.Type}'; Size:'{partition.Size}'; Offset:'{partition.Offset}'");

                    Console.WriteLine();

                    Console.WriteLine($"DETAIL PARTITION (Disk {disk.Index}, Partition {partition.Index})");

                    DiskPart.PartitionDetail partitionDetails = DiskPart.Commands.DetailPartition(disk.Index, partition.Index);

                    Console.WriteLine($"DisplayName:'{partitionDetails.DisplayName}'; Type:'{partitionDetails.Type}'; Hidden:'{partitionDetails.Hidden}'; Required:'{partitionDetails.Required}'; Attrib:'{partitionDetails.Attrib}'; OffsetInBytes:'{partitionDetails.OffsetInBytes}'");

                    Console.WriteLine();

                    if (partitionDetails.Volumes.Count > 0)
                    {
                        Console.WriteLine("Disk Volumes:");

                        foreach (DiskPart.Volume volume in partitionDetails.Volumes)
                        {
                            Console.WriteLine($"Volume Index:'{volume.Index}'; Ltr:'{volume.Ltr}'; Label:'{volume.Label}'; Fs:'{volume.Fs}'; Type:'{volume.Type}'; Size:'{volume.Size}'; Status:'{volume.Status}'; Info:'{volume.Info}'");
                        }

                        Console.WriteLine();
                    }
                }

                Console.WriteLine();

                Console.WriteLine($"DETAIL DISK (Disk {disk.Index})");

                var diskDetails = DiskPart.Commands.DetailDisk(disk.Index);

                Console.WriteLine($"DisplayName:'{diskDetails.DisplayName}'; DiskId:'{diskDetails.DiskId}'; Type:'{diskDetails.Type}'; Status:'{diskDetails.Status}'; Path:'{diskDetails.Path}'; Target:'{diskDetails.Target}'; LunId:'{diskDetails.LunId}'; LocationPath:'{diskDetails.LocationPath}'; CurrentReadOnlyState:'{diskDetails.CurrentReadOnlyState}'; ReadOnly:'{diskDetails.ReadOnly}'; BootDisk:'{diskDetails.BootDisk}'; PagefileDisk:'{diskDetails.PagefileDisk}'; HibernationFileDisk:'{diskDetails.HibernationFileDisk}'; CrashdumpDisk:'{diskDetails.CrashdumpDisk}'; ClusteredDisk:'{diskDetails.ClusteredDisk}'");

                Console.WriteLine();

                Console.WriteLine("Disk Volumes:");

                foreach (DiskPart.Volume volume in diskDetails.Volumes)
                {
                    Console.WriteLine($"Volume Index:'{volume.Index}'; Ltr:'{volume.Ltr}'; Label:'{volume.Label}'; Fs:'{volume.Fs}'; Type:'{volume.Type}'; Size:'{volume.Size}'; Status:'{volume.Status}'; Info:'{volume.Info}'");

                    var volumeDetails = DiskPart.Commands.DetailVolume(disk.Index, volume.Index);

                    Console.WriteLine();

                    Console.WriteLine($"DETAIL VOLUME (Disk {disk.Index}, Volume {volume.Index})");

                    Console.WriteLine($"DisplayName: {volumeDetails.DisplayName}; ReadOnly:'{volumeDetails.ReadOnly}'; Hidden:'{volumeDetails.Hidden}'; NoDefaultDriveLetter:'{volumeDetails.NoDefaultDriveLetter}'; ShadowCopy:'{volumeDetails.ShadowCopy}'; Offline:'{volumeDetails.Offline}'; BitlockerEncrypted:'{volumeDetails.BitLockerEncrypted}'; Installable:'{volumeDetails.Installable}'; VolumeCapacity:'{volumeDetails.VolumeCapacity}'; VolumeFreeSpace:'{volumeDetails.VolumeFreeSpace}'");

                    if (volumeDetails.Disks.Count > 0)
                    {
                        Console.WriteLine("Disks:");

                        foreach(var volumeDisk in volumeDetails.Disks)
                        {
                            Console.WriteLine($"Disk Index:'{volumeDisk.Index}'; Status:'{volumeDisk.Status}'; Size:'{volumeDisk.Size}'; Free:'{volumeDisk.Free}'; Dyn:'{volumeDisk.Dyn}'; Gpt:'{volumeDisk.Gpt}'");
                        }

                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
            }

            // List Volumes

            Console.WriteLine($"LIST VOLUME");

            var volumes = DiskPart.Commands.ListVolume();

            foreach (DiskPart.Volume volume in volumes)
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
