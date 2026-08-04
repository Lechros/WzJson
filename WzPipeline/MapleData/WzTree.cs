using Wz;

namespace WzPipeline.MapleData;

public sealed class WzTree : IDisposable
{
    private readonly WzStructure structure;

    private WzTree(WzStructure structure)
    {
        this.structure = structure;
    }

    public IWzNode BaseNode => structure.Node;

    public static WzTree Load(string baseWzPath)
    {
        if (!File.Exists(baseWzPath))
            throw new FileNotFoundException($"Base.wz not found at: {baseWzPath}");

        return new WzTree(WzStructure.Load(baseWzPath));
    }

    public IWzNode? FindNode(string path) => BaseNode.FindNode(path, WzNodeFindOptions.LoadImage);

    public IEnumerable<IWzNode> MatchNodes(string pattern) => WzMatcher.Match(BaseNode, pattern);

    public void Dispose() => structure.Dispose();
}