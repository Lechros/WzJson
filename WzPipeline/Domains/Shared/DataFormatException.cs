using Wz;
using WzPipeline.MapleData;

namespace WzPipeline.Domains.Shared;

public class DataFormatException(string message) : Exception(message)
{
    public static DataFormatException MissingRequiredNode(IWzNode node, string path) =>
        new($"Node not found: ParentNode={node.GetFullPath()}, Path={path}");
}