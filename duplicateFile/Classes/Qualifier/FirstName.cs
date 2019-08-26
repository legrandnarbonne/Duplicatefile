using System.IO;
using System.Collections.Generic;

namespace duplicateFile.Classes.Qualifier
{
    public class FirstName : Qualifier,IQualifier
    {
        private string[][] refs;

        public override string Name
        {
            get
            {
                return duplicateFile.Properties.Resources.QFirstNameName;
            }
            
        }
        public override string ColumnName
        {
            get
            {
                return "prenom";
            }

        }

        public FirstName()
        {
            init();
        }

        public override int Note(string txt, FileInfo fi)
        {
            List<SearchResult> results = new List<SearchResult>();
            return (int)(((float)SearchMethod.Search4(txt, refs, false, 1) / (float)txt.Length) * 400);
            //return SearchMethod.Search4(txt, refs, false, 1);
        }

        private void init()
        {
            SearchTools.Init();

            string[] srcRef = File.ReadAllLines(@"refs\prenom.ref");
            srcRef = SearchTools.ArrayToLower(srcRef);

            refs = SearchTools.buildArray(srcRef, 1);            
        }
    }
}
