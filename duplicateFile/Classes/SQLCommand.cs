using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.Configuration;
using Tabloid.Classes.Tools;

namespace Tabloid.Classes.Data
{
    public class SqLcommandSet
    {
        public SqLcommand Delete;
        public SqLcommand Insert;
        public SqLcommand Select;
        public SqLcommand Update;
        public SqLcommand Function;

        public SqLcommandSet()
        {
        }

        /// <summary>
        ///     clone from other SQLcommandSet
        /// </summary>
        /// <param name="source">SQLcommandSet to clone</param>
        public SqLcommandSet(SqLcommandSet source)
        {
            Select = new SqLcommand(source.Select);
            Delete = new SqLcommand(source.Delete);
            Insert = new SqLcommand(source.Insert);
            Update = new SqLcommand(source.Update);
        }

        /// <summary>
        ///     Set table name in insert,update,delete comand
        /// </summary>
        /// <param name="name"></param>
        public void SetTableName(string name)
        {
            Insert.TableName = Update.TableName = Delete.TableName = name;
        }
    }

    public class SqLcommand
    {
        public enum SqLcommandType
        {
            Select,
            Update,
            Insert,
            Delete,
            Function
        }

        private bool _fieldManualBuild;
        private List<String> _fields = new List<String>();
        private string _from;
        public string Body;
        public string Distinct;
        public List<String> Fields = new List<String>();
        public string From;
        public string GroupBy;
        public int? Limit;
        public int? Offset;
        public string OrderBy;
        public string geomField;
        public string Srid;

        public Dictionary<string, DbParameter> Parameteredfields = new Dictionary<string, DbParameter>();
        //could be string to use directly or Parameter

        public Dictionary<string, SqlJoinParameter> SqlJoinParameters = new Dictionary<string, SqlJoinParameter>();
        public string TableName;
        public SqLcommandType Type;
        public string Where;

        public SqLcommand(SqLcommandType t)
        {
            Type = t;
        }

        /// <summary>
        ///     clone from other SQLCommand
        /// </summary>
        /// <param name="source">SQLCommand to clone</param>
        public SqLcommand(SqLcommand source)
        {
            Type = source.Type;
            Fields = new List<String>(source.Fields);
            //source.fields.Select(item => (string)item.Clone()).ToList();//clone field list
            SqlJoinParameters = new Dictionary<string, SqlJoinParameter>(source.SqlJoinParameters);

            Body = source.Body;
            Limit = source.Limit;
            Offset = source.Offset;
            Distinct = source.Distinct;
            Where = source.Where;
            OrderBy = source.OrderBy;
            GroupBy = source.GroupBy;
        }

        public string Command
        {
            get
            {
                switch (Type)
                {
                    case SqLcommandType.Select:
                        return GenSelectCommand(false);

                    case SqLcommandType.Insert:
                        return GenInsertCommand();

                    case SqLcommandType.Update:
                        return GenUpdateCommand();

                    case SqLcommandType.Delete:
                        return GenDeleteCommand();

                    case SqLcommandType.Function:
                        return GenFunctionCommand();
                }
                return null;
            }
        }

        public string CountCommand
        {
            get
            {
                if (Type == SqLcommandType.Select) return GenSelectCommand(true);
                return null;
            }
        }

        public void SetManualBuild()
        {
            Fields.Clear();
            _fieldManualBuild = true;
        }

        private string GenInsertCommand()
        {
            var paramIdentifier = WebConfigurationManager.AppSettings["identParam"];

            var result = SqLcommandType.Insert + " INTO " + TableName;
            result += "(" + string.Join(",", Parameteredfields.Keys) + ")";
            result += " VALUES (" + paramIdentifier + string.Join("," + paramIdentifier, Parameteredfields.Keys) + ")";
            result += addPart(Where, "Where");

            if (!string.IsNullOrEmpty(geomField)) 
                result=result.Replace(paramIdentifier + geomField,
                    string.Format("ST_GeomFromText({0},{1})", paramIdentifier + geomField, Srid));

            return result;
        }

        private string GenFunctionCommand()
        {
            var paramIdentifier = WebConfigurationManager.AppSettings["identParam"];

            var result = SqLcommandType.Select + " " + TableName;
            result += "(" + paramIdentifier + string.Join("," + paramIdentifier, Parameteredfields.Keys) + ")";
            return result;
        }

        private string GenUpdateCommand()
        {
            var result = SqLcommandType.Update + " " + TableName;
            result += " Set " + UpdateStatementFromFields();
            result += addPart(Where, "Where");

            return result;
        }

