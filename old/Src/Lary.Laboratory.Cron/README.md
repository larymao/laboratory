# Lary.Laboratory.Cron
主要用于解析Cron表达式，遵循Linux Cron的语法规范，支持根据Cron表达式设置定时任务。

## 语法简介
```shell
.---------------- minute (0 - 59)
|  .------------- hour (0 - 23)
|  |  .---------- day of month (1 - 31)
|  |  |  .------- month (1 - 12) OR jan,feb,mar,apr ...
|  |  |  |  .---- day of week (0 - 6) (Sunday=0 or 7) OR sun,mon,tue,wed,thu,fri,sat
|  |  |  |  |
*  *  *  *  *
```

## 样例代码
请查看`Lary.Laboratory.Cron.UnitTests`以获取样例更多样例，此处仅提供部分参考：
* 正常解析Cron表达式
  ```csharp
  /// <summary>
  ///     Tests the parse of normal cron expression.
  /// </summary>
  /// <param name="cronExpr">
  ///     A string that represents the cron expression to parse.
  /// </param>
  [TestMethod]
  [DataRow("* * * * *")]
  [DataRow("*/10 */2 * 1-3,7-9 0,6")]
  [DataRow("*/10 0-5,22-23 * january-mar,JULY-SEP,11,12 MON,tue,WEDNESDAY,thursday,5")]
  public void ParseSuccessfully(string cronExpr)
  {
      var cronInfo = CronInterpreter.Parse(cronExpr);
  
      Assert.IsFalse(cronInfo == null);
  }
  ```

* 根据Cron表达式设置定时任务
  ```csharp
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
      var _scheduler = CronScheduler.Instance;
  
      // Runs scheduler.
      _scheduler.Run();
  
      var success = _scheduler.AddTask(cronExpr, () =>
      {
          ++counter;
          Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Callback of scheduled task was invoked.");
      });
  
      Thread.Sleep(TimeSpan.FromSeconds(135)); // Sets the test lifecycle.
      Assert.IsTrue(success && counter == 2);
  }
  ```