using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

public class ObservableMethodInfo
{
    public string MethodName { get; }
    public bool IsStatic { get; }
    public bool IsConstructor { get; }
    public AccessModifier Modifier { get; }
    public bool IsExtensionMethod { get; }
    public bool IsAbstract { get; }
    public bool IsVirtual { get; }
    public bool IsFinal { get; }

    public bool IsGenericMethod { get; }
    public TypeInfo[]? GenericParameters { get; }
    public ParameterInfo[] ParametersInfo { get; internal set; }
    public TypeInfo ReturnTypeInfo { get; }

    internal ObservableMethodInfo(MethodInfo methodInfo)
    {
        MethodName = methodInfo.Name;
        IsStatic = methodInfo.IsStatic;
        IsConstructor = methodInfo.IsConstructor;
        ReturnTypeInfo = methodInfo.ReturnType.GetTypeInfo();
        IsExtensionMethod = methodInfo.IsDefined(typeof(ExtensionAttribute));
        ParametersInfo = methodInfo.GetParameters();
        IsGenericMethod = methodInfo.IsGenericMethod;

        if (methodInfo.IsPrivate)
        {
            Modifier = AccessModifier.Private;
        }
        else if (methodInfo.IsFamily)
        {
            Modifier = AccessModifier.Protected;
        }
        else if (methodInfo.IsFamilyOrAssembly)
        {
            Modifier = AccessModifier.ProtectedInternal;
        }
        else if (methodInfo.IsFamilyAndAssembly)
        {
            Modifier = AccessModifier.PrivateProtected;
        }
        else if (methodInfo.IsAssembly)
        {
            Modifier = AccessModifier.Internal;
        }
        else if (methodInfo.IsPublic)
        {
            Modifier = AccessModifier.Public;
        }

        if (methodInfo.IsGenericMethodDefinition)
        {
            Type[] types = methodInfo.GetGenericArguments();
            TypeInfo[] typesInfo = new TypeInfo[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                typesInfo[i] = types[i].GetTypeInfo();
            }
            GenericParameters = typesInfo;
        }

        if (IsExtensionMethod)
        {
            if (ParametersInfo.Length != 0)
            {
                int newLength = ParametersInfo.Length - 1;
                ParameterInfo[] temp = new ParameterInfo[newLength];
                Array.Copy(ParametersInfo, 1, temp, 0, newLength);
                ParametersInfo = temp;
            }
        }
    }

}
