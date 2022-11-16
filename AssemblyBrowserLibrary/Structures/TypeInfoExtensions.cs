using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public static class TypeInfoExtensions
{
    public static string GetParametrizedArgumentName(this TypeInfo ti)
    { 
        StringBuilder result = new StringBuilder();
        result.Append(ti.Name);
        if (!ti.IsGenericType)
            return result.ToString();
        result.Append("<");
        Type[] typeArguments = ti.GenericTypeArguments;
        for (int i = 0; i < typeArguments.Length - 1; i++)
        {
            Type t = typeArguments[i];
            result.Append(GetParametrizedArgumentName(t.GetTypeInfo())).Append(", ");
        }
        if (typeArguments.Length > 0)
        {
            Type t = typeArguments[typeArguments.Length - 1];
            result.Append(GetParametrizedArgumentName(t.GetTypeInfo()));
        }
        result.Append(">");
        return result.ToString();
    }

    public static string GetParametrizedTypeName(this TypeInfo ti)
    {
        StringBuilder result = new StringBuilder();
        result.Append(ti.Name);
        if (!ti.IsGenericType)
            return result.ToString();
        result.Append("<");
        Type[] typeArguments = ti.GenericTypeParameters;
        for (int i = 0; i < typeArguments.Length - 1; i++)
        {
            Type t = typeArguments[i];
            result.Append(GetParametrizedTypeName(t.GetTypeInfo())).Append(", ");
        }
        if (typeArguments.Length > 0)
        {
            Type t = typeArguments[typeArguments.Length - 1];
            result.Append(GetParametrizedTypeName(t.GetTypeInfo()));
        }
        result.Append(">");
        return result.ToString();
    }
}
