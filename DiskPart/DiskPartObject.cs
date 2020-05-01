namespace Tyndall.DiskPart
{
    /// <summary>
    /// An abstract class for properties and utilities common to all DiskPart objects.
    /// </summary>
    public abstract class DiskPartObject
    {
        /// <summary>
        /// Parsing information for object Index.
        /// </summary>
        protected abstract (string Identifier, int StartIndex, int Length) IndexParseInfo { get; }

        /// <summary>
        /// The Index of the object.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Denotes whether or not the <c>DiskPartObject</c> is active (has focus).
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Parses a DiskPart results line for the specified String property.
        /// </summary>
        /// <param name="diskPartResultsObjectLine">A line from DiskPart results (i.e., output) that starts with the Object identifier.</param>
        /// <param name="parseStartIndex">The StartIndex where the property begins.</param>
        /// <param name="parseLength">The length of the property.</param>
        /// <returns>A trimmed string representing the property.</returns>
        protected string ParseProperty(string diskPartResultsObjectLine, int parseStartIndex, int parseLength)
        {
            var result = string.Empty;

            var adjustedParseLength = parseLength;

            if (diskPartResultsObjectLine.Length <= parseStartIndex) return result;

            if(diskPartResultsObjectLine.Length < parseStartIndex + parseLength)
            {
                adjustedParseLength = diskPartResultsObjectLine.Length - parseStartIndex;
            }

            result = diskPartResultsObjectLine.Substring(parseStartIndex, adjustedParseLength).Trim();

            return result;
        }

        /// <summary>
        /// Parses a DiskPart results line for the specified Integer property.
        /// </summary>
        /// <param name="diskPartResultsObjectLine">A line from DiskPart results (i.e., output) that starts with the Object identifier.</param>
        /// <param name="parseStartIndex">The StartIndex where the property begins.</param>
        /// <param name="parseLength">The length of the property.</param>
        /// <param name="removeText">Optional text to remove from the property when parsing as an Integer.</param>
        /// <returns>A Integer representation of the propery.</returns>
        protected int ParsePropertyAsInt(string diskPartResultsObjectLine, int parseStartIndex, int parseLength, string removeText = "")
        {
            var result = -1;

            var stringResult = ParseProperty(diskPartResultsObjectLine, parseStartIndex, parseLength);

            if(int.TryParse(stringResult.Replace(removeText, "").Trim(), out var tryParseResult))
            {
                result = tryParseResult;
            }

            return result;
        }

        /// <summary>
        /// Parses a DiskPart results line for the specified Boolean property.
        /// </summary>
        /// <param name="diskPartResultsObjectLine">A line from DiskPart results (i.e., output) that starts with the Object identifier.</param>
        /// <param name="parseStartIndex">The StartIndex where the property begins.</param>
        /// <param name="parseLength">The length of the property.</param>
        /// <param name="parseAsTrue">The string that denotes "True".</param>
        /// <returns>A Boolean representation of the property.</returns>
        protected bool ParsePropertyAsBool(string diskPartResultsObjectLine, int parseStartIndex, int parseLength, string parseAsTrue = "*")
        {
            return ParseProperty(diskPartResultsObjectLine, parseStartIndex, parseLength).Equals(parseAsTrue);
        }
    }
}