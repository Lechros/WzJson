using Wz;

namespace WzPipeline.Domains.SetItem;

public class SetItemNode(IWzNode node)
{
    public string Id => node.Name;
    public string Name => node.Nodes["setItemName"].GetString()!;

    public IEnumerable<int> ItemIds
    {
        get
        {
            foreach (var subNode in node.Nodes["ItemID"].Nodes)
            {
                if (subNode.Nodes.Count == 0)
                {
                    yield return subNode.GetInt32();
                    continue;
                }

                foreach (var partNode in subNode.Nodes)
                {
                    switch (partNode.Name)
                    {
                        case "representName":
                        case "typeName":
                        case "byGender":
                            break;
                        default:
                            yield return partNode.GetInt32();
                            break;
                    }
                }
            }
        }
    }

    public IEnumerable<EffectNode> Effects =>
        node.Nodes["Effect"].Nodes.Select(effectNode => new EffectNode(effectNode));

    public bool JokerPossible => (node.Nodes.Find("jokerPossible")?.GetInt32() ?? 0) != 0;

    public bool ZeroWeaponJokerPossible =>
        (node.Nodes.Find("zeroWeaponJokerPossible")?.GetInt32() ?? 0) != 0;

    public class EffectNode(IWzNode effectNode)
    {
        public int Index => int.Parse(effectNode.Name);

        public IEnumerable<(string Type, int value)> Properties
        {
            get
            {
                foreach (var property in effectNode.Nodes)
                {
                    if (property.Name != "Option" && property.TryConvertInt32(out var value))
                        yield return (property.Name, value);
                }
            }
        }

        public IEnumerable<(int OptionCode, int Level)> Options => effectNode.Nodes.Find("Option")?.Nodes
            .Select(n => (n.Nodes["option"].GetInt32(), n.Nodes["level"].GetInt32())) ?? [];
    }
}
