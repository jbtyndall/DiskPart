using System.Collections.Generic;

namespace Tyndall.DiskPart
{
    public class PartitionDetail : DiskPartObjectDetail
    {
        /// <summary>
        /// A dictionary of parsing info for <c>PartitionDetail</c> properties.
        /// </summary>
        private readonly Dictionary<string, string> ParseInfo = new Dictionary<string, string>
        {
            ["Type"] = "Type",
            ["Hidden"] = "Hidden",
            ["Required"] = "Required",
            ["Attrib"] = "Attrib",
            ["OffsetInBytes"] = "Offset in Bytes"
        };

        /// <summary>
        /// The Type of the partition.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Specifies whether or not the partition is hidden.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Specifies whether or not the partition is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// The Attribute of the partition.
        /// </summary>
        public string Attrib { get; set; }

        /// <summary>
        /// The Offset of the partition... in bytes.
        /// </summary>
        public long OffsetInBytes { get; set; }

        /// <summary>
        /// A list of volumes on the partition.
        /// </summary>
        public List<Volume> Volumes { get; set; }

        /// <summary>
        /// Instantiates a new <c>PartitionDetail</c> from the specified DiskPart results.
        /// </summary>
        /// <param name="diskPartDetailPartitionResults">The DiskPart results (i.e., output) from a DETAIL PARTITION command.</param>
        public PartitionDetail(string[] diskPartDetailPartitionResults)
        {
            DisplayName = ParseDisplayName(diskPartDetailPartitionResults, ParseInfo["Type"]);

            Type = ParseProperty(ParseInfo["Type"], diskPartDetailPartitionResults);

            Hidden = ParseYesNoAsBoolean(ParseProperty(ParseInfo["Hidden"], diskPartDetailPartitionResults));

            Required = ParseYesNoAsBoolean(ParseProperty(ParseInfo["Required"], diskPartDetailPartitionResults));

            Attrib = ParseProperty(ParseInfo["Attrib"], diskPartDetailPartitionResults);

            OffsetInBytes = ParsePropertyAsLong(ParseInfo["OffsetInBytes"], diskPartDetailPartitionResults);

            Volumes = ParseVolumes(diskPartDetailPartitionResults);
        }
    }
}
