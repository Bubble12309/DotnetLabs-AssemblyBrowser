using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Comparers;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public sealed class ObservableTypeInfo
{
    public string Name { get; internal set; }
    public string? Namespace { get; internal set; }
    public AccessModifier Modifier { get; internal set; }
    public TypeKind Kind { get; internal set; }
    public bool IsAbstract { get; internal set; }

    internal ObservableCollection<ObservableTypeInfo> _internalTypes;
    public ReadOnlyObservableCollection<ObservableTypeInfo> InternalTypes
    {
        get
        {
            return new(_internalTypes);
        }
    }

    internal ObservableCollection<ObservableMethodInfo> _methods;
    public ReadOnlyObservableCollection<ObservableMethodInfo> Methods
    {
        get
        {
            return new(_methods);
        }
    }

    internal ObservableCollection<ObservableFieldInfo> _fields;
    public ReadOnlyObservableCollection<ObservableFieldInfo> Fields
    {
        get
        {
            return new(_fields);
        }
    }

    internal ObservableCollection<ObservablePropertyInfo> _properties;
    public ReadOnlyObservableCollection<ObservablePropertyInfo> Properties
    {
        get
        {
            return new(_properties);
        }
    }

    internal void InjectMethod(ObservableMethodInfo observableMethodInfo)
    {
        _methods.Add(observableMethodInfo);
    }

    internal ObservableTypeInfo(TypeInfo typeInfo, bool makeEmpty = false)
    {
        SortedSet<ObservableTypeInfo> typesCollection = new(ObservableTypeInfoComparer.Default);
        SortedSet<ObservableMethodInfo> methodsCollection = new(ObservableMethodInfoComparer.Default);
        SortedSet<ObservableFieldInfo> fieldsCollection = new(ObservableFieldInfoComparer.Default);
        SortedSet<ObservablePropertyInfo> propertiesCollection = new(ObservablePropertyInfoComparer.Default);

        Name = typeInfo.GetParametrizedTypeName();
        Namespace = typeInfo.Namespace;
        IsAbstract = typeInfo.IsAbstract;
        if (typeInfo.IsClass)
            Kind = TypeKind.Class;
        else if (typeInfo.IsEnum)
            Kind = TypeKind.Enum;
        else if (typeInfo.IsInterface)
            Kind = TypeKind.Interface;
        else if (typeInfo.IsValueType)
        {
            if (typeInfo.IsByRefLike)
                Kind = TypeKind.RefStruct;
            else
                Kind = TypeKind.Struct;
        }

        if (typeInfo.IsPublic)
            Modifier = AccessModifier.Public;
        else if (typeInfo.IsNotPublic)
            Modifier = AccessModifier.Internal;

        if (makeEmpty is not true)
        {
            foreach (Type type in typeInfo.GetNestedTypes())
            {
                typesCollection.Add(new ObservableTypeInfo(type.GetTypeInfo()));
            }
        }
        _internalTypes = new(typesCollection);

        if (makeEmpty is not true)
        {
            foreach (MethodInfo methodInfo in typeInfo.DeclaredMethods)
            {
                methodsCollection.Add(new ObservableMethodInfo(methodInfo));
            }
        }
        _methods = new(methodsCollection);

        if (makeEmpty is not true)
        {
            foreach (FieldInfo fieldInfo in typeInfo.DeclaredFields)
            {
                fieldsCollection.Add(new ObservableFieldInfo(fieldInfo));
            }
        }
        _fields = new(fieldsCollection);
        if (makeEmpty is not true)
        {
            foreach (PropertyInfo propertyInfo in typeInfo.DeclaredProperties)
            {
                string propertyName = propertyInfo.Name;
                ObservableMethodInfo[] compiliedGetter = _methods.Where(m => m.MethodName == ("get_" + propertyInfo.Name)).ToArray();
                ObservableMethodInfo[] compiliedSetter = _methods.Where(m => m.MethodName == ("set_" + propertyInfo.Name)).ToArray();
                AccessModifier? getterModifier = (compiliedGetter.Length > 0) ? compiliedGetter[0].Modifier : null;
                AccessModifier? setterModifier = (compiliedSetter.Length > 0) ? compiliedSetter[0].Modifier : null;
                propertiesCollection.Add(new ObservablePropertyInfo(propertyInfo, getterModifier, setterModifier));
            }
        }
        _properties = new(propertiesCollection);
    }
}