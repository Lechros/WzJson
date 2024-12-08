using WzComparerR2.WzLib;
using WzJson.Common.Converter;
using WzJson.Repository;

namespace WzJson;

public sealed class GlobalStringDataProvider
{
    private readonly Lazy<GlobalStringData> lazyData;

    public GlobalStringDataProvider(
        StringConsumeNodeRepository stringConsumeNodeRepository,
        StringEqpNodeRepository stringEqpNodeRepository,
        StringSkillNodeRepository stringSkillNodeRepository)
    {
        lazyData = new Lazy<GlobalStringData>(() => ReadData(
            stringConsumeNodeRepository,
            stringEqpNodeRepository,
            stringSkillNodeRepository));
    }

    public GlobalStringData GlobalStringData => lazyData.Value;

    private GlobalStringData ReadData(
        StringConsumeNodeRepository stringConsumeNodeRepository,
        StringEqpNodeRepository stringEqpNodeRepository,
        StringSkillNodeRepository stringSkillNodeRepository)
    {
        var converter = new WzStringConverter();
        return new GlobalStringData
        {
            Consume = converter.Convert(stringConsumeNodeRepository.GetNodes()),
            Eqp = converter.Convert(stringEqpNodeRepository.GetNodes()),
            Skill = converter.Convert(stringSkillNodeRepository.GetNodes(), GetSkillNodeName)
        };
    }

    private string GetSkillNodeName(Wz_Node node)
    {
        return node.Text.TrimStart('0');
    }
}