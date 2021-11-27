using Lary.Laboratory.Core.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Lary.Laboratory.Core.Tests.Json
{
    /// <summary>
    /// Provides tests for <see cref="BaseFirstContractResolver"/>.
    /// </summary>
    [TestClass]
    public class BaseFirstContractResolverTests
    {
        [TestMethod]
        public void JsonSerializeWithBaseFirstContractResolver()
        {
            var c = new C
            {
                Id = 1,
                AA = "AA",
                Message = "hello",
                BB = "BB",
                Remark = "unknown",
                CC = "CC",
                CreateTime = DateTime.Now
            };

            var settings = new JsonSerializerSettings { ContractResolver = new BaseFirstContractResolver() };
            var jsonStr = JsonConvert.SerializeObject(c, settings);
            Assert.IsTrue(Regex.IsMatch(jsonStr, "AA.*BB.*CC"));
            Assert.IsTrue(Regex.IsMatch(jsonStr, @"""CreateTime"": *""\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}"""));

            Console.WriteLine($"{nameof(BaseFirstContractResolver)}: \n{jsonStr}\n");
            Console.WriteLine($"DefaultResolver: \n{JsonConvert.SerializeObject(c)}\n");
        }

        internal class A
        {
            public int Id { get; set; }

            public string? AA { get; set; }
        }

        internal class B : A
        {
            public string? Message { get; set; }

            public string? BB { get; set; }
        }

        internal class C : B
        {
            public string? Remark { get; set; }

            public string? CC { get; set; }

            [JsonConverter(typeof(DateTimeFormatConverter), "yyyy-MM-dd HH:mm:ss")]
            public DateTimeOffset CreateTime { get; set; }
        }
    }
}
