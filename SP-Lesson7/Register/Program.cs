using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    class Program
    {
        static void Main(string[] args)
        {
            string regKeyName = @"SOFTWARE\Adobe\Adobe Acrobat\11.0\AVGeneral";
            RegistryKey key =
              Registry.CurrentUser.OpenSubKey(regKeyName);
            string[] subKeyNames = key.GetSubKeyNames();
            string[] valNames = key.GetValueNames();
            Console.WriteLine("Subkey Names:");
            for (int i = 0; i < subKeyNames.Length; i++)
                Console.WriteLine(" {0}: {1}", i, subKeyNames[i]);
            Console.WriteLine("Value Names:");
            for (int i = 0; i < valNames.Length; i++)
                Console.WriteLine(" {0}: {1}", i, valNames[i]);
        }
    }
}
