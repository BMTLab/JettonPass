using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Jetton Pass Launcher")]
[assembly: AssemblyProduct("JettonPass")]
[assembly: AssemblyMetadata("Default OS", "Windows7")]
[assembly: AssemblyCompany("BMTLab")]
[assembly: AssemblyCopyright("Copyright © BMTLab, 2021")]
[assembly: AssemblyTrademark("BMTLab ®")]
[assembly: AssemblyDefaultAlias("JettonPass")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0")]
[assembly: AssemblyInformationalVersion("1.0")]
[assembly: AssemblyDescription("Launcher keeping track of the state of the main app")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif RELEASE
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: ComVisible(false)]