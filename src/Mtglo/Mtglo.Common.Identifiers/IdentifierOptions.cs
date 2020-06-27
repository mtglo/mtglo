using System;

namespace Mtglo.Common.Identifiers
{
    // TODO validate provided values

    /// <summary>Configuration options of MTGLO identifiers.</summary>
    public class IdentifierOptions
    {
        /// <summary>
        /// Gets or sets a identifier assigned to this node (these identifiers
        /// must be managed externally). This should be assigned so that it is unique while
        /// this node is active, though re-use of a particular node id is acceptable. See
        /// system documents for valid range of values (it may differ by
        /// <see cref="MuidVersion" />).
        /// </summary>
        public int NodeId { get; set; }

        public int MuidVersion { get; set; }

        /// <summary>
        /// Gets or sets maximum amount of time (in milliseconds) generating a
        /// MUID is allowed to take before throwing a <seealso cref="TimeoutException" />.
        /// </summary>
        public int MuidTimeoutMilliseconds { get; set; }

        public int MuidVersionBits { get; set; }

        public int TimestampBits { get; set; }

        public int NodeIdBits { get; set; }

        public int SequenceIdBits { get; set; }

        public long EpochOffset { get; set; }

        public IdentifierOptions Clone()
        {
            return (IdentifierOptions)MemberwiseClone();
        }
    }
}
