using Wz;
using WzPipeline.Domains.Shared;
using WzPipeline.Domains.Shared.Icon;
using WzPipeline.MapleData;

namespace WzPipeline.Domains.Item;

public class ItemNode(IWzNode node)
{
    private IWzNode InfoNode => node.Nodes.Find("info") ?? throw DataFormatException.MissingRequiredNode(node, "info");

    public string Id => node.Name;

    public IconNode? GetIconNode(GlobalFindNodeFunction findNode)
    {
        var iconNode = InfoNode.FindNode("icon");
        return iconNode is null ? null : IconNode.Create(Id, iconNode, findNode);
    }

    public IconNode? GetIconRawNode(GlobalFindNodeFunction findNode)
    {
        var iconRawNode = InfoNode.FindNode("iconRaw");
        return iconRawNode is null ? null : IconNode.Create(Id, iconRawNode, findNode);
    }
}