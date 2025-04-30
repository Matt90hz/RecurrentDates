using DiffEngine;
using System.Runtime.CompilerServices;

namespace Tests;

public static class _Initializers
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifyDiffPlex.Initialize();
        DiffTools.UseOrder([DiffTool.VisualStudio, DiffTool.VisualStudioCode]);
        VerifierSettings.AddExtraSettings(x =>
        {
            x.MaxDepth = 10;
            x.NullValueHandling = Argon.NullValueHandling.Include;
            x.DefaultValueHandling = Argon.DefaultValueHandling.Include;
            x.ReferenceLoopHandling = Argon.ReferenceLoopHandling.Ignore;
        });
        VerifierSettings.DontScrubDateTimes();
        VerifierSettings.DontIgnoreEmptyCollections();
    }
}
