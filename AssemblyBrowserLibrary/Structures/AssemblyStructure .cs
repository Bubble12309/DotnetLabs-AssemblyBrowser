using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public sealed class AssemblyStructure
{
    public AssemblyName Name { get; private set; }
    public IReadOnlyCollection<TypeInfo> NonNamespaceTypes { get; private set; }
    public IReadOnlyCollection<NamespaceInfo> InternalNamespaces { get; private set; }
    internal AssemblyStructure(Assembly assembly)
    {
        IDictionary<string, SortedSet<TypeInfo>> ditionary = new Dictionary<string, SortedSet<TypeInfo>>();
        SortedSet<TypeInfo> nonNamespaceTypes = new(TypeInfoNameComparer.Default);
        SortedSet<NamespaceInfo> namespaces = new(NamespaceInfoComparer.Default);

        Name = assembly.GetName();
        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
            string? typeNamespace = type.Namespace;
            if (typeNamespace is null)
            {
                nonNamespaceTypes.Add(type.GetTypeInfo());
            }
            else
            {
                SortedSet<TypeInfo> namespaceTypes;
                if (!ditionary.TryGetValue(typeNamespace, out namespaceTypes!))
                {
                    namespaceTypes = new SortedSet<TypeInfo>(TypeInfoNameComparer.Default);
                    ditionary.Add(typeNamespace, namespaceTypes);
                }
                namespaceTypes.Add(type.GetTypeInfo());
            }
        }

        NonNamespaceTypes = nonNamespaceTypes.ToImmutableSortedSet<TypeInfo>(TypeInfoNameComparer.Default);
        foreach (KeyValuePair<string, SortedSet<TypeInfo>> keyValuePair in ditionary)
        {
            NamespaceInfo namespaceInfo = new NamespaceInfo(keyValuePair.Key);
            namespaceInfo.InternalTypes = keyValuePair.Value.ToImmutableSortedSet<TypeInfo>(TypeInfoNameComparer.Default);
            namespaces.Add(namespaceInfo);
        }
        InternalNamespaces = namespaces.ToImmutableSortedSet<NamespaceInfo>(NamespaceInfoComparer.Default);
    }
}