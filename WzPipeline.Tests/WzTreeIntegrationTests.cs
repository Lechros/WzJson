using WzPipeline.Application.Configuration;
using WzPipeline.Application.Sources;
using WzPipeline.MapleData;

namespace WzPipeline.Tests;

public class WzTreeIntegrationTests
{
    [Test]
    public void MatchNodes_ReadsRealGearStringNodeThroughWzNet()
    {
        using var tree = WzTree.Load(ApplicationConfiguration.BaseWzPath);

        var node = tree.MatchNodes(SourcePaths.GearString).First();

        node.Name.Should().NotBeNullOrWhiteSpace();
        node.Nodes.Find("name").Should().NotBeNull();
    }

    [Test]
    public void MatchNodes_ReadsRealGearNodeThroughWzNet()
    {
        using var tree = WzTree.Load(ApplicationConfiguration.BaseWzPath);

        var node = tree.MatchNodes(SourcePaths.Gear).FirstOrDefault();

        node.Should().NotBeNull(
            $"root nodes are: {string.Join(", ", tree.BaseNode.Nodes.Select(n => n.Name))}");
        node!.Name.Should().EndWith(".img");
        node.Nodes.Find("info").Should().NotBeNull();
    }
}
