﻿using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("JettonPass (App)")]
[assembly: AssemblyProduct("JettonPass")]
[assembly: AssemblyMetadata("Default OS", "Windows7-x64")]
[assembly: AssemblyCompany("BMTLab")]
[assembly: AssemblyCopyright("Copyright © BMTLab, 2021")]
[assembly: AssemblyTrademark("BMTLab ®")]
[assembly: AssemblyDefaultAlias("JettonPass")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0")]
[assembly: AssemblyInformationalVersion("1.0")]
[assembly: AssemblyDescription("Application for time management through the use of jettons")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif RELEASE
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: ComVisible(false)]