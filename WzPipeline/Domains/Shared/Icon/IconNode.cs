using System.Drawing;
using Wz;
using WzPipeline.MapleData;

namespace WzPipeline.Domains.Shared.Icon;

public sealed class IconNode
{
    private readonly IWzNode node;
    private readonly IWzNode sourceNode;

    private IconNode(string id, IWzNode node, IWzNode sourceNode)
    {
        Id = id;
        this.node = node;
        this.sourceNode = sourceNode;
    }

    public string Id { get; }
    public Bitmap Image => sourceNode.GetCanvas().ExtractBitmap();

    public Point? Origin
    {
        get
        {
            var originNode = node.Nodes.Find("origin");
            if (originNode is null)
                return null;

            var vector = originNode.GetVector();
            return new Point(vector.X, vector.Y);
        }
    }

    public static IconNode Create(string id, IWzNode node, GlobalFindNodeFunction findNode)
    {
        ArgumentNullException.ThrowIfNull(node);
        node = node.ResolveUol(findNode) ?? node;
        var sourceNode = node.GetLinkedSourceNode(findNode) ??
                         throw new InvalidOperationException($"Linked icon source was not found: {node.GetFullPath()}");
        return new IconNode(id, node, sourceNode);
    }
}