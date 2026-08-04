using Wz;
using WzPipeline.Domains.Shared;

namespace WzPipeline.Domains.AstraSubWeapon;

public class SubWeaponTransferNode(IWzNode node)
{
    public IWzNode Node => node;
    public string Id => node.Name;
    public int Job => int.Parse(Id);

    public IEnumerable<IEnumerable<int>> TargetIdGroups
    {
        get
        {
            var targetNode = node.Nodes.Find("target") ?? throw DataFormatException.MissingRequiredNode(node, "target");

            var shieldNode = targetNode.FindNode("shield");
            if (shieldNode is not null)
                yield return shieldNode.Nodes.Select(n => int.Parse(n.Name));

            var nonshieldNode = targetNode.FindNode("nonshield");
            if (nonshieldNode is not null)
                yield return nonshieldNode.Nodes.Select(n => int.Parse(n.Name));

            if (shieldNode is null && nonshieldNode is null)
                yield return targetNode.Nodes.Select(n => int.Parse(n.Name));
        }
    }
}