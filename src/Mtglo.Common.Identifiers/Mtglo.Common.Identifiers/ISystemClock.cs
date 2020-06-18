namespace Mtglo.Common.Identifiers
{
    /// <summary>
    /// /// Provides access to the normal system clock with precision in milliseconds to facilitate testing.
    /// </summary>
    public interface ISystemClock
    {
        /// <summary>
        /// Retrieves the current system time in UTC as the number of milliseconds since the Unix epoch.
        /// </summary>
        long UnixTimeMilliseconds { get; }
    }
}
