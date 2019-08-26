using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duplicateFile.Classes.Tools
{
    public static class ResourceHelper
    {
        /// <summary>
        /// Set DataGridView header text from localized resource file
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        public static void setHeaders(DataGridView dgv)
        {
            foreach(DataGridViewColumn c in dgv.Columns)
                            if (c.Visible) c.HeaderText = getResource(c.HeaderText);                
            
        }

        /// <summary>
        /// Return translated text from localized resource file
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string getResource(string txt)
        {
            var result= Resources.Languages.Resources.ResourceManager.GetString(txt);
            if (result != null) return result;

            return txt;
        }
    }
}
