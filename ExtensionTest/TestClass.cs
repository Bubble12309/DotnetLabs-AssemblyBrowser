using System.Runtime.CompilerServices;
using University.DotnetLabs.Lab3.AssemblyBrowserLibrary;

namespace ExtensionTest
{
    public static class TestClass
    {
        public static bool ExtMethod(this string a, int b, int c, double e = 2.5)
        {
            return true;
        }

        public static bool Ext2Method(this AssemblyBrowser a, double c, decimal e)
        { 
            return false;
        }

        public static bool Ext4Method(this string a, int b, int c, double e, FileSystemEventHandler fseh)
        {
            return true;
        }
    }
}