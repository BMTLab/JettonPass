using JetBrains.Annotations;

using NullGuard;


namespace JettonPass.Launcher.Models.Options
{
    [NullGuard(ValidationFlags.None)]
    public sealed record LauncherOptions
    {
        public bool ShutdownExplorer { get; init; }
        public string AppPath { get; [UsedImplicitly] init; } = string.Empty;
    }
}