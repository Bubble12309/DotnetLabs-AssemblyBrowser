using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public class ObservableFieldInfo
{
    public string Name { get; }
    public TypeInfo TypeInfo { get; }
    public AccessModifier Modifier { get; }
    public bool IsStatic { get; }
    public bool IsReadOnly { get; }
    internal ObservableFieldInfo(FieldInfo fieldInfo)
    { 
        Name = fieldInfo.Name;
        TypeInfo = fieldInfo.DeclaringType.GetTypeInfo();
        IsStatic = fieldInfo.IsStatic;
        IsReadOnly = fieldInfo.IsInitOnly;
        if (fieldInfo.IsPrivate)
            Modifier = AccessModifier.Private;
        else if (fieldInfo.IsFamily)
            Modifier = AccessModifier.Protected;
        else if (fieldInfo.IsFamilyOrAssembly)
            Modifier = AccessModifier.ProtectedInternal;
        else if (fieldInfo.IsFamilyAndAssembly)
            Modifier = AccessModifier.PrivateProtected;
        else if (fieldInfo.IsAssembly)
            Modifier = AccessModifier.Internal;
        else if (fieldInfo.IsPublic)
            Modifier = AccessModifier.Public;

    }
}
