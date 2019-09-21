using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RegistryBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            int value = 11000;

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(address, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                    {
                        if (key != null)
                        {
                            string keyValue = key.GetValue("test.exe", "Not Found") .ToString();

                            //create a new key 
                            if (keyValue == "Not Found")
                            {
                                key.SetValue("test.exe", value, RegistryValueKind.DWord); 
                            }
                            else if (keyValue != value.ToString())
                            {
                                key.SetValue("test.exe", value, RegistryValueKind.DWord);  
                            }
                        }
                    }
                }
            }
            catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
            {
                //react appropriately
            }
        }
    }
}
