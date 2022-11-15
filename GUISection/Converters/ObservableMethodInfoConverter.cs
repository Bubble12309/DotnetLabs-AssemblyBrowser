using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Windows;

namespace University.DotnetLabs.Lab3.GUISection.Converters;

[ValueConversion(typeof(ObservableMethodInfo), typeof(string))]
public class ObservableMethodInfoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ObservableMethodInfo info = (ObservableMethodInfo)value;
        StringBuilder sb = new StringBuilder();
        if (info.IsExtensionMethod)
            sb.Append("(extended) ");
        sb.Append(AccessModifierToStringConverter.ConvertStatic(info.Modifier)).Append(" ");
        if (info.IsStatic && !info.IsExtensionMethod)
            sb.Append("static ");
        else 
        {
            if (info.IsAbstract)
                sb.Append("abstract ");
            else 
            {
                if (info.IsVirtual)
                    sb.Append("virtual ");
                if (info.IsFinal)
                    sb.Append("final ");
            }
        }
        if (!info.IsConstructor)
            sb.Append($"{info.ReturnTypeInfo.Name} ");
        sb.Append(info.MethodName).Append("(");
        for (int i = 0; i < info.ParametersInfo.Length - 1; i++)
        {
            ParameterInfo pi = info.ParametersInfo[i];
            if (pi.IsIn)
                sb.Append("in ");
            if (pi.IsOut)
                sb.Append("out ");
            if (pi.ParameterType.IsByRef)
                sb.Append("ref ");
            sb.Append($"{pi.ParameterType.Name} {pi.Name}");
            if (pi.HasDefaultValue)
                sb.Append($" = {pi.DefaultValue}");
            sb.Append(", ");
        }
        if (info.ParametersInfo.Length > 0)
        {
            ParameterInfo pi = info.ParametersInfo[info.ParametersInfo.Length - 1];
            if (pi.IsIn)
                sb.Append("in ");
            else if (pi.IsOut)
                sb.Append("out ");
            else if (pi.ParameterType.IsByRef)
                sb.Append("ref ");
            sb.Append($"{pi.ParameterType.Name} {pi.Name}");
            if (pi.HasDefaultValue)
                sb.Append($" = {pi.DefaultValue}");
        }
        sb.Append(")");
        return sb.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}