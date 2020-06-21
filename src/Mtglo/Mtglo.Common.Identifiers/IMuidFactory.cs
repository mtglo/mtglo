using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// Factory that generates and decodes MUIDs. Generated MUIDs are
    /// guaranteed to be unique for a given node, as configured in
    /// <seealso cref="IdentifierOptions" />.
    /// </summary>
    public interface IMuidFactory
    {
        /// <summary>
        /// Generates an encoded <seealso cref="Muid" />, unique to the current
        /// timestamp and the current node, see <seealso cref="IdentifierOptions" />. This
        /// operation may infrequently need to wait a short time to ensure uniqueness
        /// (typically 1 ms).
        /// </summary>
        /// <param name="cancellationToken">Token that can be used to cancel the operation.</param>
        /// <returns>A new MUID, encoded.</returns>
        /// <exception cref="InvalidOperationException">
        /// Throws when the current system
        /// timestamp cannot be encoded into the MUID. This may happen if the timestamp is
        /// outside the valid range for the MUID version.
        /// </exception>
        /// <exception cref="TimeoutException">
        /// Thrown when the operation attempts to wait
        /// longer than the configured MuidTimeout in <seealso cref="IdentifierOptions" />.
        /// </exception>
        Task<long> GenerateMuidAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Generates an encoded <seealso cref="Muid" />, unique to this node and
        /// the current timestamp. The new MUID will have timestamp after the provided MUID
        /// (typically 1ms later). This may be useful to guarantee proper sorting of MUIDs
        /// in the case that they are related (e.g. by saga or entity), but variance in
        /// system clocks from different nodes may cause them to appear in the wrong order
        /// when lexicographically sorted. Essentially this allows the calling code to wait
        /// (blocking further execution) until the MUIDs will sort in the order they were
        /// created.
        /// </summary>
        /// <param name="afterMuid">
        /// A MUID whose timestamp should occur before the new MUID
        /// being generated.
        /// </param>
        /// <param name="cancellationToken">Token that can be used to cancel the operation.</param>
        /// <returns>A new (encoded) MUID whose timestamp is after the one provided.</returns>
        /// <exception cref="InvalidOperationException">
        /// Throws when the current system
        /// timestamp cannot be encoded into the MUID. This may happen if the timestamp is
        /// outside the valid range for the MUID version.
        /// </exception>
        /// <exception cref="TimeoutException">
        /// Thrown when the operation attempts to wait
        /// longer than the configured MuidTimeout in <seealso cref="IdentifierOptions" />.
        /// </exception>
        Task<long> GenerateMuidAsync(long afterMuid, CancellationToken cancellationToken);

        /// <inheritdoc cref="GenerateMuidAsync(long, CancellationToken)" />
        Task<long> GenerateMuidAsync(Muid afterMuid, CancellationToken cancellationToken);

        /// <summary>
        /// Generates an encoded <seealso cref="Muid" />, unique to this node and
        /// the current timestamp. The new MUID will have timestamp after the provided
        /// timestamp (typically 1ms later). This allows the calling code to wait (blocking
        /// further execution) until the generated MUID has a timestamp later than the
        /// provided one, which may be useful to control MUID order, when sorted
        /// lexicographically.
        /// </summary>
        /// <param name="afterTimestamp">
        /// A timestamp that should occur before the new MUID
        /// being generated.
        /// </param>
        /// <param name="cancellationToken">Token that can be used to cancel the operation.</param>
        /// <inheritdoc cref="GenerateMuidAsync(long, CancellationToken)" />
        Task<long> GenerateMuidAsync(DateTimeOffset afterTimestamp, CancellationToken cancellationToken);

        /// <summary>Decodes a provided long to the object representation of a MUID.</summary>
        /// <param name="encodedMuid">An encoded MUID to be decoded.</param>
        /// <returns>The decoded MUID.</returns>
        /// <exception cref="FormatException">
        /// Thrown if the provided number does not
        /// conform to a known MUID format.
        /// </exception>
        Muid DecodeMuid(long encodedMuid);
    }
}
