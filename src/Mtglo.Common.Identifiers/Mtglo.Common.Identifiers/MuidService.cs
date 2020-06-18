using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// Creates and decodes MTGLO identifiers.
    /// </summary>
    public class MuidService : IMuidFactory
    {
        private readonly ISystemClock _clock;
        private readonly IdentifierOptions _options;
        private readonly ILogger<MuidService> _logger;
        private readonly int _nodeId;
        
        private static int _timesInstantiated;

        private long _lastTime;
        private int _sequence;
        private object _syncLock;
        
        /// <summary>
        /// Constructs an IdentifierService.
        /// </summary>
        /// <param name="clock">Clock used to read the current time.</param>
        /// <param name="options">Options used by this object.</param>
        /// <param name="logger">Logger used by this object.</param>
        public MuidService(ISystemClock clock, IOptions<IdentifierOptions> options, ILogger<MuidService> logger)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            _lastTime = 0;
            _sequence = 0;
            _syncLock = new object();
            
            _nodeId = (_options.NodeId >= 0 && _options.NodeId <= IdentifierConstants.MaxNodeId)
                ? _options.NodeId
                : throw new ArgumentOutOfRangeException(nameof(_options.NodeId), $"The provided NodeId must be between 0 and {IdentifierConstants.MaxNodeId}");
            _timesInstantiated++;
            if (!_options.AllowMultipleInstances && _timesInstantiated > 1)
            {
                throw new InvalidOperationException("Attempted to instantiate IdentifierService more than once");
            }
        }

        /// <summary>
        /// Generates a serialized MUID, unique to this node and current timestamp. See <seealso cref="Muid"/>.
        /// </summary>
        /// <returns>A new MUID, serialized.</returns>
        public long GenerateId()
        {
            lock (_syncLock)
            {
                var currentTime = _clock.UnixTimeMilliseconds;
                if (currentTime == _lastTime)
                {
                    _sequence++;
                    if (_sequence > IdentifierConstants.MaxSequenceId)
                    {
                        Thread.Sleep(1);
                        currentTime = _clock.UnixTimeMilliseconds;
                        
                    }
                }
            }

            return 0L;
        }
        
        
    }
}