using Lary.Laboratory.Cron.Exceptions;
using Lary.Laboratory.Cron.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lary.Laboratory.Cron
{
    /// <summary>
    ///     The time-based job scheduler. <see cref="CronScheduler"/> enables users to schedule <see cref="CronTask"/> 
    ///     to run periodically at certain times, dates or intervals.
    /// </summary>
    public class CronScheduler : IDisposable
    {
        private static CronScheduler _instance;

        /// <summary>
        ///     The singleton of <see cref="CronScheduler"/>.
        /// </summary>
        public static CronScheduler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new CronScheduler();
                        }
                    }
                }

                return _instance;
            }
        }

        private bool _running;

        /// <summary>
        ///     Indicates whether current scheduler is running.
        /// </summary>
        public bool Running => _running;


        private static readonly object _instanceLocker = new object();
        private static readonly object _tasksLocker = new object();
        private static readonly TimeSpan _interval = TimeSpan.FromSeconds(60); // The interval of the _timer.
        private List<CronTask> _scheduledTasks;
        private Timer _timer;


        /// <summary>
        ///     Initializes a new instance of the <see cref="CronScheduler"/> class.
        /// </summary>
        private CronScheduler()
        {
        }


        /// <summary>
        ///     Add a cron task to current scheduler with cron expression and the action.
        /// </summary>
        /// <param name="cronExpr">
        ///     A string that represents the cron expression.
        /// </param>
        /// <param name="action">
        ///     An action to run when the scheduled task is triggered.
        /// </param>
        /// <param name="taskId">
        ///     Task's id, which helps you to get a better control of the task. If it's null or empty, a random guid 
        ///     will be generated.
        /// </param>
        /// <returns>
        ///     True if the task is scheduled successfully; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if action is null.
        /// </exception>
        /// <exception cref="CronParserException">
        ///     Throw if failed to parse cronExpr.
        /// </exception>
        public bool AddTask(string cronExpr, Action action, string taskId = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var cron = CronInterpreter.Parse(cronExpr);
            var task = new CronTask
            {
                Id = String.IsNullOrEmpty(taskId) ? Guid.NewGuid().ToString("N") : taskId,
                CronInfo = cron,
                Action = action,
                IsEnabled = true
            };

            return AddTask(task);
        }

        /// <summary>
        ///     Add a cron task to current scheduler with cron information and the action.
        /// </summary>
        /// <param name="cronInfo">
        ///     An object of <see cref="Cron"/>.
        /// </param>
        /// <param name="action">
        ///     An action to run when the scheduled task is triggered.
        /// </param>
        /// <param name="taskId">
        ///     Task's id, which helps you to get a better control of the task. If it's null or empty, a random guid 
        ///     will be generated.
        /// </param>
        /// <returns>
        ///     True if the task is scheduled successfully; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if cronInfo or action is null.
        /// </exception>
        public bool AddTask(CronInfo cronInfo, Action action, string taskId = null)
        {
            if (cronInfo == null)
            {
                throw new ArgumentNullException(nameof(cronInfo));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var task = new CronTask
            {
                Id = String.IsNullOrEmpty(taskId) ? Guid.NewGuid().ToString("N") : taskId,
                CronInfo = cronInfo,
                Action = action,
                IsEnabled = true
            };

            return AddTask(task);
        }

        /// <summary>
        ///     Add a cron task to current scheduler.
        /// </summary>
        /// <param name="task">
        ///     The cron task to schedule.
        /// </param>
        /// <returns>
        ///     True if the task is scheduled successfully; otherwise, false. If the <see cref="CronTask.IsEnabled"/> 
        ///     of task is set to false, this operation will fail.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the task is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Throw if the Id, CronInfo or Action of the task is null.
        /// </exception>
        public bool AddTask(CronTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (String.IsNullOrEmpty(task.Id) || task.CronInfo == null || task.Action == null)
            {
                throw new ArgumentException(nameof(task));
            }

            if (task.IsEnabled)
            {
                lock (_tasksLocker)
                {
                    this._scheduledTasks.Add(task);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Releases all resources used by the current instance of <see cref="CronScheduler"/>.
        /// </summary>
        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }

            _scheduledTasks = null;

            _running = false;
        }

        /// <summary>
        ///     Removes all the tasks of which Id equals to taskId.
        /// </summary>
        /// <param name="taskId">
        ///     Defines the tasks to remove.
        /// </param>
        /// <returns>
        ///       The number of tasks removed from the scheduler.
        /// </returns>
        public int Remove(string taskId)
        {
            var affects = 0;

            lock (_tasksLocker)
            {
                affects = this._scheduledTasks.RemoveAll(aa => aa.Id == taskId);
            }

            return affects;
        }

        /// <summary>
        ///     Starts current scheduler to execute tasks.
        /// </summary>
        public void Run()
        {
            if (!_running)
            {
                _scheduledTasks = new List<CronTask>();
                _timer = new Timer(new TimerCallback(this.Callback), null, TimeSpan.Zero, _interval); // Invokes callback every minute.

                _running = true;
            }
        }

        /// <summary>
        ///     Get a list of scheduled tasks.
        /// </summary>
        /// <returns>
        ///     A list of scheduled tasks.
        /// </returns>
        public List<CronTask> ScheduledTasks()
        {
            var tasks = new List<CronTask>();

            lock (_tasksLocker)
            {
                // Generates a copy of the _scheduledTasks to avoid the unsafe modify outside.
                foreach (var task in this._scheduledTasks)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }


        /// <summary>
        ///     The callback method of <see cref="CronScheduler._timer"/>.
        /// </summary>
        /// <param name="state">
        ///     An object containing information to be used by current callback method, or null.
        /// </param>
        private void Callback(object state)
        {
#if DEBUG
            var startTime = DateTime.Now;
            System.Diagnostics.Debug.WriteLine($"[{startTime.ToString("yyyy-MM-dd HH:mm:ss")}] Begining invoke the callback of cron scheduler.");
#endif
            lock (_tasksLocker)
            {
                foreach (var task in this._scheduledTasks)
                {
                    task.TryExecute();
                }
            }

#if DEBUG
            var duration = DateTime.Now - startTime;
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] The callback of cron scheduler has executed. Duration: {duration.TotalSeconds.ToString("N2")}s.");
#endif
        }
    }
}
