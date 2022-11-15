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

[ValueConversion(typeof(TypeKind), typeof(string))]
public class TypeKindToStringConverter : IValueConverter
{
    public static string ConvertStatic(TypeKind tk)
    {
        switch (tk)
        {
            case TypeKind.Class: return "class";
            case TypeKind.Interface: return "interface";
            case TypeKind.Struct: return "struct";
            case TypeKind.RefStruct: return "ref struct";
            case TypeKind.Enum: return "enum";
        }
        return string.Empty;
    }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        TypeKind tk = (TypeKind)value;
        return ConvertStatic(tk);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
