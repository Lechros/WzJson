using WzComparerR2.WzLib;
using WzJson.Common;

namespace WzJson.ItemOption;

public class ItemOptionParser : AbstractWzParser
{
    public const string ItemOptionJsonName = "item-option.json";

    private readonly ItemOptionNodeRepository itemOptionNodeRepository;

    public ItemOptionParser(ItemOptionNodeRepository itemOptionNodeRepository)
    {
        this.itemOptionNodeRepository = itemOptionNodeRepository;
    }

    protected override IEnumerable<Wz_Node> GetNodes() => itemOptionNodeRepository.GetNodes();

    protected override IList<INodeConverter<object>> GetConverters()
    {
        return new List<INodeConverter<object>>
        {
            new ItemOptionConverter(ItemOptionJsonName)
        };
    }
}