using Lary.Laboratory.Core.Math;
using Newtonsoft.Json;

namespace Lary.Laboratory.Core.Tests.Math;

public class MathHelperTests
{
    [Fact]
    public void MatchHelper_Round_AllPropertiesRounded()
    {
        var dt = DateTime.Now;
        var obj = new B
        {
            PropA1 = 1.2345M,
            PropA2 = null,
            PropA3 = 1.2345f,
            PropA4 = [2.34567M, 3.45678M, 4.56789M],
            PropA5 = "hello world",
            PropA6 = 1,
            PropA7 = dt,
            PropB1 = [9.87654M, 8.76543M],
            PropB2 = [9.87654d, 8.76543d],
            PropB3 = [9.87654f, 8.76543f, null],
            PropB4 = ["hello class b"],
            PropB5 = [new C { PropC1 = 1.91734134 }]
        };
        var expected = new B
        {
            PropA1 = 1.2345M,
            PropA2 = null,
            PropA3 = 1.2f,
            PropA4 = [2.3457M, 3.4568M, 4.5679M],
            PropA5 = "hello world",
            PropA6 = 1,
            PropA7 = dt,
            PropB1 = [9.88M, 8.77M],
            PropB2 = [9.88d, 8.77d],
            PropB3 = [9.88f, 8.77f, null],
            PropB4 = ["hello class b"],
            PropB5 = [new C { PropC1 = 1.92 }]
        };

        var roundedObj = MathHelper.Round(obj);

        JsonConvert.SerializeObject(roundedObj)
            .Should().Be(JsonConvert.SerializeObject(expected));
    }

    internal record class A
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

    internal record class B : A
    {
        public List<decimal>? PropB1 { get; set; }

        public List<double>? PropB2 { get; set; }

        public List<float?>? PropB3 { get; set; }

        public List<string>? PropB4 { get; set; }

        public C[] PropB5 { get; set; } = default!;
    }

    internal record class C
    {
        public double PropC1 { get; set; }
    }
}
