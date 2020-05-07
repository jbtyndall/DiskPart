using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tyndall.DiskPart
{
    public abstract class DiskPartObjectDetail
    {
        /// <summary>
        /// The text to identify a Volume.
        /// </summary>
        private const string VolumeIdentifier = "Volume";

        /// <summary>
        /// The text to identify a Disk.
        /// </summary>
        private const string DiskIdentifier = "Disk";

        /// <summary>
        /// The character that separates a property value from its identifier. The default value is a colon (':').
        /// </summary>
        private const char PropertySeparator = ':';

        /// <summary>
        /// The Display Name of the object.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Parses DiskPart DETAIL command results for the specified property.
        /// </summary>
        /// <param name="propertyIdentifier">The Identifier of the property.</param>
        /// <param name="diskPartDetailResults">The DiskPart results (i.e., output) from a DETAIL command.</param>
        /// <param name="propertySeparator">The character that separates a property value from its identifier. The default value is a colon (':').</param>
        /// <param name="expectedSplitCount">The expected Split count when splitting a DiskPart Results line using the property separator.</param>
        /// <returns>A trimmed string representing the value of the specified property.</returns>
        protected string ParseProperty(string propertyIdentifier, string[] diskPartDetailResults, char propertySeparator = PropertySeparator, int expectedSplitCount = 2)
        {
            var result = string.Empty;

            foreach(var line in diskPartDetailResults)
            {
                var match = Regex.Match(line, $@"{propertyIdentifier}\s*{propertySeparator}");

                if(match.Success)
                {
                    var split = line.Split(propertySeparator);

                    if(split.Length == expectedSplitCount)
                    {
                        result = split[1].Trim();

                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Parses DiskPart DETAIL command results for the specified property as a Long.
        /// </summary>
        /// <param name="propertyIdentifier">The Identifier of the property.</param>
        /// <param name="diskPartDetailResults">The DiskPart results (i.e., output) from a DETAIL command.</param>
        /// <param name="propertySeparator">The character that separates a property value from its identifier. The default value is a colon (':').</param>
        /// <param name="expectedSplitCount">The expected Split count when splitting a DiskPart Results line using the property separator.</param>
        /// <returns>A Long value of the specified property.</returns>
        protected long ParsePropertyAsLong(string propertyIdentifier, string[] diskPartDetailResults, char propertySeparator = PropertySeparator, int expectedSplitCount = 2)
        {
            long result = -1;

            foreach (var line in diskPartDetailResults)
            {
                var match = Regex.Match(line, $@"{propertyIdentifier}\s*{propertySeparator}");

                if (match.Success)
                {
                    var split = line.Split(propertySeparator);

                    if (split.Length == expectedSplitCount)
                    {
                        if(long.TryParse(split[1].Trim(), out var tryParseResult))
                        {
                            result = tryParseResult;
                        }

                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Parases the Display Name from the DiskPart DETAIL command results.
        /// </summary>
        /// <param name="diskPartDetailsResults">The DiskPart results (i.e., output) from a DETAIL command.</param>
        /// <param name="firstPropertyIdentifier">The identifier of the object's first listed property.</param>
        /// <param name="propertySeparator">The character that separates a property value from its identifier. The default value is a colon (':').</param>
        /// <returns>A trimmed string representing the value of the object's Display Name.</returns>
        /// <remarks>The Display Name is typically on the line preceding the ID.</remarks>
        protected string ParseDisplayName(string[] diskPartDetailsResults, string firstPropertyIdentifier, char propertySeparator = PropertySeparator)
        {
            var result = string.Empty;

            for(int i = 0; i < diskPartDetailsResults.Length; i++)
            {
                var match = Regex.Match(diskPartDetailsResults[i], $@"{firstPropertyIdentifier}\s*{propertySeparator}");

                if(match.Success)
                {
                    result = diskPartDetailsResults[i - 1].Trim();

                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Utility function for parsing a Yes/No text as a Boolean.
        /// </summary>
        /// <param name="yesNo">Input "Yes" or "No" string.</param>
        /// <param name="trueText">The text to consider as "True".</param>
        /// <returns>True if the input text matches the "trueText", otherwise False.</returns>
        protected static bool ParseYesNoAsBoolean(string yesNo, string trueText = "yes")
        {
            return yesNo.ToLower().Equals(trueText);
        }

        /// <summary>
        /// Parses Volumes displayed in the DETAIL DISK results.
        /// </summary>
        /// <param name="diskPartDetailResults">The DiskPart results (i.e., output) from a DETAIL DISK command.</param>
        /// <param name="volumeIdentifier">The text to identify a Volume. The default value is "Volume".</param>
        /// <returns></returns>
        protected static List<Volume> ParseVolumes(string[] diskPartDetailResults, string volumeIdentifier = VolumeIdentifier)
        {
            var volumes = new List<Volume>();

            foreach (var diskPartDetailResultLine in diskPartDetailResults)
            {
                var volumeMatch = Regex.Match(diskPartDetailResultLine, $@"{volumeIdentifier} \d+");

                if (volumeMatch.Success)
                {
                    volumes.Add(new Volume(diskPartDetailResultLine));
                }
            }

            return volumes;
        }

        /// <summary>
        /// Parses Disks displayed in the DETAIL VOLUME results.
        /// </summary>
        /// <param name="diskPartDetailResults">The DiskPart results (i.e., output) from a DETAIL VOLUME command.</param>
        /// <param name="diskIdentifier">The text to identify a Disk. The default value is "Disk".</param>
        /// <returns></returns>
        protected static List<Disk> ParseDisks(string[] diskPartDetailResults, string diskIdentifier = DiskIdentifier)
        {
            var disks = new List<Disk>();

            foreach (var diskPartDetailResultLine in diskPartDetailResults)
            {
                var diskMatch = Regex.Match(diskPartDetailResultLine, $@"{diskIdentifier} \d+");

                if (diskMatch.Success)
                {
                    disks.Add(new Disk(diskPartDetailResultLine));
                }
            }

            return disks;
        }
    }
}
