using System.Text.RegularExpressions;
using Sprache;
using Wz;

namespace WzPipeline.MapleData;

public sealed class WzMatcher(string pattern)
{
    private readonly List<IPathSegmentSelector> selectors = CreateSelectors(pattern);

    public static IEnumerable<IWzNode> Match(IWzNode node, string pattern) =>
        new WzMatcher(pattern).Match(node);

    public IEnumerable<IWzNode> Match(IWzNode node) => Match(node, 0);

    private IEnumerable<IWzNode> Match(IWzNode node, int depth)
    {
        if (depth == selectors.Count)
        {
            yield return node;
            yield break;
        }

        foreach (var child in selectors[depth].Select(node))
        foreach (var result in Match(child, depth + 1))
            yield return result;
    }

    private static List<IPathSegmentSelector> CreateSelectors(string pattern)
    {
        var tokens = Parsers.Tokens.Parse(pattern).ToList();
        if (tokens.Count == 0)
            throw new ArgumentException($@"Invalid pattern: {pattern}", nameof(pattern));

        var requiredLiteral = true;
        var selectors = new List<IPathSegmentSelector>(tokens.Count);
        foreach (var token in tokens)
        {
            selectors.Add(token switch
            {
                LiteralToken literal => new LiteralSegmentSelector(literal.Value, requiredLiteral),
                BraceToken brace => new SetSegmentSelector(brace.Values),
                GlobToken glob => GlobSegmentSelector.Create(glob.Pattern),
                _ => throw new NotSupportedException($"Unknown token type: {token.GetType()}")
            });

            if (token is not LiteralToken)
                requiredLiteral = false;
        }

        return selectors;
    }

    private interface IPathSegmentSelector
    {
        IEnumerable<IWzNode> Select(IWzNode node);
    }

    private sealed class LiteralSegmentSelector(string value, bool required) : IPathSegmentSelector
    {
        public IEnumerable<IWzNode> Select(IWzNode node)
        {
            var child = node.Nodes.Find(value);
            if (child is not null)
            {
                yield return child;
                yield break;
            }

            if (required)
                throw new InvalidOperationException(
                    $"Required path segment '{value}' was not found under '{node.GetFullPath()}'.");
        }
    }

    private sealed class SetSegmentSelector(IEnumerable<string> values) : IPathSegmentSelector
    {
        private readonly HashSet<string> values = [..values];

        public IEnumerable<IWzNode> Select(IWzNode node) =>
            node.Nodes.Where(child => values.Contains(child.Name));
    }

    private sealed class AllChildrenSegmentSelector : IPathSegmentSelector
    {
        public IEnumerable<IWzNode> Select(IWzNode node) => node.Nodes;
    }

    private sealed class GlobSegmentSelector : IPathSegmentSelector
    {
        private readonly Regex regex;

        private GlobSegmentSelector(string pattern, bool compiled)
        {
            regex = new Regex(
                "^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$",
                compiled ? RegexOptions.Compiled : RegexOptions.None);
        }

        public static IPathSegmentSelector Create(string pattern, bool compiled = false) =>
            pattern == "*" ? new AllChildrenSegmentSelector() : new GlobSegmentSelector(pattern, compiled);

        public IEnumerable<IWzNode> Select(IWzNode node) =>
            node.Nodes.Where(child => regex.IsMatch(child.Name));
    }
}
