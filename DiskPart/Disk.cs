namespace Tyndall.DiskPart
{
    public class Disk : DiskPartObject
    {
        /// <summary>
        /// Parsing info for disk Status.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) StatusParseInfo = ("Status", 12, 13);

        /// <summary>
        /// Parsing info for disk Size.
        /// </summary>
        private readonly (string Identifer, int StartIndex, int Length) SizeParseInfo = ("Size", 27, 7);

        /// <summary>
        /// Parsing info for disk Free space left to create a partition.
        /// </summary>
        private readonly (string Identifer, int StartIndex, int Length) FreeParseInfo = ("Free", 36, 7);

        /// <summary>
        /// Parsing info for Dynamic disk designation.
        /// </summary>
        private readonly (string Identifer, int StartIndex, int Length) DynParseInfo = ("Dyn", 45, 3);

        /// <summary>
        /// Parsing info for GPT designation.
        /// </summary>
        private readonly (string Identifer, int StartIndex, int Length) GptParseInfo = ("Gpt", 50, 3);

        /// <summary>
        /// Parsing info for disk Index.
        /// </summary>
        protected override (string Identifier, int StartIndex, int Length) IndexParseInfo { get => ("Disk", 0, 10); }

        /// <summary>
        /// The Status of the Disk.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The Size of the Disk.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string Size { get; set; }

        /// <summary>
        /// The amount of free space left on the diisk to create a partition.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string Free { get; set; }

        /// <summary>
        /// Specifies whether or not the disk is dynamic.
        /// </summary>
        public bool Dyn { get; set; }

        /// <summary>
        /// Specifies whether or not the disk uses GUID Partition Table (GPT).
        /// </summary>
        public bool Gpt { get; set; }

        /// <summary>
        /// Instantiates a new <c>Disk</c> from the specified DiskPart Results Disk line.
        /// </summary>
        /// <param name="diskPartResultsDiskLine">A line from DiskPart results (output) that starts with "Disk".</param>
        public Disk(string diskPartResultsDiskLine)
        {
            Index = ParsePropertyAsInt(diskPartResultsDiskLine, IndexParseInfo.StartIndex, IndexParseInfo.Length, IndexParseInfo.Identifier);

            Status = ParseProperty(diskPartResultsDiskLine, StatusParseInfo.StartIndex, StatusParseInfo.Length);

            Size = ParseProperty(diskPartResultsDiskLine, SizeParseInfo.StartIndex, SizeParseInfo.Length);

            Free = ParseProperty(diskPartResultsDiskLine, FreeParseInfo.StartIndex, FreeParseInfo.Length);

            Dyn = ParsePropertyAsBool(diskPartResultsDiskLine, DynParseInfo.StartIndex, DynParseInfo.Length);

            Gpt = ParsePropertyAsBool(diskPartResultsDiskLine, GptParseInfo.StartIndex, GptParseInfo.Length);
        }
    }
}
