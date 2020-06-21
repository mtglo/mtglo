using System;
using JetBrains.Annotations;

namespace Mtglo.Common.Identifiers
{
    /// <summary>Configuration options of MTGLO identifiers.</summary>
    [PublicAPI]
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

        /// <summary>Gets or sets asd.</summary>
        public int MuidVersion { get; set; }

        /// <summary>
        /// Gets or sets maximum amount of time (in milliseconds) generating a
        /// MUID is allowed to take before throwing a <seealso cref="TimeoutException" />.
        /// </summary>
        public int MuidTimeoutMilliseconds { get; set; }
    }
}
