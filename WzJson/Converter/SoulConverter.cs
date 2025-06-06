using System.Text.RegularExpressions;
using WzComparerR2.WzLib;
using WzJson.Common;
using WzJson.Data;
using WzJson.DataProvider;
using WzJson.Model;

namespace WzJson.Converter;

public partial class SoulConverter(
    GlobalStringDataProvider globalStringDataProvider,
    SoulCollectionDataProvider soulCollectionDataProvider,
    SkillOptionDataProvider skillOptionDataProvider)
    : AbstractNodeConverter<Soul>
{
    [GeneratedRegex(@"추가 잠재능력 : ([\w가-힣]+) \+(\d+)")]
    private static partial Regex SoulDescOptionRegex();

    public override string GetNodeKey(Wz_Node node) => WzUtility.GetNodeCode(node);

    public override Soul? Convert(Wz_Node node, string key)
    {
        if (!int.TryParse(key, out var soulId) || !soulCollectionDataProvider.Data.ContainsSoul(soulId)) return null;
        var skillId = soulCollectionDataProvider.Data.GetSoulSkillId(soulId);
        var magnificent = soulCollectionDataProvider.Data.IsMagnificentSoul(soulId);
        globalStringDataProvider.Data.Consume.TryGetValue(key, out var soulString);
        if (soulString?.Name == null) return null;

        var skillOptionNodes = skillOptionDataProvider.Data.GetNodesBySkillId(skillId);
        var skillOptionNode = magnificent
            ? skillOptionNodes[0]
            : skillOptionNodes[soulCollectionDataProvider.Data.GetSoulIndexInList(soulId)];

        var soul = new Soul
        {
            Name = soulString.Name,
            Skill = GetSoulSkillName(skillId),
            ChargeFactor = GetSoulChargeFactor(skillOptionNode),
            Magnificent = magnificent
        };
        if (magnificent)
            soul.Options = GetSoulRandomOptions(skillOptionNode);
        else
            soul.Option = GetSoulOption(skillOptionNode);

        return soul;
    }

    private string GetSoulSkillName(int skillId)
    {
        return globalStringDataProvider.Data.Skill[skillId.ToString()].Name ??
               throw new ApplicationException("Skill name not found for: " + skillId);
    }

    private int GetSoulChargeFactor(SkillOptionNode skillOptionNode)
    {
        return skillOptionNode.IncTableId;
    }

    private GearOption GetSoulOption(SkillOptionNode skillOptionNode)
    {
        return skillOptionNode.TempOption[0];
    }

    private Soul.RandomOptions GetSoulRandomOptions(SkillOptionNode skillOptionNode)
    {
        return new Soul.RandomOptions
        {
            AttackPower = skillOptionNode.TempOption[0],
            MagicPower = skillOptionNode.TempOption[1],
            AllStat = skillOptionNode.TempOption[2],
            MaxHp = skillOptionNode.TempOption[3],
            CriticalRate = skillOptionNode.TempOption[4],
            IgnoreMonsterArmor = skillOptionNode.TempOption[5],
            BossDamage = skillOptionNode.TempOption[6]
        };
    }
}