using System;

namespace Mtglo.Common.Identifiers
{
    /// <summary>MTGLO Universal Identifier.</summary>
    public class Muid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Muid" /> class by
        /// decoding the provided encoded value.
        /// </summary>
        /// <param name="encodedMuid">An encoded MUID.</param>
        /// <exception cref="FormatException">
        /// Thrown if the provided number does not
        /// conform to a known MUID format.
        /// </exception>
        internal Muid(long encodedMuid)
        {
            EncodedValue = encodedMuid;
            throw new NotImplementedException();
        }

        /// <summary>Gets or sets the version of the MUID.</summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the number of milliseconds since the Unix Epoch of the
        /// MUID.
        /// </summary>
        public long EpochTimeStamp { get; set; }

        /// <summary>Gets or sets the node identifier the MUID is associated with.</summary>
        public int NodeId { get; set; }

        /// <summary>Gets or sets the sequence identifier of the MUID.</summary>
        public int SequenceId { get; set; }

        /// <summary>
        /// Gets the timestamp for this identifier as a
        /// <seealso cref="DateTimeOffset" />, derived from <see cref="EpochTimeStamp" />.
        /// </summary>
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(EpochTimeStamp);

        /// <summary>Gets the encoded value of the MUID.</summary>
        public long EncodedValue { get; }
    }
}
