using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tyndall.DiskPart
{  
    public abstract class DiskPartObjectDetail
    {
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
        /// Parases the Display Name from the DiskPart DETAIL command results.
        /// </summary>
        /// <param name="diskPartDetailsResults">The DiskPart results (i.e., output) from a DETAIL command.</param>
        /// <param name="idIdentifier">The ID identifier for the object.</param>
        /// <param name="propertySeparator">The character that separates a property value from its identifier. The default value is a colon (':').</param>
        /// <returns>A trimmed string representing the value of the object's Display Name.</returns>
        /// <remarks>The Display Name is typically on the line preceding the ID.</remarks>
        protected string ParseDisplayName(string[] diskPartDetailsResults, string idIdentifier, char propertySeparator = PropertySeparator)
        {
            var result = string.Empty;

            for(int i = 0; i < diskPartDetailsResults.Length; i++)
            {
                var match = Regex.Match(diskPartDetailsResults[i], $@"{idIdentifier}\s*{propertySeparator}");

                if(match.Success)
                {
                    result = diskPartDetailsResults[i - 1].Trim();

                    break;
                }
            }

            return result;
        }
    }
}