        private string GenDeleteCommand()
        {
            var result = SqLcommandType.Delete + " From " + TableName;
            result += addPart(Where, "Where");
            return result;
        }

        private string GenSelectCommand(bool count)
        {
            var result = ""; // body;

            _from = From;

            SetFromSqlJoinParameter();

            if (!string.IsNullOrEmpty(Distinct))
                result = SqlConverter.GetSql(SqlConverter.SqlType.Distinct, new string[] { Distinct }) + " ";

            //

            //var countField = string.IsNullOrEmpty(Distinct) ? "*" : Distinct;
            //result += count
            //    ? "Count(" + countField + ")"
            //    : string.Join(",", _fields.FindAll(s => !String.IsNullOrEmpty(s)));

            result += string.Join(",", _fields.FindAll(s => !String.IsNullOrEmpty(s)));
            result = SqLcommandType.Select + " " + result;

            result += addPart(_from, "From");
            result += addPart(Where, "Where");
            result += addPart(GroupBy, "Group By");

            if (!count)
            {
                result += addPart(OrderBy, "Order By");
                result += addPart(Limit, "Limit");
                result += addPart(Offset, "Offset");
            }

            if (count) result = "SELECT COUNT(*) FROM (" + result + ") AS temp;";

            return result;
        }

        private void SetFromSqlJoinParameter()
        {
            _fields = new List<String>();
            _fields.AddRange(Fields);

            _from = From;

            foreach (var sqlj in SqlJoinParameters.Values.OrderBy(i => i.Level))
            {
                if (!_fieldManualBuild) _fields.AddRange(sqlj.SelectField);
                _from = sqlj.JoinPart + _from + sqlj.OnPart;
            }
        }

        private string UpdateStatementFromFields()
        {
            var result = "";
            var paramIdentifier = WebConfigurationManager.AppSettings["identParam"];
            var sep = "";

            foreach (var o in Parameteredfields.Values)
            {
                result += sep + o.ParameterName + "=" + paramIdentifier + o.ParameterName;
                sep = ",";
            }


            if (!string.IsNullOrEmpty(geomField))
                result = result.Replace(paramIdentifier + geomField,
                    string.Format("ST_GeomFromText({0},{1})", paramIdentifier + geomField, Srid));

            return result;
        }

        /// <summary>
        ///     Set properties from sql string
        /// </summary>
        /// <param name="cmd"></param>
        public void SetFromString(string cmd)
        {
            cmd = cmd.ToLower();
            var posWhere = cmd.IndexOf("where", StringComparison.Ordinal);
            var posOrder = cmd.IndexOf("order by", StringComparison.Ordinal);
            var posGroupe = cmd.IndexOf("group by", StringComparison.Ordinal);
            //Define corps
            var posW = Common.FirstString(new[] { posWhere, posOrder, posGroupe }) - 1; //find next part
            Body = cmd.Substring(0, posW).Trim();
            //Define where
            posW = Common.FirstString(new[] { posOrder, posGroupe }) - 1; //find next part
            if (posW > -1 && posWhere > -1) Where = cmd.Substring(posWhere, posW);
            //Define order
            if (posOrder != -1)
            {
                var fin = Math.Max(cmd.Length, posGroupe);
                OrderBy = posOrder == posW ? cmd.Substring(posW + 9, fin - posW) : cmd.Substring(posOrder + 9, fin - posOrder - 9);
            }
            //Define group
            if (posGroupe != -1)
            {
                var fin = Math.Max(cmd.Length, posOrder);
                GroupBy = posOrder == posW ? cmd.Substring(posW + 9, fin - posGroupe - 9) : cmd.Substring(posGroupe + 9, fin - posW - 9);
            }
        }

        /// <summary>
        ///     SQL Builder add part if not null return empty string if part is null or concatened name and part string
        /// </summary>
        /// <param name="part">command value</param>
        /// <param name="name">command</param>
        /// <returns></returns>
        private string addPart(string part, string name)
        {
            return string.IsNullOrEmpty(part) ? "" : " " + name + " " + part;
        }

        /// <summary>
        ///     SQL Builder add part if not null return empty string if part is null or concatened name and part string
        /// </summary>
        /// <param name="part">int command value</param>
        /// <param name="name">command</param>
        /// <returns></returns>
        private string addPart(int? part, string name)
        {
            return part == null ? "" : " " + name + " " + part;
        }

        //define part to add for a join
        public class SqlJoinParameter
        {
            public string JoinPart;
            public int Level;
            public string OnPart;
            public List<String> SelectField = new List<String>();
        }
    }
}