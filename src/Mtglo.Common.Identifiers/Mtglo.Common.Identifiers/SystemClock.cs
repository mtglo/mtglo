using System;
using JetBrains.Annotations;

namespace Mtglo.Common.Identifiers
{
    /// <inheritdoc cref="ISystemClock"/>
    [PublicAPI]
    public class SystemClock : ISystemClock
    {
        /// <inheritdoc cref="ISystemClock.UnixTimeMilliseconds"/>
        public long UnixTimeMilliseconds => DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}
