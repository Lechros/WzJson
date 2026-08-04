using Wz;

namespace WzPipeline.Domains.Soul;

public class SoulCollectionNode(IWzNode node)
{
    public string Id => node.Name;
    public int SoulSkill => node.Nodes["soulSkill"].GetInt32();
    public int? SoulSkillH => node.Nodes.Find("soulSkillH")?.GetInt32();
    public int[][] SoulList => node.Nodes["soulList"].Nodes.Select(SoulListNodeToArray).ToArray();

    private static int[] SoulListNodeToArray(IWzNode node) => node.Nodes.Select(n => n.GetInt32()).ToArray();
}
