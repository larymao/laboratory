using Lary.Laboratory.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lary.Laboratory.Core.Tests.Utils
{
    /// <summary>
    /// Provides tests for <see cref="MathHelper"/>.
    /// </summary>
    [TestClass]
    public class MathHelperTests
    {
        [TestMethod]
        public void RoundObject()
        {
            var bb = new B
            {
                PropA1 = 1.2345M,
                PropA2 = null,
                PropA3 = 1.2345f,
                PropA4 = new List<decimal>
                {
                    2.34567M,
                    3.45678M,
                    4.56789M
                },
                PropA5 = "hello world",
                PropA6 = 1,
                PropA7 = DateTime.Now,
                PropB1 = new List<decimal>
                {
                    9.87654M,
                    8.76543M
                },
                PropB2 = new List<double>
                {
                    9.87654d,
                    8.76543d
                },
                PropB3 = new List<float?>
                {
                    9.87654f,
                    8.76543f,
                    null
                },
                PropB4 = new List<string>
                {
                    "hello class b"
                },
                PropB5 = new C[]
                {
                    new C { PropC1 = 1.91734134 }
                }
            };

            var newBB = MathHelper.Round(bb);
            Console.WriteLine(JsonConvert.SerializeObject(newBB, Formatting.Indented));
        }

        internal class A
        {
            [Fraction(7)]
            public decimal PropA1 { get; set; }

            public double? PropA2 { get; set; }

            [Fraction(1)]
            public float PropA3 { get; set; }

            [Fraction(4)]
            public List<decimal>? PropA4 { get; set; }

            public string? PropA5 { get; set; }

            public int PropA6 { get; set; }

            public DateTime PropA7 { get; set; }
        }

        internal class B : A
        {
            public List<decimal>? PropB1 { get; set; }

            public List<double>? PropB2 { get; set; }

            public List<float?>? PropB3 { get; set; }

            public List<string>? PropB4 { get; set; }

            public C[] PropB5 { get; set; } = default!;
        }

        internal class C
        {
            public double PropC1 { get; set; }
        }
    }
}
