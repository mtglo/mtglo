using JetBrains.Annotations;

namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// A collection of constant values used by MTGLO identifiers.
    /// </summary>
    internal static class IdentifierConstants
    {
        /// <summary>
        /// The total number of bits used to specify the node id.
        /// </summary>
        [UsedImplicitly]
        internal const int NodeIdBits = 12;

        /// <summary>
        /// The maximum node id, calculated by (2^ <see cref="NodeIdBits"/> - 1).
        /// </summary>
        internal const int MaxNodeId = 4095;

        /// <summary>
        /// The number of bits used to specify the sequence id.
        /// </summary>
        [UsedImplicitly]
        internal const int SequenceIdBits = 8;

        /// <summary>
        /// The maximum sequence id, calculated by (2^ <see cref="SequenceIdBits"/> - 1).
        /// </summary>
        internal const int MaxSequenceId = 255;
    }
}
