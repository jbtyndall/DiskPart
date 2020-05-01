using System;
using System.Collections.Generic;
using System.Text;

namespace Tyndall.DiskPart
{
    public class Partition : DiskPartObject
    {
        /// <summary>
        /// Parsing info for partition Type.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) TypeParseInfo = ("Type", 17, 16);

        /// <summary>
        /// Parsing info for partition Size.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) SizeParseInfo = ("Size", 35, 7);

        /// <summary>
        /// Parsing info for partition Offset.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) OffsetParseInfo = ("Offset", 44, 7);

        /// <summary>
        /// Parsing info for partition Index.
        /// </summary>
        protected override (string Identifier, int StartIndex, int Length) IndexParseInfo { get => ("Partition", 0, 15); }

        /// <summary>
        /// The Type of the partition.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Size of the partition.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string Size { get; set; }

        /// <summary>
        /// The Offset of the partition.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string Offset { get; set; }

        /// <summary>
        /// Instantiates a new <c>Partition</c> from the specified DiskPart results Partition line.
        /// </summary>
        /// <param name="diskPartResultsPartitionLine">A line from DiskPart results (output) that starts with "Partition".</param>
        public Partition(string diskPartResultsPartitionLine)
        {
            Index = ParsePropertyAsInt(diskPartResultsPartitionLine, IndexParseInfo.StartIndex, IndexParseInfo.Length, IndexParseInfo.Identifier);

            Type = ParseProperty(diskPartResultsPartitionLine, TypeParseInfo.StartIndex, TypeParseInfo.Length);

            Size = ParseProperty(diskPartResultsPartitionLine, SizeParseInfo.StartIndex, SizeParseInfo.Length);

            Offset = ParseProperty(diskPartResultsPartitionLine, OffsetParseInfo.StartIndex, OffsetParseInfo.Length);
        }
    }
}
