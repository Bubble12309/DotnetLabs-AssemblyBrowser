using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public class ObservablePropertyInfo
{
    public string Name { get; }
    public TypeInfo? TypeInfo { get; }
    public AccessModifier? GetterAccessModifier { get; }
    public AccessModifier? SetterAccessModifier { get; }
    internal ObservablePropertyInfo(PropertyInfo propertyInfo, AccessModifier? getterAccessModifier, AccessModifier? setterAccessModifier)
    {
        Name = propertyInfo.Name;
        TypeInfo = propertyInfo.PropertyType.GetTypeInfo();
        GetterAccessModifier = getterAccessModifier;
        SetterAccessModifier = setterAccessModifier;   
    }
}
