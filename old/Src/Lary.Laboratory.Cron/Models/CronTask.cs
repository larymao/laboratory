using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Cron.Models
{
    /// <summary>
    ///     Cron task information.
    /// </summary>
    public partial class CronTask
    {
        /// <summary>
        ///     Tries to execute current cron task. A return value indicates whether the execution succeeded or failed.
        /// </summary>
        /// <returns>
        ///     True if the execution succeeded or the task should not be executed; otherwise, false.
        /// </returns>
        public bool TryExecute()
        {
            try
            {
                if (this.ShouldExecuteRightNow())
                {
                    //task.Action?.Invoke();
                    Task.Run(this.Action);

                    this.LastExecution = DateTime.Now;
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        ///     Checks whether current task should be executed right now. The task won't be executed twice in a minute.
        /// </summary>
        /// <returns>
        ///     True if current task should be executed right now; otherwise, false.
        /// </returns>
        public bool ShouldExecuteRightNow()
        {
            var now = DateTime.Now;

            if ((now - this.LastExecution) > TimeSpan.FromMinutes(1))
            {
                if (this.CronInfo.DaysOfWeek != null && this.CronInfo.DaysOfWeek.Contains((ushort)now.DayOfWeek))
                {
                    if (this.CronInfo.Months != null && this.CronInfo.Months.Contains((ushort)now.Month))
                    {
                        if (this.CronInfo.DaysOfMonth != null && this.CronInfo.DaysOfMonth.Contains((ushort)now.Day))
                        {
                            if (this.CronInfo.Hours != null && this.CronInfo.Hours.Contains((ushort)now.Hour))
                            {
                                if (this.CronInfo.Minutes != null && this.CronInfo.Minutes.Contains((ushort)now.Minute))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
