using System;
using System.Collections.Generic;
using System.Text;

namespace Tyndall.DiskPart
{
    /// <summary>
    /// Displays the disks on which the current volume resides.
    /// </summary>
    public class VolumeDetail : DiskPartObjectDetail
    {
        /// <summary>
        /// A dictionary of parsing info for <c>VolumeDetail</c> properties.
        /// </summary>
        private readonly Dictionary<string, string> ParseInfo = new Dictionary<string, string>
        {
            ["ReadOnly"] = "Read-only",
            ["Hidden"] = "Hidden",
            ["NoDefaultDriveLetter"] = "No Default Drive Letter",
            ["ShadowCopy"] = "Shadow Copy",
            ["Offline"] = "Offline",
            ["BitLockerEncrypted"] = "BitLocker Encrypted",
            ["Installable"] = "Installable",
            ["VolumeCapacity"] = "Volume Capacity",
            ["VolumeFreeSpace"] = "Volume Free Space"
        };

        /// <summary>
        /// Specifies that the volume is read-only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Specifies that the volume is hidden.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Specifies that the volume does not receive a drive letter by default.
        /// </summary>
        public bool NoDefaultDriveLetter { get; set; }

        /// <summary>
        /// Specifies that the volume is a shadow copy volume. 
        /// </summary>
        public bool ShadowCopy { get; set; }

        /// <summary>
        /// Specifies that the volume is offline.
        /// </summary>
        public bool Offline { get; set; }

        /// <summary>
        /// Specifies that the volume is BitLocker-encrypted.
        /// </summary>
        public bool BitLockerEncrypted { get; set; }

        /// <summary>
        /// Specifies that the volume is installable.
        /// </summary>
        public bool Installable { get; set; }

        /// <summary>
        /// The capacity of the volume.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string VolumeCapacity { get; set; }

        /// <summary>
        /// The amount of free space left on the volume.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string VolumeFreeSpace { get; set; }

        /// <summary>
        /// The list of disks on which the current volume resides.
        /// </summary>
        public List<Disk> Disks { get; set; }

        /// <summary>
        /// Instantiates a new <c>VolumeDetail</c> from the specified DiskPart results.
        /// </summary>
        /// <param name="diskPartDetailVolumeResults">The DiskPart results (i.e., output) from a DETAIL VOLUME command.</param>
        public VolumeDetail(string[] diskPartDetailVolumeResults)
        {
            Disks = ParseDisks(diskPartDetailVolumeResults);

            ReadOnly = ParseYesNoAsBoolean(ParseProperty(ParseInfo["ReadOnly"], diskPartDetailVolumeResults));

            Hidden = ParseYesNoAsBoolean(ParseProperty(ParseInfo["Hidden"], diskPartDetailVolumeResults));

            NoDefaultDriveLetter = ParseYesNoAsBoolean(ParseProperty(ParseInfo["NoDefaultDriveLetter"], diskPartDetailVolumeResults));

            ShadowCopy = ParseYesNoAsBoolean(ParseProperty(ParseInfo["ShadowCopy"], diskPartDetailVolumeResults));

            BitLockerEncrypted = ParseYesNoAsBoolean(ParseProperty(ParseInfo["BitLockerEncrypted"], diskPartDetailVolumeResults));

            Installable = ParseYesNoAsBoolean(ParseProperty(ParseInfo["Installable"], diskPartDetailVolumeResults));

            VolumeCapacity = ParseProperty(ParseInfo["VolumeCapacity"], diskPartDetailVolumeResults);

            VolumeFreeSpace = ParseProperty(ParseInfo["VolumeFreeSpace"], diskPartDetailVolumeResults);
        }

        /// <summary>
        /// Instantiates a new <c>VolumeDetail</c> from the specified DiskPart results.
        /// </summary>
        /// <param name="index">The Index of the volume.</param>
        /// <param name="diskPartDetailVolumeResults">The DiskPart results (i.e., output) from a DETAIL VOLUME command.</param>
        public VolumeDetail(int index, string[] diskPartDetailVolumeResults)
        {
            DisplayName = $"Volume {index}";

            Disks = ParseDisks(diskPartDetailVolumeResults);

            ReadOnly = ParseYesNoAsBoolean(ParseProperty(ParseInfo["ReadOnly"], diskPartDetailVolumeResults));

            Hidden = ParseYesNoAsBoolean(ParseProperty(ParseInfo["Hidden"], diskPartDetailVolumeResults));

            NoDefaultDriveLetter = ParseYesNoAsBoolean(ParseProperty(ParseInfo["NoDefaultDriveLetter"], diskPartDetailVolumeResults));

            ShadowCopy = ParseYesNoAsBoolean(ParseProperty(ParseInfo["ShadowCopy"], diskPartDetailVolumeResults));

            BitLockerEncrypted = ParseYesNoAsBoolean(ParseProperty(ParseInfo["BitLockerEncrypted"], diskPartDetailVolumeResults));

            Installable = ParseYesNoAsBoolean(ParseProperty(ParseInfo["Installable"], diskPartDetailVolumeResults));

            VolumeCapacity = ParseProperty(ParseInfo["VolumeCapacity"], diskPartDetailVolumeResults);

            VolumeFreeSpace = ParseProperty(ParseInfo["VolumeFreeSpace"], diskPartDetailVolumeResults);
        }
    }
}
