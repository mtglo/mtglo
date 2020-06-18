using System;

namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// Factory that creates MUIDs.
    /// </summary>
    public interface IMuidFactory
    {
        /// <summary>
        /// Generates a serialized MUID, unique to this node and current timestamp. See <seealso cref="Muid"/>.
        /// </summary>
        /// <returns>A new MUID, serialized.</returns>
        long GenerateId();

        long GenerateId(long notBefore);

        long GenerateId(DateTimeOffset notBefore);

        long SerializeMuid(Muid id);

        long DeserializeMuid(long id);
    }
}