
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace duplicateFile.Classes.Qualifier
{
    public class Address : Qualifier, IQualifier
    {
        private string[][] refs;
        public override string Name
        {
            get
            {
                return "Recherche d'adresse";
            }

        }

        public override string ColumnName
        {
            get
            {
                return "Adr";
            }

        }

        public Address()
        {
            init();
        }

        public override int Note(string txt, FileInfo fi)
        {
            List<SearchResult> results = new List<SearchResult>();
            return (int)(((float)SearchMethod.Search4(txt, refs, false, 1) / (float)txt.Length) * 500);
            //return SearchMethod.Search4(txt, refs, false, 1);
        }

        private void init()
        {
            SearchTools.Init();

            string[] srcRef = File.ReadAllLines(@"refs\voie.ref");
            srcRef = SearchTools.ArrayToLower(srcRef);

            refs = SearchTools.buildArray(srcRef, 1);
        }




    }
}
