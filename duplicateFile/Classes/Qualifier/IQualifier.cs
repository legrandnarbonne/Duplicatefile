
using System.IO;

namespace duplicateFile.Classes.Qualifier
{
    interface IQualifier
    {
        string Name { get;  }
        string ColumnName { get; }
        int Note(string txt,FileInfo fi);
    }
}
