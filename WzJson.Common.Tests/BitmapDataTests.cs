using System.Drawing;
using WzJson.Common.Data;

namespace WzJson.Common.Tests;

public class BitmapDataTests
{
    [Fact]
    public void Ctor_LabelAndPath_PropertiesReturn()
    {
        const string label = "label";
        const string path = "test-data.json";

        var data = new BitmapData(label, path);

        Assert.Equal(label, data.Label);
        Assert.Equal(path, data.Path);
    }

    [Fact]
    public void Add_NewKeyValuePair_ItemsContainsPair()
    {
        const string path = "test-data.json";
        const string key = "1234567";
        using var value = new Bitmap(1, 1);
        var data = new BitmapData(path, path);

        data.Add(key, value);

        Assert.Equal(value, data.Items[key]);
    }
}