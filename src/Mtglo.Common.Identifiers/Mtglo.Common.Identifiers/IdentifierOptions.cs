using System;

namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// Configuration options of MTGLO identifiers.
    /// </summary>
    public class IdentifierOptions
    {
        /// <summary>
        /// A identifier assigned to this node. This should be assigned so that it is unique while it is being used,
        /// though re-use of a node id is acceptable.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Whether or not to allow multiple instances of the <see cref="MuidService"/>. This should generally
        /// only be true to facilitate unit testing as it is intended to be used as a singleton. 
        /// </summary>
        public bool AllowMultipleInstances { get; set; } = false;

    }
}