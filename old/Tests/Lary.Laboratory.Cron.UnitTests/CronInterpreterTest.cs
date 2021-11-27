using Lary.Laboratory.Cron.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lary.Laboratory.Cron.UnitTests
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="CronInterpreter"/>.
    /// </summary>
    [TestClass]
    public class CronInterpreterTest
    {
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

        /// <summary>
        ///     Tests the parse of abnormal cron expression.
        /// </summary>
        /// <param name="cronExpr">
        ///     A string that represents the cron expression to parse.
        /// </param>
        [TestMethod]
        [DataRow("* * * *")]
        [DataRow("*/* */2 * * *")]
        [DataRow("* */2 janu * *")]
        public void ParseWithError(string cronExpr)
        {
            Assert.ThrowsException<CronParserException>(() =>
            {
                CronInterpreter.Parse(cronExpr);
            });
        }
    }
}
