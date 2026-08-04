using Wz;

namespace WzPipeline.Domains.ExclusiveEquip;

public class ExclusiveEquipNode(IWzNode node)
{
    public string Id => node.Name;
    public string? Info => node.Nodes.Find("info")?.GetString();
    public int[] ItemIds => node.Nodes["item"].Nodes.Select(n => n.GetInt32()).ToArray();
    public string? Msg => node.Nodes.Find("msg")?.GetString();
}
