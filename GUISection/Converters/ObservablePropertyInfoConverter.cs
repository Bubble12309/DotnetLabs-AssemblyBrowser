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

[ValueConversion(typeof(ObservablePropertyInfo), typeof(string))]
public class ObservablePropertyInfoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ObservablePropertyInfo observablePropertyInfo = (ObservablePropertyInfo)value;
        StringBuilder sb = new StringBuilder();
        sb.Append($"{observablePropertyInfo.TypeInfo?.Name} ").
           Append(observablePropertyInfo.Name).Append(" {").
           Append((observablePropertyInfo.GetterAccessModifier is not null) ?
                    AccessModifierToStringConverter.ConvertStatic((AccessModifier)observablePropertyInfo.GetterAccessModifier) + " get; " : string.Empty).
           Append((observablePropertyInfo.SetterAccessModifier is not null) ?
                    AccessModifierToStringConverter.ConvertStatic((AccessModifier)observablePropertyInfo.SetterAccessModifier) + " set; " : string.Empty).
           Append("}");
        return sb.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}