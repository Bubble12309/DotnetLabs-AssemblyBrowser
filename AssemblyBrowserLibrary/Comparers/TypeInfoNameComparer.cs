using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Comparers;

internal sealed class TypeInfoNameComparer : IComparer<TypeInfo?>
{
    private TypeInfoNameComparer() { }
    private static TypeInfoNameComparer? _instance = null;
    private static object _mutex = new();
    public static TypeInfoNameComparer Default
    {
        get
        {
            if (_instance is null)
            {
                lock (_mutex)
                {
                    if (_instance is null)
                    {
                        _instance = new();
                    }
                }
            }
            return _instance;
        }
    }
    public int Compare(TypeInfo? x, TypeInfo? y)
    {
        if (x is null && y is null)
            return 0;
        if (x is null)
            return -1;
        if (y is null)
            return 1;
        return string.Compare(x.Name, y.Name, ignoreCase: true);
    }
}
