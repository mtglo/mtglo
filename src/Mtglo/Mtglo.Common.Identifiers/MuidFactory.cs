using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mtglo.Common.Abstractions;

namespace Mtglo.Common.Identifiers
{
    /// <inheritdoc />
    public class MuidFactory : IMuidFactory, IDisposable
    {
        private readonly ISystemClock _clock;
        private readonly ILogger<MuidFactory> _logger;
        private readonly IdentifierOptions _options;

        private readonly BlockingCollection<int> _sequenceIds;
        private readonly SemaphoreSlim _syncRoot;

        /// <summary>Initializes a new instance of the <see cref="MuidFactory" /> class.</summary>
        /// <param name="options">Used to configure this factory.</param>
        /// <param name="clock">Used to retrieve the current system timestamp.</param>
        /// <param name="logger">Used for logging.</param>
        public MuidFactory(
            IOptions<IdentifierOptions> options,
            ISystemClock clock,
            ILogger<MuidFactory> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _sequenceIds = new BlockingCollection<int>();
            _syncRoot = new SemaphoreSlim(1, 1);
            _ = GenerateSequenceIds(0, CancellationToken.None).FailFastOnExceptions();
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAsync(CancellationToken cancellationToken)
        {
            if (_sequenceIds.Count == 0)
            {
                _ = GenerateSequenceIds(_clock.UnixTimeMilliseconds, CancellationToken.None).FailFastOnExceptions();
            }

            var options = _options.Clone();

            var firstWaitTime = options.MuidTimeoutMilliseconds / 2;
            var secondWaitTime = options.MuidTimeoutMilliseconds - firstWaitTime;

            cancellationToken.ThrowIfCancellationRequested();
            if (!_sequenceIds.TryTake(out var sequenceId, firstWaitTime, cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (secondWaitTime > 0)
                {
                    _ = GenerateSequenceIds(_clock.UnixTimeMilliseconds, CancellationToken.None).FailFastOnExceptions();
                }

                if (secondWaitTime == 0 || !_sequenceIds.TryTake(out sequenceId, options.MuidTimeoutMilliseconds / 2, cancellationToken))
                {
                    throw new TimeoutException();
                }
            }

            var now = _clock.UnixTimeMilliseconds;

            long id = options.MuidVersion << (options.TimestampBits + options.NodeIdBits + options.SequenceIdBits);
            id |= now << (options.NodeIdBits + options.SequenceIdBits);
            id |= (uint)_options.NodeId << options.SequenceIdBits;
            id |= (uint)sequenceId;

            return Task.FromResult(id);
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAsync(long afterMuid, CancellationToken cancellationToken)
        {
            var muid = new Muid(afterMuid);
            return GenerateMuidAsync(muid, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAsync(Muid afterMuid, CancellationToken cancellationToken)
        {
            var afterTimestamp = afterMuid?.EpochTimeStamp ?? throw new ArgumentNullException(nameof(afterMuid));
            return GenerateMuidAfterTimestampAsync(afterTimestamp, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAsync(DateTimeOffset afterTimestamp, CancellationToken cancellationToken)
        {
            return GenerateMuidAfterTimestampAsync(afterTimestamp.ToUnixTimeMilliseconds(), cancellationToken);
        }

        private async Task<long> GenerateMuidAfterTimestampAsync(long afterTimestamp, CancellationToken cancellationToken)
        {
            while (_clock.UnixTimeMilliseconds <= afterTimestamp)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var delay = afterTimestamp - _clock.UnixTimeMilliseconds;
                await Task.Delay((int)(delay <= int.MaxValue ? delay : int.MaxValue) + 1).ConfigureAwait(false);
            }

            return await GenerateMuidAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Muid DecodeMuid(long encodedMuid)
        {
            return new Muid(encodedMuid);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="Dispose()" />
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _sequenceIds.Dispose();
            _syncRoot.Dispose();
        }

        private async Task GenerateSequenceIds(long afterTimestamp, CancellationToken cancellationToken)
        {
            await _syncRoot.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (_sequenceIds.Any())
                {
                    return;
                }

                var options = _options.Clone();

                while (_clock.UnixTimeMilliseconds <= afterTimestamp)
                {
                    await Task.Delay(1).ConfigureAwait(false);
                }

                for (var i = 0; i < (2 ^ options.SequenceIdBits); i++)
                {
                    _sequenceIds.Add(i, cancellationToken);
                }
            }
            finally
            {
                _syncRoot.Release();
            }
        }

        private async Task WaitUntilAfter(long afterTimestamp, CancellationToken cancellationToken)
        {
            while (_clock.UnixTimeMilliseconds <= afterTimestamp)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var delay = afterTimestamp - _clock.UnixTimeMilliseconds;
                await Task.Delay((int)(delay <= int.MaxValue ? delay : int.MaxValue)).ConfigureAwait(false);
            }
        }
    }
}
