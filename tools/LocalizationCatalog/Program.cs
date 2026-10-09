using System.Text.Json;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var root = Path.GetFullPath(args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "../../../../"));
var output = Path.GetFullPath(args.Length > 1 ? args[1] : Path.Combine(root, "docs/localization/source-candidates.json"));
var candidates = new SortedSet<string>(StringComparer.Ordinal);
var wrappedKeys = new SortedSet<string>(StringComparer.Ordinal);
var sourceRoot = Path.Combine(root, "src");
foreach (var file in Directory.EnumerateFiles(sourceRoot, "*.cs", SearchOption.AllDirectories)
             .Where(path => !path.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar) && !path.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar)))
{
    var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file), path: file);
    foreach (var node in tree.GetRoot().DescendantNodes())
    {
        if (node is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.StringLiteralExpression))
        {
            var wrapped = IsLocalizationCall(literal);
            if (wrapped || IsUiText(literal)) Add(literal.Token.ValueText, wrapped);
        }
        if (node is InterpolatedStringExpressionSyntax interpolated && (IsLocalizationCall(interpolated) || IsUiText(interpolated)))
        {
            var wrapped = IsLocalizationCall(interpolated);
            var placeholder = 0;
            var template = string.Concat(interpolated.Contents.Select(part => part switch
            {
                InterpolatedStringTextSyntax text => text.TextToken.ValueText,
                InterpolationSyntax expression => "{" + placeholder++ + (expression.AlignmentClause?.ToString() ?? "") + (expression.FormatClause is null ? "" : ":" + expression.FormatClause.FormatStringToken.ValueText) + "}",
                _ => "",
            }));
            Add(template, wrapped);
        }
    }
}

foreach (var file in Directory.EnumerateFiles(sourceRoot, "*.xaml", SearchOption.AllDirectories)
             .Where(path => !path.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar) && !path.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar)))
{
    var document = XDocument.Load(file);
    foreach (var attribute in document.Descendants().Attributes().Where(attribute =>
                 attribute.Name.LocalName is "Text" or "Content" or "Header" or "Title" or "ToolTip" or "Label" or "Watermark"))
        Add(attribute.Value, false);
}

Directory.CreateDirectory(Path.GetDirectoryName(output)!);
File.WriteAllText(output, JsonSerializer.Serialize(candidates, new JsonSerializerOptions { WriteIndented = true }) + Environment.NewLine);
var wrappedOutput = Path.Combine(Path.GetDirectoryName(output)!, "source-keys.json");
File.WriteAllText(wrappedOutput, JsonSerializer.Serialize(wrappedKeys, new JsonSerializerOptions { WriteIndented = true }) + Environment.NewLine);
Console.WriteLine($"Wrote {candidates.Count} localization candidates and {wrappedKeys.Count} wrapped source keys to {output}");

void Add(string value, bool wrapped)
{
    if (!wrapped && value.StartsWith('{') && value.EndsWith('}')) return; // XAML markup extensions are expressions, not UI copy.
    if (value.Length > 0 && value.Any(char.IsLetter))
    {
        candidates.Add(value);
        if (wrapped) wrappedKeys.Add(value);
    }
}

static bool IsLocalizationCall(SyntaxNode expression)
{
    for (var parent = expression.Parent; parent is not null && parent is not InvocationExpressionSyntax; parent = parent.Parent) { }
    var invocation = expression.Ancestors().OfType<InvocationExpressionSyntax>().FirstOrDefault();
    if (invocation is null) return false;
    var name = invocation.Expression switch
    {
        IdentifierNameSyntax identifier => identifier.Identifier.ValueText,
        MemberAccessExpressionSyntax member => member.Name.Identifier.ValueText,
        _ => "",
    };
    return name is "L" or "LF" or "Translate" or "Format";
}

static bool IsUiText(SyntaxNode expression)
{
    foreach (var ancestor in expression.Ancestors().TakeWhile(node => node is not BlockSyntax and not MethodDeclarationSyntax and not LocalFunctionStatementSyntax))
    {
        if (ancestor is AssignmentExpressionSyntax assignment && assignment.Left is MemberAccessExpressionSyntax member &&
            member.Name.Identifier.ValueText is "Text" or "Content" or "Header" or "Title" or "ToolTip" or "Filter") return true;
        if (ancestor is AnonymousObjectMemberDeclaratorSyntax initializer && initializer.NameEquals is not null &&
            initializer.NameEquals.Name.Identifier.ValueText is "Text" or "Content" or "Header" or "Title" or "ToolTip" or "Filter") return true;
        if (ancestor is InvocationExpressionSyntax invocation && invocation.Expression.ToString().Contains("MessageBox.Show", StringComparison.Ordinal)) return true;
        if (ancestor is InvocationExpressionSyntax call && call.Expression is IdentifierNameSyntax name && name.Identifier.ValueText is "Label" or "Button") return true;
    }
    return false;
}
