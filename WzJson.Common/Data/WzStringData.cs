namespace WzJson.Common.Data;

public class WzString : Dictionary<string, string>
{
    public string? Name
    {
        get => TryGetValue("name", out var name) ? name : null;
        set => this["name"] = value;
    }

    public string? Desc
    {
        get => TryGetValue("desc", out var desc) ? desc : null;
        set => this["desc"] = value;
    }
}

public class WzStringData : Dictionary<string, WzString>, IData
{
    public void Add<T>(string key, T item) where T : notnull
    {
        base.Add(key, item as WzString);
    }
}