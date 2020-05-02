using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Tyndall.DiskPart
{
    public static class Commands
    {
        /// <summary>
        /// The separator character between DiskPart results lines.
        /// </summary>
        private const char DiskPartResultsSeparator = '\n';

        /// <summary>
        /// Path to DiskPart.
        /// </summary>
        private static readonly string DiskPartPath = Environment.SystemDirectory + @"\diskpart.exe";

        /// <summary>
        /// Path to temporary Results file.
        /// </summary>
        private static readonly string DiskPartResultsPath = Path.GetTempPath() + @"diskpart.tmp";

        /// <summary>
        /// Lists disks similar to the LIST DISK command.
        /// </summary>
        /// <param name="identifier">The text to specify a Disk. The default value is "Disk".</param>
        /// <param name="diskPartResultsSeparator">The separator character between DiskPart results lines. The default value is a newline.</param>
        /// <returns>A list of <c>Disk</c>s, as specified by a DiskPart LIST DISK command.</returns>
        public static List<Disk> ListDisk(string identifier = "Disk", char diskPartResultsSeparator = DiskPartResultsSeparator)
        {
            var diskPartCommand = $"RESCAN{Environment.NewLine}LIST {identifier.ToUpper()}{Environment.NewLine}EXIT";

            var diskPartResults = Run(diskPartCommand).Split(diskPartResultsSeparator);

            var disks = new List<Disk>();

            foreach (var diskPartResultLine in diskPartResults)
            {
                var diskMatch = Regex.Match(diskPartResultLine, $@"{identifier} \d+");

                if (diskMatch.Success)
                {
                    disks.Add(new Disk(diskPartResultLine));
                }
            }

            return disks;
        }

        /// <summary>
        /// Lists volumes similar to the LIST VOLUME command.
        /// </summary>
        /// <param name="identifier">The text to specify a Volume. The default value is "Volume".</param>
        /// <param name="diskPartResultsSeparator">The separator character between DiskPart results lines. The default value is a newline.</param>
        /// <returns>A list of <c>Volume</c>s, as specified by a DiskPart LIST VOLUME command.</returns>
        public static List<Volume> ListVolume(string identifier = "Volume", char diskPartResultsSeparator = DiskPartResultsSeparator)
        {
            var diskPartCommand = $"RESCAN{Environment.NewLine}LIST {identifier.ToUpper()}{Environment.NewLine}EXIT";

            var diskParResults = Run(diskPartCommand).Split(diskPartResultsSeparator);

            var volumes = new List<Volume>();

            foreach (var diskPartResultLine in diskParResults)
            {
                var volumeMatch = Regex.Match(diskPartResultLine, $@"{identifier} \d+");

                if (volumeMatch.Success)
                {
                    volumes.Add(new Volume(diskPartResultLine));
                }
            }

            return volumes;
        }

        /// <summary>
        /// List partitions similar to the LIST PARTITION command.
        /// </summary>
        /// <param name="diskIndex">The index of the Disk to list partitions for.</param>
        /// <param name="identifier">The text to specify a Partition. The default value is "Partition."</param>
        /// <param name="diskPartResultsSeparator">The separator character between DiskPart results lines. The default value is a newline.</param>
        /// <returns>A list of <c>Partition</c>s, as specified by a DiskPart LIST PARTITION command.</returns>
        public static List<Partition> ListPartition(int diskIndex, string identifier = "Partition", char diskPartResultsSeparator = DiskPartResultsSeparator)
        {
            var diskPartCommand = $"RESCAN{Environment.NewLine}SELECT DISK {diskIndex}{Environment.NewLine}LIST {identifier.ToUpper()}{Environment.NewLine}EXIT";

            var diskParResults = Run(diskPartCommand).Split(diskPartResultsSeparator);

            var partitions = new List<Partition>();

            foreach (var diskPartResultLine in diskParResults)
            {
                var partitionMatch = Regex.Match(diskPartResultLine, $@"{identifier} \d+");

                if (partitionMatch.Success)
                {
                    partitions.Add(new Partition(diskPartResultLine));
                }
            }

            return partitions;
        }

        /// <summary>
        /// Displays the properties of the specified partition on the specified disk similar to the DETAIL DISK command.
        /// </summary>
        /// <param name="diskIndex">The Index of the disk to select.</param>
        /// <param name="diskPartResultsSeparator">The separator character between DiskPart results lines. The default value is a newline.</param>
        /// <returns>A list of <c>DiskDetail</c>s, as specified by a DiskPart DETAIL DISK command.</returns>
        public static DiskDetail DetailDisk(int diskIndex, char diskPartResultsSeparator = DiskPartResultsSeparator)
        {
            var diskPartCommand = $"RESCAN{Environment.NewLine}SELECT DISK {diskIndex}{Environment.NewLine}DETAIL DISK{Environment.NewLine}EXIT";

            var diskPartResults = Run(diskPartCommand, true).Split(diskPartResultsSeparator);

            var diskDetails = new DiskDetail(diskPartResults);

            return diskDetails;
        }

        /// <summary>
        /// Displays the properties of the specified partition on the specified disk similar to the DETAIL PARTITION command.
        /// </summary>
        /// <param name="diskIndex">The Index of the disk to select.</param>
        /// <param name="partitionIndex">The Index of the partition to select.</param>
        /// <param name="diskPartResultsSeparator">The separator character between DiskPart results lines. The default value is a newline.</param>
        /// <returns>A list of <c>PartitionDetail</c>s, as specified by a DiskPart DETAIL PARTITION command.</returns>
        public static PartitionDetail DetailPartition(int diskIndex, int partitionIndex, char diskPartResultsSeparator = DiskPartResultsSeparator)
        {
            var diskPartCommand = $"RESCAN{Environment.NewLine}SELECT DISK {diskIndex}{Environment.NewLine}SELECT PARTITION {partitionIndex}{Environment.NewLine}DETAIL PARTITION{Environment.NewLine}EXIT";

            var diskPartResults = Run(diskPartCommand, true).Split(diskPartResultsSeparator);

            var partitionDetails = new PartitionDetail(diskPartResults);

            return partitionDetails;
        }

        /// <summary>
        /// Runs a DiskPart RESCAN command.
        /// </summary>
        public static void Rescan(bool writeResultsToFile = false)
        {
            Run($"RESCAN{Environment.NewLine}EXIT", writeResultsToFile);
        }

        /// <summary>
        /// Runs a DiskPart command.
        /// </summary>
        /// <param name="command">The DiskPart command, with each command separated by Environment.NewLine.</param>
        /// <param name="writeResultsToFile">Specifies whether or not to write results to a temporary file. The default value is FALSE.</param>
        public static string Run(string command, bool writeResultsToFile = false, string diskPartPath = "", string diskPartResultsPath = "")
        {
            if(string.IsNullOrEmpty(diskPartPath))
            {
                diskPartPath = DiskPartPath;
            }

            if(!File.Exists(diskPartPath))
            {
                throw new FileNotFoundException(diskPartPath);
            }

            if(string.IsNullOrEmpty(diskPartResultsPath))
            {
                diskPartResultsPath = DiskPartResultsPath;
            }

            if(File.Exists(diskPartResultsPath))
            {
                //File.Delete(diskPartResultsPath);
            }

            var commandArray = command.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var process = new Process
            {
                StartInfo =
                {
                    FileName = diskPartPath,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                }
            };

            process.Start();

            foreach (var c in commandArray)
            {
                process.StandardInput.WriteLine(c);
            }

            process.WaitForExit();

            var diskPartResults = process.StandardOutput.ReadToEnd();

            if (writeResultsToFile)
            {
                File.WriteAllText(diskPartResultsPath, diskPartResults);
            }

            return diskPartResults;
        }
    }
}
