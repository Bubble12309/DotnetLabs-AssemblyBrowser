using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public sealed class NamespaceInfo
{
    public string Name { get; }
    public IReadOnlyCollection<TypeInfo>? InternalTypes { get; internal set; }
    internal NamespaceInfo(string name)
    {
        Name = name;
    }
}
