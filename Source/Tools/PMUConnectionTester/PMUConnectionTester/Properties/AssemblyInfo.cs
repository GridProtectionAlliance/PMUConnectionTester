using System.Reflection;
using System.Runtime.InteropServices;

// Assembly identity attributes.
[assembly: AssemblyVersion("4.7.0.0")]

// Informational attributes.
[assembly: AssemblyCompany("Grid Protection Alliance")]
[assembly: AssemblyCopyright("Copyright © 2010-2022, All Rights Reserved.")]
[assembly: AssemblyProduct("GSF")]

// Assembly manifest attributes.
#if DEBUG
[assembly: AssemblyConfiguration("Debug Build")]
#else
[assembly: AssemblyConfiguration("Release Build")]
#endif

[assembly: AssemblyTitle("PMU Connection Tester")]
[assembly: AssemblyDescription("PMU Connection Tester")]

// Other configuration attributes.
[assembly: ComVisible(false)]
[assembly: Guid("8a76a65e-5664-441e-9a8d-fe0909047c3d")]
