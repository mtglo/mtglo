using System;

namespace Mtglo.Common.Identifiers
{
    /// <inheritdoc cref="ISystemClock"/>
    public class SystemClock : ISystemClock
    {
        /// <inheritdoc cref="ISystemClock.UnixTimeMilliseconds"/>
        public long UnixTimeMilliseconds
        {
            get
            {
                return DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }
        }
    }
}