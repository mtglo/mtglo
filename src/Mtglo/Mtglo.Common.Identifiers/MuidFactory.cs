using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mtglo.Common.Identifiers
{
    /// <inheritdoc />
    public class MuidFactory : IMuidFactory
    {
        /// <inheritdoc />
        public async Task<long> GenerateMuidAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<long> GenerateMuidAsync(long afterMuid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<long> GenerateMuidAsync(Muid afterMuid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<long> GenerateMuidAsync(DateTimeOffset afterTimestamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Muid DecodeMuid(long encodedMuid)
        {
            throw new NotImplementedException();
        }
    }
}
