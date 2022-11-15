using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Comparers;

internal sealed class ObservableTypeInfoComparer : IComparer<ObservableTypeInfo>
{
    private ObservableTypeInfoComparer() { }
    private static ObservableTypeInfoComparer? _instance = null;
    private static object _mutex = new();
    public static ObservableTypeInfoComparer Default
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

    public int Compare(ObservableTypeInfo? x, ObservableTypeInfo? y)
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
