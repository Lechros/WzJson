using Wz;

namespace WzPipeline.Domains.Soul;

public class SkillOptionNode(IWzNode node)
{
    public string Id => node.Name;
    public int SkillId => node.Nodes["skillId"].GetInt32();
    public int ReqLevel => node.Nodes["reqLevel"].GetInt32();
    public int IncTableId => node.Nodes.Find("incTableID")?.GetInt32() ?? 0;

    public TempOptionNode[] TempOption => node.Nodes["tempOption"].Nodes.Select(n => new TempOptionNode(n)).ToArray();

    public class TempOptionNode(IWzNode node)
    {
        public int Id => node.Nodes["id"].GetInt32();
        public int Prob => node.Nodes["prob"].GetInt32();
    }
}
