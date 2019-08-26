using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataTools
{
    public static class DataTools
    {
        public static string DefaultProviderName;
        public static DbProviderFactory DefaultFactory;

        public static DbConnection SetConnection(string connectionString)
        {
            return SetConnection(connectionString, null);
        }

        private static DbConnection SetConnection(string connectionString, string providerName)
        {
            DbConnection conn = null;

            try
            {
                if (DefaultFactory == null) DefaultFactory = DbProviderFactories.GetFactory(DefaultProviderName);

                var factory =
                    !string.IsNullOrEmpty(providerName)
                        ? DbProviderFactories.GetFactory(providerName)
                        : DefaultFactory;

                conn = factory.CreateConnection();
                if (conn != null) conn.ConnectionString = connectionString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "\n" + providerName + "\n" + connectionString);
            }
            return conn;
        }

        public static DbDataAdapter getAdapter(string selectCommand, DbConnection provider)
        {
            try
            {
                provider.Open();
                var adapter = DefaultFactory.CreateDataAdapter();
                if (adapter != null)
                {
                    adapter.SelectCommand = DefaultFactory.CreateCommand();
                    if (adapter.SelectCommand != null)
                    {
                        adapter.SelectCommand.Connection = provider;
                        adapter.SelectCommand.CommandText = selectCommand;
                    }

                    var cmbBuilder = DefaultFactory.CreateCommandBuilder();

                    if (cmbBuilder != null)
                    {
                        cmbBuilder.DataAdapter = adapter;

                        adapter.DeleteCommand = cmbBuilder.GetDeleteCommand();
                        adapter.UpdateCommand = cmbBuilder.GetUpdateCommand();
                        adapter.InsertCommand = cmbBuilder.GetInsertCommand();
                    }
                }
                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + selectCommand);
                return null;
            }
            finally
            {
                if (provider.State == ConnectionState.Open) provider.Close();
            }
        }

        /// <summary>
        ///     retourne une table correstondant au select fourni
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="connectionString">Chaine de connection</param>
        /// <param name="lastError"></param>
        public static DataTable Data(string sql, string connectionString, DbDataAdapter adapter = null)
        {
            var conn = SetConnection(connectionString);
            //if (conn != null) return data(sql, conn, out _lastError);
            return conn != null ? Data(sql, conn, adapter) : null;
        }

        /// <summary>
        ///     retourne une table correstondant au select fourni
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="provider">Connecteur MySql</param>
        /// <param name="lastError"></param>
        public static DataTable Data(string sql, DbConnection provider, DbDataAdapter adapter = null)
        {
            var dt = new DataTable();

            try
            {
                provider.Open();
                using (adapter = DefaultFactory.CreateDataAdapter())
                {
                    if (adapter != null)
                    {
                        adapter.SelectCommand = DefaultFactory.CreateCommand();
                        if (adapter.SelectCommand != null)
                        {
                            adapter.SelectCommand.Connection = provider;
                            adapter.SelectCommand.CommandText = sql;
                        }
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + sql);
                return null;
            }
            finally
            {
                if (provider.State == ConnectionState.Open) provider.Close();
            }
            return dt;
        }

        /// <summary>
        ///     affecte une table dans un dataset
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="table">nom de la table</param>
        /// <param name="connectionString">chaine de connection</param>
        /// <param name="dset">dataset devant contenir la table</param>
        /// <param name="lastError"></param>
        public static void Data(string sql, string table, string connectionString, DataSet dset)
        {
            var conn = SetConnection(connectionString);
            if (conn != null)
            {
                Data(sql, table, conn, dset);
            }
        }

        /// <summary>
        ///     affecte une table dans un dataset
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="table">nom de la table</param>
        /// <param name="provider">Connecteur MySql</param>
        /// <param name="dset">dataset devant contenir la table</param>
        /// <param name="lastError"></param>
        public static void Data(string sql, string table, DbConnection provider, DataSet dset)
        {
            try
            {
                provider.Open();
                if (dset.Tables[table] != null) dset.Tables.Remove(table);
                dset.Tables.Add(Data(sql, provider));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + sql);
            }
            finally
            {
                if (provider.State == ConnectionState.Open) provider.Close();
            }
        }

        /// <summary>
        ///     Execute command
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connectionString"></param>
        /// <param name="lastError"></param>
        /// <returns></returns>
        public static int Command(string sql, string connectionString, List<DbParameter> param = null)
        {
            var connecteur = SetConnection(connectionString);
            if (connecteur != null)
                return Command(sql, param, connecteur);
            return -1;
        }

        public static object ScalarCommand(string sql, List<DbParameter> param, string connectionString)
        {
            var connecteur = SetConnection(connectionString);
            return connecteur != null ? ScalarCommand(sql, param, connecteur) : null;
        }

        /// <summary>
        ///     Exécute la commande sql sur le connecteur spécifié
        ///     retourne le nombre de ligne modifié ou -1 en cas d'erreur
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connecteur"></param>
        /// <param name="lastError"></param>
        /// <returns></returns>
        public static int Command(string sql, List<DbParameter> param, DbConnection connecteur)
        {
            var num = -1;
            try
            {
                if (connecteur.State != ConnectionState.Open)
                    connecteur.Open();

                using (var command = DefaultFactory.CreateCommand())
                {
                    if (command != null)
                    {
                        command.CommandText = sql;
                        command.Connection = connecteur;
                        if (param != null) command.Parameters.AddRange(param.ToArray());
                        num = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "\n" + sql);
            }
            finally
            {
                if (connecteur.State == ConnectionState.Open)
                    connecteur.Close();
            }
            return num;
        }

        public static object ScalarCommand(string sql, List<DbParameter> param, DbConnection connecteur)
        {
            try
            {
                if (connecteur.State != ConnectionState.Open)
                    connecteur.Open();
                using (var command = DefaultFactory.CreateCommand())
                {
                    if (command != null)
                    {
                        command.CommandText = sql;
                        command.Connection = connecteur;
                        command.Parameters.AddRange(param.ToArray());
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "\n" + sql);
            }
            finally
            {
                if (connecteur.State == ConnectionState.Open)
                    connecteur.Close();
            }
            return false;
        }

        public static string Filtre(string src)
        {
            src = src.Replace("\"", "''");
            return src.Replace("'", "''");
        }

        public static string FormatDate(DateTime dt)
        {
            return String.Format("{0:yyyy/MM/dd HH:mm:ss}", dt);
        }

        /// <summary>
        ///     Retourne la valeur entre guillemet si besoin
        /// </summary>
        /// <param name="val">valeur du champ</param>
        /// <param name="t">type du champ</param>
        /// <returns>chaine complété</returns>
        public static bool isQuotedType(DbType t)
        {
            return
                t == DbType.String ||
                t == DbType.Date ||
                t == DbType.DateTime ||
                t == DbType.DateTime2 ||
                t == DbType.DateTimeOffset ||
                t == DbType.StringFixedLength ||
                t == DbType.Time ||
                t == DbType.Xml;
        }

        public static bool ImportRowIfNotExists(DataTable dataTable, DataRow dataRow, string keyColumnName)
        {
            string selectStatement = string.Format("{0} = '{1}'", keyColumnName, dataRow[keyColumnName]);
            DataRow[] rows = dataTable.Select(selectStatement);
            if (rows.Length == 0)
            {
                dataTable.Rows.Add(dataRow);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static class GenericParameter
    {
        public static DbParameter Get(string name, DbType type, object value)
        {
            var p = DataTools.DefaultFactory.CreateParameter();
            if (p == null) return null;
            p.ParameterName = name;
            p.DbType = type;

            p.Value = value;
            return p;
        }

        public static DbParameter Get(string name, DbType type, int size, string sourceColumn)
        {
            var p = DataTools.DefaultFactory.CreateParameter();
            if (p == null) return null;
            p.ParameterName = name;
            p.DbType = type;
            p.SourceColumn = sourceColumn;
            p.Size = size;
            return p;
        }
    }
}