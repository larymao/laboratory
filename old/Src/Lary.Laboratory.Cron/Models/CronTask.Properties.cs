using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Cron.Models
{
    /// <summary>
    ///     Cron task information.
    /// </summary>
    public partial class CronTask
    {
        /// <summary>
        ///     The action of current task.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        ///     The cron infomation of current task.
        /// </summary>
        public CronInfo CronInfo { get; set; }

        /// <summary>
        ///     The identity code of current task, guid string without '-' is recommended.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Indicates whether the task is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        ///     Indicates datetime of the last execution.
        /// </summary>
        public DateTime LastExecution { get; set; }
    }
}
