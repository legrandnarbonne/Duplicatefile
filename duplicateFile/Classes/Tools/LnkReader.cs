using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFile.Classes.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class LnkReader
    {
        public static string GetShortcutTarget(string file)
        {
            try
            {
                if (System.IO.File.Exists(file))
                {
                    // WshShellClass shell = new WshShellClass();
                    WshShell shell = new WshShell(); //Create a new WshShell Interface
                    IWshShortcut link = (IWshShortcut)shell.CreateShortcut(file); //Link the interface to our shortcut
                    return link.TargetPath; //Show the target in a MessageBox using IWshShortcut
                }

                return null;
            }

            catch
            {
                return null;
            }
        }
    }
}
