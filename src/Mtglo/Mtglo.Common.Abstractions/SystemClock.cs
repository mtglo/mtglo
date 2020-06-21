using System;

namespace Mtglo.Common.Abstractions
{
    /// <inheritdoc cref="ISystemClock" />
    public class SystemClock : ISystemClock
    {
        /// <inheritdoc cref="ISystemClock.UnixTimeMilliseconds" />
        public long UnixTimeMilliseconds => DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}
