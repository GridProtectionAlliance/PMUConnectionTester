using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Assembly identity attributes.
[assembly: AssemblyVersion("4.7.11.0")]

// Grants the test project access to internal types (ApiSettings, engine types) for unit testing.
[assembly: InternalsVisibleTo("PMUConnectionTester.Api.Tests")]

// Lets Moq's Castle DynamicProxy generate a mock for the internal IPmuConnectionTestEngine interface.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

// Informational attributes.
[assembly: AssemblyCompany("Grid Protection Alliance")]
[assembly: AssemblyCopyright("Copyright © 2010-2026, All Rights Reserved.")]
[assembly: AssemblyProduct("GSF")]

// Assembly manifest attributes.
#if DEBUG
[assembly: AssemblyConfiguration("Debug Build")]
#else
[assembly: AssemblyConfiguration("Release Build")]
#endif

[assembly: AssemblyTitle("PMU Connection Tester API")]
[assembly: AssemblyDescription("PMU Connection Tester REST API")]

// Other configuration attributes.
[assembly: ComVisible(false)]
[assembly: Guid("2f3f9f2e-2f36-4a3a-9b3a-6c1c6f6bfb51")]
