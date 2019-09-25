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
            string addressHKLM = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string addressHKCU = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string addressHK32 = @"SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION";
            List<string> addr = new List<string>() { addressHKLM, addressHK32, addressHKCU };
            int value = 11000;
            int count = 2;
            RegistryHive reg = RegistryHive.LocalMachine;
            try
            {
                foreach (string HK in addr)
                {
                    if (count++ > 2)
                        reg = RegistryHive.CurrentUser;

                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(reg, RegistryView.Registry64))
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(HK, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                        {
                            if (key != null)
                            {
                                string keyValue = key.GetValue("test.exe", "Not Found").ToString();

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
            }
            catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
            {
                TestExec.InstructionMessage("The Following Exception Occurred: General Exception.");
            }
        }
    }
}
