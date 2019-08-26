
using System.IO;
using System.Linq;

namespace duplicateFile.Classes.Qualifier
{
    public class Phone : Qualifier, IQualifier
    {
        public override string Name
        {
            get
            {
                return duplicateFile.Properties.Resources.PhoneQualifierName;
            }

        }

        public override string ColumnName
        {
            get
            {
                return "Tel";
            }

        }

        public override int Note(string txt, FileInfo fi)
        {
            var excel = new string[] { ".xlsx", "xlsm" };

            var patern = excel.Contains(fi.Extension) ?
            @"(?:\s)(?:(?:\+|00)33|0)?\s?[1-9](?:[\s.-]?\d{2}){4}\b" ://for excel in phone format there is no 0 recorded 
            @"(?:\s)(?:(?:\+|00)33|0)\s?[1-9](?:[\s.-]?\d{2}){4}\b";

            return
                (int)((((float)SearchMethod.SearchRegExp(txt, patern) * 10) / (float)txt.Length) * 400);
            //return SearchMethod.SearchRegExp(txt, patern);

        }

        public void save(string txt)
        {
            var s = new StreamWriter(@"c:\txt.txt");

            s.Write(txt);
            s.Close();
        }



    }
}
