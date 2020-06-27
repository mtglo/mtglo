using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mtglo.Common.Abstractions;
using static System.Threading.Tasks.TaskContinuationOptions;

namespace Mtglo.Common.Identifiers
{
    /// <inheritdoc />
    public class MuidFactory : IMuidFactory, IDisposable
    {
        private readonly ISystemClock _clock;
        private readonly ILogger<MuidFactory> _logger;
        private readonly IdentifierOptions _options;

        private readonly ConcurrentQueue<int> _sequenceIds;
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
            _options = options?.Value.Clone() ?? throw new ArgumentNullException(nameof(options));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _sequenceIds = new ConcurrentQueue<int>();
            _syncRoot = new SemaphoreSlim(1, 1);
        }

        /// <inheritdoc />
        public async ValueTask<long> GenerateMuidAsync(CancellationToken cancellationToken)
        {
            var start = _clock.UnixTimeMilliseconds;
            int sequenceId;

            while (!_sequenceIds.TryDequeue(out sequenceId))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_clock.UnixTimeMilliseconds > start + _options.MuidTimeoutMilliseconds)
                {
                    throw new TimeoutException($"Could not get a sequence id within {_options.MuidTimeoutMilliseconds}ms");
                }

                _ = GenerateSequenceIds(_clock.UnixTimeMilliseconds, CancellationToken.None).ContinueWith(
                    t => Environment.FailFast("Task faulted", t.Exception),
                    CancellationToken.None,
                    OnlyOnFaulted | ExecuteSynchronously | DenyChildAttach,
                    TaskScheduler.Current);

                await Task.Delay(1, cancellationToken).ConfigureAwait(false);
            }

            var timestamp = _clock.UnixTimeMilliseconds - _options.EpochOffset;

            long id = _options.MuidVersion << (_options.TimestampBits + _options.NodeIdBits + _options.SequenceIdBits);
            id |= timestamp << (_options.NodeIdBits + _options.SequenceIdBits);
            id |= (uint)_options.NodeId << _options.SequenceIdBits;
            id |= (uint)sequenceId;

            return id;
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAsync(long afterMuid, CancellationToken cancellationToken)
        {
            var muid = DecodeMuid(afterMuid);
            return GenerateMuidAsync(muid, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAsync(Muid afterMuid, CancellationToken cancellationToken)
        {
            var afterTimestamp = afterMuid?.EpochTimeStamp + _options.EpochOffset ?? throw new ArgumentNullException(nameof(afterMuid));
            return GenerateMuidAfterTimestampAsync(afterTimestamp, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> GenerateMuidAfterTimestampAsync(DateTimeOffset afterTimestamp, CancellationToken cancellationToken)
        {
            return GenerateMuidAfterTimestampAsync(afterTimestamp.ToUnixTimeMilliseconds(), cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> GenerateMuidAfterTimestampAsync(long afterTimestamp, CancellationToken cancellationToken)
        {
            while (_clock.UnixTimeMilliseconds <= afterTimestamp)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var delay = afterTimestamp - _clock.UnixTimeMilliseconds;
                await Task.Delay((int)(delay <= int.MaxValue ? delay + 1 : int.MaxValue)).ConfigureAwait(false);
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

            _syncRoot.Dispose();
        }

        private async Task GenerateSequenceIds(long afterTimestamp, CancellationToken cancellationToken)
        {
            await _syncRoot.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (!_sequenceIds.IsEmpty)
                {
                    return;
                }

                while (_clock.UnixTimeMilliseconds <= afterTimestamp)
                {
                    await Task.Delay(1, cancellationToken).ConfigureAwait(false);
                }

                for (var i = 0; i < (2 ^ _options.SequenceIdBits); i++)
                {
                    _sequenceIds.Enqueue(i);
                }
            }
            finally
            {
                _ = _syncRoot.Release();
            }
        }
    }
}
