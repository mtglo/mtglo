using System;

namespace Mtglo.Common.Identifiers
{
    // TODO validate provided values

    /// <summary>Configuration options of MTGLO identifiers.</summary>
    public class IdentifierOptions
    {
        /// <summary>
        /// Gets or sets a identifier assigned to this node (these identifiers
        /// must be managed externally). This should be assigned so that it is
        /// unique while this node is active, though re-use of a particular node
        /// id is acceptable. See system documents for valid range of values (it
        /// may differ by <see cref="MuidVersion" />).
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Gets or sets the version ID of MUIDs to be generated.
        /// </summary>
        public int MuidVersion { get; set; }

        /// <summary>
        /// Gets or sets maximum amount of time (in milliseconds) generating a
        /// MUID is allowed to take before throwing a <seealso cref="TimeoutException" />.
        /// </summary>
        public int MuidTimeoutMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the total number of bits reserved for the MUID version
        /// ID of MUIDs to be generated.
        /// </summary>
        public int MuidVersionBits { get; set; }

        /// <summary>
        /// Gets or sets the total number of bits reserved for the timestamp of
        /// MUIDs to be generated.
        /// </summary>
        public int TimestampBits { get; set; }

        /// <summary>
        /// Gets or sets the total number of bits reserved for the Node ID of
        /// MUIDs to be generated.
        /// </summary>
        public int NodeIdBits { get; set; }

        /// <summary>
        /// Gets or sets the total number of bits reserved for the Sequence ID
        /// of MUIDs to be generated.
        /// </summary>
        public int SequenceIdBits { get; set; }

        /// <summary>
        /// Gets or sets an offset (in milliseconds) to the Unix Epoch to be
        /// used for MUIDs being generated.
        /// </summary>
        public long EpochOffset { get; set; }

        /// <summary>
        /// Creeates a deep copy of this object.
        /// </summary>
        /// <returns>A copy of this <see cref="IdentifierOptions" /> object.</returns>
        public IdentifierOptions Clone()
        {
            return (IdentifierOptions)MemberwiseClone();
        }
    }
}
