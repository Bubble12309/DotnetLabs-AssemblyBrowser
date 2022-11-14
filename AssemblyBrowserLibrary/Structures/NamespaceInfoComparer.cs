using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

internal sealed class NamespaceInfoComparer : IComparer<NamespaceInfo?>
{
    private NamespaceInfoComparer() { }
    private static NamespaceInfoComparer? _instance = null;
    private static Mutex _mutex = new();
    public static NamespaceInfoComparer Default 
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
    
    public int Compare(NamespaceInfo? x, NamespaceInfo? y)
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
