using System.Reflection;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Structures;

namespace University.DotnetLabs.Lab3.AssemblyBrowserLibrary.Tests;

[TestClass]
public class AssemblyBrowserTests
{
    private AssemblyBrowser AssemblyBrowser { get; set; }
    
    [TestInitialize]
    public void AssemblyInitialization()
    { 
        AssemblyBrowser = new AssemblyBrowser();
    }

    [TestMethod]
    public void TestExtensions()
    {
        Assembly assembly = Assembly.LoadFrom("Files/ExtensionTest.dll");
        AssemblyBrowser.BrowsedAssembly = assembly;
        AssemblyStructure structure = AssemblyBrowser.Demonstration!;
        ObservableNamespaceInfo ns = structure.InternalNamespaces.Where(observableNamespace => observableNamespace.Name == "System").First();
        ObservableTypeInfo stringObservableType = ns.InternalTypes.Where(type => type.Name == typeof(string).Name).First();
        Assert.AreEqual(stringObservableType.Methods.Count, 2);
    }

    [TestMethod]
    public void TestName() 
    {
        Assembly assembly = Assembly.LoadFrom("Files/ExtensionTest.dll");
        AssemblyBrowser.BrowsedAssembly = assembly;
        Assert.AreEqual(AssemblyBrowser.Demonstration!.AssemblyName.Name, assembly.GetName().Name);
    }

    [TestMethod]
    public void TestNamespaces()
    {
        Assembly assembly = Assembly.LoadFrom("Files/FakerLibrary.dll");
        AssemblyBrowser.BrowsedAssembly = assembly;
        Assert.AreEqual(AssemblyBrowser.Demonstration!.InternalNamespaces.Count, 5);
    }

}