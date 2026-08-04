using Wz;

namespace WzPipeline.Domains.Shared.String;

public class StringNode(IWzNode node)
{
    public string Key => node.Name;
    public string? Name => node.Nodes.Find("name")?.GetString();
    public string? Desc => node.Nodes.Find("desc")?.GetString();
}
