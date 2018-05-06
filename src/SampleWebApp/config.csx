using System.Reflection;
using System.Diagnostics;
 
Add("value1", "hi");
Add("value2", "world");
Add("value3:nested", "yay!");

Add("klr", Process.GetCurrentProcess().MainModule.FileName);
Add("machineName", Environment.MachineName);