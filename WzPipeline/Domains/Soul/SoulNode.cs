using Wz;

namespace WzPipeline.Domains.Soul;

public class SoulNode(IWzNode node)
{
    public string Id => node.Name.Split('.')[0].TrimStart('0');
}