using System.Collections;
using Wz;
using WzPipeline.Domains.Shared;
using WzPipeline.Domains.Shared.Icon;
using WzPipeline.MapleData;

namespace WzPipeline.Domains.Gear;

public class GearNode(IWzNode node)
{
    private IWzNode InfoNode => node.Nodes.Find("info") ?? throw DataFormatException.MissingRequiredNode(node, "info");

    public int? Id => SafeIdParse(node.Name);

    public bool IsCash
    {
        get
        {
            var cashNode = InfoNode.Nodes.Find("cash");
            return cashNode is not null && cashNode.TryConvertInt32(out var cash) && cash != 0;
        }
    }

    public (int OptionCode, int Level)[]? Options => InfoNode.Nodes.Find("option")?.Nodes
        .Select(n => (n.Nodes["option"].GetInt32(), n.Nodes["level"].GetInt32()))
        .ToArray();

    public IReadOnlyDictionary<GearPropType, int> Properties => new PropertyDictionary(
        InfoNode, new HashSet<string> { "icon", "iconRaw", "addition", "option" }, "onlyUpgrade");

    public IEnumerable<int> ReqSpecJobs
    {
        get
        {
            var reqSpecJobsNode = InfoNode.FindNode("reqSpecJobs");
            if (reqSpecJobsNode is not null)
                foreach (var jobNode in reqSpecJobsNode.Nodes)
                    yield return jobNode.GetInt32();
        }
    }

    public IconNode? GetIconNode(GlobalFindNodeFunction findNode)
    {
        var iconNode = InfoNode.FindNode("icon");
        return iconNode is null ? null : IconNode.Create(Id?.ToString() ?? "(null)", iconNode, findNode);
    }

    public IconNode? GetIconRawNode(GlobalFindNodeFunction findNode)
    {
        var iconRawNode = InfoNode.FindNode("iconRaw");
        return iconRawNode is null ? null : IconNode.Create(Id?.ToString() ?? "(null)", iconRawNode, findNode);
    }

    private static int? SafeIdParse(string text) => int.TryParse(text.Split('.')[0], out var id) ? id : null;

    private sealed class PropertyDictionary(
        IWzNode infoNode,
        IReadOnlySet<string> ignoredProperties,
        string onlyUpgrade) : IReadOnlyDictionary<GearPropType, int>
    {
        private IEnumerable<(GearPropType Key, int Value)> Entries
        {
            get
            {
                foreach (var node in infoNode.Nodes)
                {
                    if (ignoredProperties.Contains(node.Name) ||
                        !Enum.TryParse<GearPropType>(node.Name, out var key) ||
                        !TryGetNodeValue(node, out var value))
                        continue;

                    yield return (key, value);
                }
            }
        }

        private bool TryGetNodeValue(IWzNode valueNode, out int value)
        {
            if (valueNode.Name == onlyUpgrade)
            {
                value = valueNode.Nodes.Count;
                return true;
            }

            return valueNode.TryConvertInt32(out value);
        }

        public IEnumerator<KeyValuePair<GearPropType, int>> GetEnumerator() =>
            Entries.Select(e => KeyValuePair.Create(e.Key, e.Value)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => Entries.Count();
        public bool ContainsKey(GearPropType key) => TryGetValue(key, out _);

        public bool TryGetValue(GearPropType key, out int value)
        {
            var valueNode = infoNode.Nodes.Find(key.ToString());
            if (valueNode is null || ignoredProperties.Contains(key.ToString()) ||
                !TryGetNodeValue(valueNode, out value))
            {
                value = 0;
                return false;
            }

            return true;
        }

        public int this[GearPropType key] => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();

        public IEnumerable<GearPropType> Keys => Entries.Select(e => e.Key);
        public IEnumerable<int> Values => Entries.Select(e => e.Value);
    }
}
