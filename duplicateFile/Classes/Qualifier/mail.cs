
using System.IO;
using System.Linq;

namespace duplicateFile.Classes.Qualifier
{
    public class Mail : Qualifier, IQualifier
    {
        public override string Name
        {
            get
            {
                return duplicateFile.Properties.Resources.MailQualifierTitle;
            }

        }

        public override string ColumnName
        {
            get
            {
                return "mail";
            }

        }

        public override int Note(string txt, FileInfo fi)
        {
            var patern = @"[[a-zA-Z0-9_]{1,50}(\.[a-zA-Z0-9_]+)*\@[a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*\.[a-zA-Z]{2,4}\b";
            // @"[^\W]?[[a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*\@[a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*\.[a-zA-Z]{2,4}\b";

            if (txt.IndexOf('@') == -1) return 0;

            return
                (int)((((float)SearchMethod.SearchRegExp(txt, patern) * 10) / (float)txt.Length) * 400);

        }

        public void save(string txt)
        {
            var s = new StreamWriter(@"c:\txt.txt");

            s.Write(txt);
            s.Close();
        }



    }
}
