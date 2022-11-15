using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary;

namespace University.DotnetLabs.Lab3.GUISection;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() is true)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(openFileDialog.FileName);
                AssemblyBrowser assemblyBrowser = (AssemblyBrowser)Application.Current.Resources["MainAssemblyBrowser"];
                assemblyBrowser.BrowsedAssembly = assembly;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace?.Normalize());
            }
        }
    }
}
