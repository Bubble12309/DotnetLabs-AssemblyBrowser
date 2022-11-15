using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

namespace University.DotnetLabs.Lab3.GUISection.ViewModel;

internal class MainWindowDataContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    { 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));  
    }

    private AssemblyBrowser _assemblyBrowser = (AssemblyBrowser)Application.Current.Resources["MainAssemblyBrowser"];

    public AssemblyBrowser Browser
    { 
        get 
        {
            return _assemblyBrowser;
        } 
    }

}
