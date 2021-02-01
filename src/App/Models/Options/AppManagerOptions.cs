using System.Collections.Generic;

using NullGuard;


namespace JettonPass.App.Models.Options
{
    [NullGuard(ValidationFlags.None)]
    public sealed record AppManagerOptions
    {
        public bool UseBasePath { get; init; }
        public string? BasePath { get; init; }
        public bool SearchInSubfolders { get; init; }
        public ICollection<string>? CustomPaths { get; init; }
    }
}