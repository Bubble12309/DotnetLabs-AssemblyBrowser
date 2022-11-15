using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public sealed class ObservableNamespaceInfo
{
    public string Name { get; }
    public ReadOnlyObservableCollection<ObservableTypeInfo> InternalTypes { get; internal set; } 
    internal ObservableNamespaceInfo(string name)
    {
        Name = name;
    }
}
