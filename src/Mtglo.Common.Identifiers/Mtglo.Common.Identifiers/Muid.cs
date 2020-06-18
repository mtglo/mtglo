using System;

namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// MTGLO Universal Identifier.
    /// </summary>
    public class Muid
    {
        /// <summary>
        /// What version Muid this is.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The number of milliseconds since the Unix Epoch.
        /// </summary>
        public long EpochTime { get; set; }

        /// <summary>
        /// The node identifier this id is associated with.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// The sequence identifier for this identifier.
        /// </summary>
        public int SequenceId { get; set; }
        
        /// <summary>
        /// The timestamp for this identifier as a DateTimeOffset, derived from <see cref="EpochTime"/>.
        /// </summary>
        public DateTimeOffset Timestamp
        {
            get
            {
                return _timestamp ?? (_timestamp = DateTimeOffset.FromUnixTimeMilliseconds(EpochTime)).Value;
            }
        }

        private DateTimeOffset? _timestamp;
    }
}
