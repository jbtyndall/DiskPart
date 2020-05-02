using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tyndall.DiskPart
{
    public class DiskDetail : DiskPartObjectDetail
    {
        /// <summary>
        /// A dictionary of parsing info for <c>DiskDetail</c> properties.
        /// </summary>
        private readonly Dictionary<string, string> ParseInfo = new Dictionary<string, string>
        {
            ["DiskId"] = "Disk ID",
            ["Type"] = "Type",
            ["Status"] = "Status",
            ["Path"] = "Path",
            ["Target"] = "Target",
            ["LunId"] = "LUN ID",
            ["LocationPath"] = "Location Path",
            ["CurrentReadOnlyState"] = "Current Read-only State",
            ["ReadOnly"] = "Read-only",
            ["BootDisk"] = "Boot Disk",
            ["PagefileDisk"] = "Pagefile Disk",
            ["HibernationFileDisk"] = "Hibernation File Disk",
            ["CrashdumpDisk"] = "Crashdump Disk",
            ["ClusteredDisk"] = "Clustered Disk"
        };

        /// <summary>
        /// The Disk ID of the disk.
        /// </summary>
        public string DiskId { get; set; }

        /// <summary>
        /// The Type of the disk.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Status of the disk.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The Path of the disk.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The Target of the disk.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The LUN ID of the disk.
        /// </summary>
        public string LunId { get; set; }
        
        /// <summary>
        /// The Location Path of the disk.
        /// </summary>
        public string LocationPath { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is in Current read-only state.
        /// </summary>
        public bool CurrentReadOnlyState { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is read-only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is a boot disk.
        /// </summary>
        public bool BootDisk { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is a pagefile disk.
        /// </summary>
        public bool PagefileDisk { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is a hibernation file disk.
        /// </summary>
        public bool HibernationFileDisk { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is a crashdump disk.
        /// </summary>
        public bool CrashdumpDisk { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is a clustered disk.
        /// </summary>
        public bool ClusteredDisk { get; set; }

        /// <summary>
        /// A list of volumes on the disk.
        /// </summary>
        public List<Volume> Volumes { get; set; }

        /// <summary>
        /// Instaniates a new <c>DiskDetail</c> from the specified DiskPart results.
        /// </summary>
        /// <param name="diskPartDetailDiskResults">The DiskPart results (i.e., output) from a DETAIL DISK command.</param>
        public DiskDetail(string[] diskPartDetailDiskResults)
        {
            DisplayName = ParseDisplayName(diskPartDetailDiskResults, ParseInfo["DiskId"]);

            DiskId = ParseProperty(ParseInfo["DiskId"], diskPartDetailDiskResults);

            Type = ParseProperty(ParseInfo["Type"], diskPartDetailDiskResults);

            Status = ParseProperty(ParseInfo["Status"], diskPartDetailDiskResults);

            Path = ParseProperty(ParseInfo["Path"], diskPartDetailDiskResults);

            Target = ParseProperty(ParseInfo["Target"], diskPartDetailDiskResults);

            LunId = ParseProperty(ParseInfo["LunId"], diskPartDetailDiskResults);

            LocationPath = ParseProperty(ParseInfo["LocationPath"], diskPartDetailDiskResults);

            CurrentReadOnlyState = ParseYesNoAsBoolean(ParseProperty(ParseInfo["CurrentReadOnlyState"], diskPartDetailDiskResults));

            ReadOnly = ParseYesNoAsBoolean(ParseProperty(ParseInfo["ReadOnly"], diskPartDetailDiskResults));

            BootDisk = ParseYesNoAsBoolean(ParseProperty(ParseInfo["BootDisk"], diskPartDetailDiskResults));

            PagefileDisk = ParseYesNoAsBoolean(ParseProperty(ParseInfo["PagefileDisk"], diskPartDetailDiskResults));

            HibernationFileDisk = ParseYesNoAsBoolean(ParseProperty(ParseInfo["HibernationFileDisk"], diskPartDetailDiskResults));

            CrashdumpDisk = ParseYesNoAsBoolean(ParseProperty(ParseInfo["CrashdumpDisk"], diskPartDetailDiskResults));

            ClusteredDisk = ParseYesNoAsBoolean(ParseProperty(ParseInfo["ClusteredDisk"], diskPartDetailDiskResults));

            Volumes = ParseVolumes(diskPartDetailDiskResults);
        }
    }
}
