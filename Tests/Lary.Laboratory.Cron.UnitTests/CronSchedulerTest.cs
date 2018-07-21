using Lary.Laboratory.Cron.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Lary.Laboratory.Cron.UnitTests
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="CronScheduler"/>.
    /// </summary>
    [TestClass]
    public class CronSchedulerTest
    {
        private CronScheduler _scheduler = CronScheduler.Instance;

        /// <summary>
        ///     Initializes basic data for tests.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // Runs scheduler.
            _scheduler.Run();
        }

        /// <summary>
        ///     Test for adding a task to <see cref="CronScheduler"/>.
        /// </summary>
        /// <param name="cronExpr">
        ///     The string representation of the task's CronInfo.
        /// </param>
        [TestMethod]
        [DataRow("* * * * *")]
        public void AddSingleTask(string cronExpr)
        {
            var counter = 0;

            var success = _scheduler.AddTask(cronExpr, () =>
            {
                ++counter;
                Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Callback of scheduled task was invoked.");
            });

            Thread.Sleep(TimeSpan.FromSeconds(135)); // Sets the test lifecycle.
            Assert.IsTrue(success && counter == 2);
        }

        /// <summary>
        ///     Test for adding/removing tasks.
        /// </summary>
        [TestMethod]
        public void MultiTasks()
        {
            var cronExpr = "* * * * *";

            var counter_task = 0;
            var cronInfo = CronInterpreter.Parse(cronExpr);
            var task = new CronTask
            {
                Id = Guid.NewGuid().ToString("N"),
                CronInfo = cronInfo,
                IsEnabled = true,
                Action = () =>
                {
                    ++counter_task;
                    Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Callback. ---Task 01");
                }
            };

            //  Adds tasks.
            var success_task = _scheduler.AddTask(task);

            var counter_expr = 0;
            var success_expr = _scheduler.AddTask(cronExpr, () =>
            {
                ++counter_expr;
                Thread.Sleep(TimeSpan.FromSeconds(3)); // Simulates time-consuming operation.
                Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Callback. ---Task 02");
            });

            var counter_info = 0;
            var success_info = _scheduler.AddTask(cronInfo, () =>
            {
                ++counter_info;
                Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Callback. ---Task 03");
            });

            // Gets scheduled tasks.
            var scheduledTasks = _scheduler.ScheduledTasks();
            scheduledTasks.RemoveAt(0);
            var modifiedScheduledTasks = _scheduler.ScheduledTasks();

            // Removes task.
            var taskToRemove = scheduledTasks.Last();
            _scheduler.Remove(taskToRemove.Id);

            // Gets scheduled tasks again.
            modifiedScheduledTasks = _scheduler.ScheduledTasks();

            Thread.Sleep(TimeSpan.FromSeconds(135)); // Sets the test lifecycle.

            Assert.IsTrue(
                   success_task
                && success_expr
                && success_info
                && counter_task == 2
                && counter_expr == 2
                && counter_info == 0
            );
        }
    }
}
