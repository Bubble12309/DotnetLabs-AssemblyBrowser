using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Comparers;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public sealed class AssemblyStructure
{
    public AssemblyName AssemblyName { get; private set; }
    private ObservableCollection<ObservableNamespaceInfo> _internalNamespaces;
    public ReadOnlyObservableCollection<ObservableNamespaceInfo> InternalNamespaces
    {
        get
        {
            return new(_internalNamespaces);
        }
    }
    internal AssemblyStructure(Assembly assembly)
    {
        Dictionary<string, SortedSet<ObservableTypeInfo>> dictionary = new Dictionary<string, SortedSet<ObservableTypeInfo>>();
        SortedSet<ObservableTypeInfo> nonNamespaceTypes = new(ObservableTypeInfoComparer.Default);
        SortedSet<ObservableNamespaceInfo> namespaces = new(ObservableNamespaceInfoComparer.Default);

        ExtensionsEncounter extensionsEncounter = new(nonNamespaceTypes, namespaces, dictionary);

        AssemblyName = assembly.GetName();
        Type[] types = assembly.GetTypes();
        var extensionTypes = types.Where(type => type.IsDefined(typeof(ExtensionAttribute)));
        var normalTypes = types.Except(extensionTypes);
        
        foreach (Type type in normalTypes)
        {
            string? namespaceOfType = type.Namespace;
            
            if (namespaceOfType is null)
            {
                nonNamespaceTypes.Add(new ObservableTypeInfo(type.GetTypeInfo()));
            }
            else
            {
                SortedSet<ObservableTypeInfo> typesWithinNamespace;
                if (!dictionary.TryGetValue(namespaceOfType, out typesWithinNamespace!))
                {
                    typesWithinNamespace = new SortedSet<ObservableTypeInfo>(ObservableTypeInfoComparer.Default);
                    dictionary.Add(namespaceOfType, typesWithinNamespace);
                }
                typesWithinNamespace.Add(new ObservableTypeInfo(type.GetTypeInfo()));
            }
        }

        foreach (Type type in extensionTypes)
        { 
            var extensionMethods = type.GetMethods().Where(method => method.IsDefined(typeof(ExtensionAttribute)));
            foreach (MethodInfo extensionMethod in extensionMethods)
            {
                extensionsEncounter.Encount(extensionMethod);
            }
        }
        
        foreach (KeyValuePair<string, SortedSet<ObservableTypeInfo>> keyValuePair in dictionary)
        {
            ObservableNamespaceInfo namespaceInfo = new ObservableNamespaceInfo(keyValuePair.Key);
            namespaceInfo.InternalTypes = new(new(keyValuePair.Value));
            namespaces.Add(namespaceInfo);
        }

        ObservableCollection<ObservableNamespaceInfo> observableNamespaces = new(namespaces);
        if (nonNamespaceTypes.Count > 0)
        {
            ObservableNamespaceInfo nullNamespace = new ObservableNamespaceInfo("");
            ReadOnlyObservableCollection<ObservableTypeInfo> roNonObservableTypes = new(new(nonNamespaceTypes));
            nullNamespace.InternalTypes = roNonObservableTypes;
            observableNamespaces.Prepend(nullNamespace);
        }
        _internalNamespaces = new(observableNamespaces);
    }
}