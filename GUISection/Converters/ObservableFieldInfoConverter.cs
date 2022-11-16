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

[ValueConversion(typeof(ObservableFieldInfo), typeof(string))]
public class ObservableFieldInfoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ObservableFieldInfo observableFieldInfo = (ObservableFieldInfo)value;
        StringBuilder sb = new StringBuilder();
        sb.Append(AccessModifierToStringConverter.ConvertStatic(observableFieldInfo.Modifier)).Append(" ")
           .Append((observableFieldInfo.IsStatic) ? "static" : string.Empty).Append(" ")
           .Append((observableFieldInfo.IsReadOnly) ? "readonly" : string.Empty).Append(" ")
           .Append(observableFieldInfo.TypeInfo.GetParametrizedArgumentName()).Append(" ")
           .Append(observableFieldInfo.Name);
        return sb.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}