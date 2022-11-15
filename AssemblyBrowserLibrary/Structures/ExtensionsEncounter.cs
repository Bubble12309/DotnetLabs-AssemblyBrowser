using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Comparers;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

internal class ExtensionsEncounter
{
    private SortedSet<ObservableTypeInfo> _nonNamespaceTypes;
    private SortedSet<ObservableNamespaceInfo> _namespaces;
    private Dictionary<string, SortedSet<ObservableTypeInfo>> _namespacesDictionary;

    internal ExtensionsEncounter(SortedSet<ObservableTypeInfo> nonNamespaceTypes, 
        SortedSet<ObservableNamespaceInfo> namespaces, Dictionary<string, SortedSet<ObservableTypeInfo>> namespacesDictionary)
    {
        _nonNamespaceTypes = nonNamespaceTypes;
        _namespaces = namespaces;
        _namespacesDictionary = namespacesDictionary;
    }

    internal void Encount(MethodInfo extensionMethodInfo)
    {
        ParameterInfo thisParameter =  extensionMethodInfo.GetParameters().First();
        Type extendedType = thisParameter.ParameterType;
        string? ns = extendedType.Namespace;
        if (ns is null)
        {
            ObservableTypeInfo[] observableTypeArray =  
                _nonNamespaceTypes.Where(observableTypeInfo => observableTypeInfo.Name == extendedType.Name).ToArray();
            if (observableTypeArray.Length == 0)
            {
                ObservableTypeInfo newObservbleTypeInfo = new ObservableTypeInfo(extendedType.GetTypeInfo(), makeEmpty: true);
                newObservbleTypeInfo.InjectMethod(new ObservableMethodInfo(extensionMethodInfo));
            }
            else 
            {
                ObservableTypeInfo observableTypeInfo = observableTypeArray[0];
                observableTypeInfo.InjectMethod(new ObservableMethodInfo(extensionMethodInfo));
            }
        }
        else 
        {
            SortedSet<ObservableTypeInfo> nstypes;
            if (!_namespacesDictionary.TryGetValue(ns, out nstypes))
            {
                nstypes = new SortedSet<ObservableTypeInfo>(ObservableTypeInfoComparer.Default);
                _namespacesDictionary.Add(ns, nstypes);
            }
            ObservableTypeInfo[] observableTypeArray =
                nstypes.Where(observableTypeInfo => observableTypeInfo.Name == extendedType.Name).ToArray();
            if (observableTypeArray.Length == 0)
            {
                ObservableTypeInfo newObservbleTypeInfo = new ObservableTypeInfo(extendedType.GetTypeInfo(), makeEmpty: true);
                nstypes.Add(newObservbleTypeInfo);
                newObservbleTypeInfo.InjectMethod(new ObservableMethodInfo(extensionMethodInfo));
            }
            else
            {
                ObservableTypeInfo observableTypeInfo = observableTypeArray[0];
                observableTypeInfo.InjectMethod(new ObservableMethodInfo(extensionMethodInfo));
            }
        }
    }
}
