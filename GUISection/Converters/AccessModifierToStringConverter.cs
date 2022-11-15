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

[ValueConversion(typeof(AccessModifier), typeof(string))]
public class AccessModifierToStringConverter : IValueConverter
{
    public static string ConvertStatic(AccessModifier am)
    {
        switch (am)
        {
            case AccessModifier.Private: return "private";
            case AccessModifier.Public: return  "public";
            case AccessModifier.Protected: return  "protected";
            case AccessModifier.PrivateProtected: return  "private protected";
            case AccessModifier.ProtectedInternal: return  "protected internal";
            case AccessModifier.Internal: return "internal";
        }
        return string.Empty;
    }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        AccessModifier accessModifier = (AccessModifier)value;
        return ConvertStatic(accessModifier);   
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
       return DependencyProperty.UnsetValue;
    }
}
