using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Configuration;


namespace Tabloid.Classes.Data
{
    [DataObject(true)]
    public static class TabloidDataSource
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable SelectData(string sql, Int32 maximumRows, Int32 startRowIndex)
        {
            string error;
            var dt = DataTools.Data(
                sql,
                WebConfigurationManager.ConnectionStrings["TabloidConnection"].ConnectionString,
                out error);

            if (dt == null) throw new Exception(error); //generateException(sql, sqlTools.lastError);

            return dt;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Int32 TotalRowCount(string sql)
        {
            int result;

            string error;
            var dt = DataTools.Data(
                sql,
                WebConfigurationManager.ConnectionStrings["TabloidConnection"].ConnectionString,
                out error);

            if (dt != null) result = Convert.ToInt32(dt.Rows[0][0]);
            else throw new Exception(Ressources.FR.TabloidDataSource_TotalRowCount_Erreur_sur_l_execution_du_Select_la_requête_était__ + sql);

            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static bool UpDateData(object sqlSet)
        {
            string error;
            var r = DataTools.Command(
                ((SqLcommandSet)sqlSet).Update.Command,
                ((SqLcommandSet)sqlSet).Update.Parameteredfields.Values.ToList(),
                WebConfigurationManager.ConnectionStrings["TabloidConnection"].ConnectionString,
                out error);

            if (r == -1) throw new Exception(error);
            return true;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static bool InsertData(object sqlSet, object keysNames, out object outKeysValues)
        {
            string error;
            var sql = ((SqLcommandSet)sqlSet).Insert.Command +
                      string.Format(
                          SqlConverter.GetSql(SqlConverter.SqlType.InsertCommandKeyOut),
                          ((string[])keysNames)[0]);
            var id = DataTools.ScalarCommand(
                sql,
                ((SqLcommandSet)sqlSet).Insert.Parameteredfields.Values.ToList(),
                WebConfigurationManager.ConnectionStrings["TabloidConnection"].ConnectionString,
                out error);

            if (id is bool) id = null;
            if (id == null) throw new Exception(error);

            outKeysValues = new[] { id.ToString() };
            return true;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static bool DeleteData()
        {
            return true;
        }
    }
}