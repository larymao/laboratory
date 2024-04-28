using Lary.Laboratory.Core.Json;

namespace Lary.Laboratory.Core.Tests.Json;

[TestClass]
public class JsonFormatterTests
{
    [TestMethod]
    public void FormatJson()
    {
        var innerObj = new
        {
            InnerPropA = 1,
            InnerPropB = "Inner Prop B",
            InnerPropC = "This is a composite format string. Arg0: {0}. Arg1: {1}"
        };

        var obj = new
        {
            PropA = 1,
            PropB = 1.234,
            PropC = "Hello",
            PropD = false,
            propE = new[] { 1, 2, 3 },
            propF = new[] { "Hello 1", "Hello 2" },
            PropG = innerObj,
            PropH = JsonConvert.SerializeObject(innerObj)
        };

        var jsonString = JsonConvert.SerializeObject(obj, Formatting.None);

        Console.WriteLine(JsonFormatter.FormatJson(jsonString));
    }
}
