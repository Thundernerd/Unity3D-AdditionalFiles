using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AdditionalFilesSourceGenerator;

/// <summary>
/// A sample source generator that creates C# classes based on the text file (in this case, Domain Driven Design ubiquitous language registry).
/// When using a simple text file as a baseline, we can create a non-incremental source generator.
/// </summary>
[Generator]
public class SampleSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this generator.
    }

    public void Execute(GeneratorExecutionContext context)
    {
        StringBuilder sb = new StringBuilder();
        
        foreach (AdditionalText additionalFile in context.AdditionalFiles)
        {
            sb.AppendLine("// " + additionalFile.Path);
        }

        if (context.AdditionalFiles.Length == 0)
        {
            sb.AppendLine("// No additional files");
        }

        context.AddSource("AdditionalFilesExample.g.cs",
            $@"// Auto generated {DateTime.UtcNow}

public class AdditionalFilesExample
{{
    public AdditionalFilesExample()
    {{
        {sb.ToString()}
    }}
}}");
    }
}
