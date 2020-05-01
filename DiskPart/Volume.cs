namespace Tyndall.DiskPart
{
    public class Volume : DiskPartObject
    {
        /// <summary>
        /// Parsing info for volume Letter.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) LtrParseInfo = ("Ltr", 14, 3);

        /// <summary>
        /// Parsing info for volume Label.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) LabelParseInfo = ("Label", 19, 11);

        /// <summary>
        /// Parsing info for volume Filesystem.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) FsParseInfo = ("Fs", 32, 5);

        /// <summary>
        /// Parsing info for volume Type.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) TypeParseInfo = ("Type", 39, 10);

        /// <summary>
        /// Parsing info for volume Size.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) SizeParseInfo = ("Size", 51, 7);

        /// <summary>
        /// Parsing info for volume Status.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) StatusParseInfo = ("Status", 60, 9);

        /// <summary>
        /// Parsing info for volume Info.
        /// </summary>
        private readonly (string Identifier, int StartIndex, int Length) InfoParseInfo = ("Info", 71, 8);

        /// <summary>
        /// Parsing info for volume Index.
        /// </summary>
        protected override (string Identifier, int StartIndex, int Length) IndexParseInfo { get => ("Volume", 0, 12); }

        /// <summary>
        /// The Letter of the volume.
        /// </summary>
        /// <remarks>If the Volume does not have a Letter, it is denoted as an empty string.</remarks>
        public string Ltr { get; set; }

        /// <summary>
        /// The Label of the volume.
        /// </summary>
        /// <remarks>If the Volume does not have a Label, it is denoted as an empty string.</remarks>
        public string Label { get; set; }

        /// <summary>
        /// The FileSystem of the volume.
        /// </summary>
        public string Fs { get; set; }

        /// <summary>
        /// The Type of the volume.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Size of the volume.
        /// </summary>
        /// <remarks>This value matches what DiskPart reports (i.e., a size label), rather than the actual size (e.g., in bytes).</remarks>
        public string Size { get; set; }

        /// <summary>
        /// The Status of the volume.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The Info of the volume.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Instaniates a new <c>Volume</c> from the specified DiskPartResults Volume Line.
        /// </summary>
        /// <param name="diskPartResultsVolumeLine">A line from DiskPart results (output) that starts with "Volume".</param>
        public Volume(string diskPartResultsVolumeLine)
        {
            Index = ParsePropertyAsInt(diskPartResultsVolumeLine, IndexParseInfo.StartIndex, IndexParseInfo.Length, IndexParseInfo.Identifier);

            Ltr = ParseProperty(diskPartResultsVolumeLine, LtrParseInfo.StartIndex, LtrParseInfo.Length);

            Label = ParseProperty(diskPartResultsVolumeLine, LabelParseInfo.StartIndex, LabelParseInfo.Length);

            Fs = ParseProperty(diskPartResultsVolumeLine, FsParseInfo.StartIndex, FsParseInfo.Length);

            Type = ParseProperty(diskPartResultsVolumeLine, TypeParseInfo.StartIndex, TypeParseInfo.Length);

            Size = ParseProperty(diskPartResultsVolumeLine, SizeParseInfo.StartIndex, SizeParseInfo.Length);

            Status = ParseProperty(diskPartResultsVolumeLine, StatusParseInfo.StartIndex, StatusParseInfo.Length);

            Info = ParseProperty(diskPartResultsVolumeLine, InfoParseInfo.StartIndex, InfoParseInfo.Length);
        }
    }
}
