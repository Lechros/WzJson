using FluentAssertions;
using WzJson.Common.Data;

namespace WzJson.Common.Tests;

[TestFixture]
public class JsonDataTests
{
    [Test]
    public void Ctor_LabelAndPath_PropertiesReturn()
    {
        const string label = "label";
        const string path = "test-data.json";

        var data = new JsonData<object>(label, path);

        data.Label.Should().Be(label);
        data.Path.Should().Be(path);
    }

    [Test]
    public void Add_NewKeyValuePair_ItemsContainsPair()
    {
        const string label = "label";
        const string path = "test-data.json";
        const string key = "1234567";
        const int value = 123;
        var data = new JsonData<int>(label, path);

        data.Add(key, value);

        data[key].Should().Be(value);
    }
}