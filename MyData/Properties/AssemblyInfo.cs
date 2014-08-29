using System.Reflection;
using System.Runtime.InteropServices;
using MyData.Properties;

// Project-specific information
[assembly: AssemblyTitle("My Data Library")]
[assembly: AssemblyDescription("Read data from various sources")]

// Solution-wide information about the assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with all assemblies across the solution.
[assembly: AssemblyProduct("My Data Library")]
[assembly: AssemblyCompany("Jan Magne Tjensvold")]
[assembly: AssemblyCopyright("Copyright © 2014 Jan Magne Tjensvold")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(AssemblyVersion.Main + ".*")]
[assembly: AssemblyFileVersion(AssemblyVersion.Main + AssemblyVersion.PreRelease)]
[assembly: AssemblyInformationalVersion(AssemblyVersion.Main + AssemblyVersion.PreRelease)]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Retail")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

