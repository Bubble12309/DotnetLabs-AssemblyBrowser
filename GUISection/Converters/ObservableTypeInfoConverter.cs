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

[ValueConversion(typeof(ObservableTypeInfo), typeof(string))]
public class ObservableTypeInfoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ObservableTypeInfo info = (ObservableTypeInfo)value;
        StringBuilder sb = new StringBuilder();
        sb.Append(AccessModifierToStringConverter.ConvertStatic(info.Modifier)).Append(" ")
          .Append((info.Kind == TypeKind.Class && info.IsAbstract)?"abstract ":string.Empty)
          .Append(TypeKindToStringConverter.ConvertStatic(info.Kind)).Append(" ")
          .Append(info.Name);
        return sb.ToString();
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}