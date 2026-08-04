using Wz;

namespace WzPipeline.MapleData;

public delegate IWzNode? GlobalFindNodeFunction(string path);

public static class WzNodeExtensions
{
    public static string GetFullPath(this IWzNode node)
    {
        var segments = new Stack<string>();
        for (var current = node; current is not null; current = current.Parent)
            segments.Push(current.Name);
        return string.Join('/', segments);
    }

    public static IWzNode? GetLinkedSourceNode(this IWzNode node, GlobalFindNodeFunction findNode)
    {
        var source = node.Nodes.Find("source")?.GetString();
        if (!string.IsNullOrEmpty(source))
            return findNode(source)?.ResolveUol(findNode);

        var inlink = node.Nodes.Find("_inlink")?.GetString();
        if (!string.IsNullOrEmpty(inlink))
            return GetImageRoot(node)
                .FindNode(inlink, WzNodeFindOptions.LoadImage)?
                .ResolveUol(findNode);

        var outlink = node.Nodes.Find("_outlink")?.GetString();
        if (!string.IsNullOrEmpty(outlink))
            return findNode(outlink)?.ResolveUol(findNode);

        return node;
    }

    public static IWzNode? ResolveUol(this IWzNode node, GlobalFindNodeFunction findNode)
    {
        var visited = new HashSet<IWzNode>(ReferenceEqualityComparer.Instance);
        while (node.Type == WzNodeType.Uol)
        {
            if (!visited.Add(node))
                throw new InvalidDataException($"UOL cycle detected at '{node.GetFullPath()}'.");

            var uol = node.GetUol();
            var target = uol.Path?.StartsWith('/') == true
                ? findNode(uol.Path.TrimStart('/'))
                : uol.ResolveTarget(node);

            if (target is null)
                return null;

            node = target;
        }

        return node;
    }

    private static IWzNode GetImageRoot(IWzNode node)
    {
        var root = node;
        while (root.Parent is WzImageNode parent)
            root = parent;
        return root;
    }
}
