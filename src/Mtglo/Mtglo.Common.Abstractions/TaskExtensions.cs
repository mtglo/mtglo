using System;
using System.Threading.Tasks;

namespace Mtglo.Common.Abstractions
{
    /// <summary>
    /// Extensions that assist managing <seealso cref="Task" />.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Causes the <seealso cref="Environment" /> to fail (crash) quickly
        /// when the task completes with an exception.
        /// </summary>
        /// <param name="task">The task to fail fast on.</param>
        /// <returns>A <see cref="Task"/> representing the result of the
        /// asynchronous operation.</returns>
        public static Task? FailFastOnExceptions(this Task task)
        {
            return task?.ContinueWith(t => Environment.FailFast("Task faulted", t.Exception),
                TaskContinuationOptions.OnlyOnFaulted |
                TaskContinuationOptions.ExecuteSynchronously |
                TaskContinuationOptions.DenyChildAttach);
        }

        /// <summary>
        /// Causes the <seealso cref="Environment" /> to ignore if/when the task
        /// completes with an exception.
        /// </summary>
        /// <param name="task">The task to ignore unhandled exceptions in.</param>
        /// <returns>A <see cref="Task"/> representing the result of the
        /// asynchronous operation.</returns>
        public static Task IgnoreExceptions(this Task task)
        {
            return task.ContinueWith(Ït => { var ignored = t.Exception; },
                TaskContinuationOptions.OnlyOnFaulted |
                TaskContinuationOptions.ExecuteSynchronously |
                TaskContinuationOptions.DenyChildAttach);
        }
    }
}
