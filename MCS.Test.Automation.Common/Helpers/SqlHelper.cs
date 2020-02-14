// <copyright file="SqlHelper.cs" company="MCS">
// Copyright (c) MCS. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Reflection;
    using System.Text;
    using NLog;
    using RelevantCodes.ExtentReports;

    /// <summary>
    /// Class is used for execution SQL queries and reading data from database.
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// NLog logger handle
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method is used for execution SQL query (select) and reading each row from column.
        /// </summary>
        /// <param name="command">SQL query</param>
        /// <param name="column">Name of column</param>
        /// <returns>
        /// Collection of each row existed in column.
        /// </returns>
        /// <example>How to use it: <code>
        /// var connectionString = "User ID=sqluser;Password=sqluserpassword;server=servername;";
        /// const string ColumnName = "AccountNumber";
        /// const string SqlQuery = "SELECT  AccountNumber as " + ColumnName + " FROM [AdventureWorks].[Sales].[Customer] where [CustomerID] in (1, 2)";
        /// var result = SqlHelper.ExecuteSqlCommand(SqlQuery, connectionString, ColumnName);
        /// </code></example>
        public static List<string> GetSingleColumnValue(string command, string column)
        {
            var resultList = new List<string>();
            try
            {
                Logger.Trace(CultureInfo.CurrentCulture, "Execute sql query '{0}'", command);

                using (var connection = new SqlConnection(SqlConnectionString()))
                {
                    connection.Open();
                    using (var sqlCommand = new SqlCommand(command, connection))
                    {
                        using (var sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (!sqlDataReader.HasRows)
                            {
                                Logger.Error(CultureInfo.CurrentCulture, "No result for: {0} \n {1} \n {2}", command, SqlConnectionString(), column);
                                return resultList;
                            }

                            while (sqlDataReader.Read())
                            {
                                resultList.Add(sqlDataReader[column].ToString().Trim());
                            }
                        }
                    }
                }

                if (resultList.Count == 0)
                {

                    Logger.Error(CultureInfo.CurrentCulture, "No result for: {0} \n {1} \n {2}", command, SqlConnectionString(), column);
                }
                else
                {
                    Logger.Trace(CultureInfo.CurrentCulture, "Sql results: {0}", resultList);
                }
                // DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify " + message + " list from database ", "Sql results for " + message + " : " + resultList);
            }

            catch (SqlException e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Connection with Database ", "Failed to connect to database due to : " + e.InnerException);
                Logger.Info("Failed to connect to database");
                throw;
            }

            return resultList;
        }

         public static Dictionary<string, string> GetTableColumnValueInDictionary(DataTable dt, List<string> columnNames)
        {
            Dictionary<string, string> getDict = new Dictionary<string, string>();
            foreach (var keyvalue in columnNames)
            {
                getDict.Add(keyvalue, dt.Rows[0][keyvalue].ToString());
            }
            return getDict;
        }


        public static Dictionary<string, string> GenerateDictionaryfromLists(List<string> keylist1, List<string> valueslist2)
        {
            Dictionary<string, string> getDict = new Dictionary<string, string>();

            if (keylist1.Count == valueslist2.Count)
            {
                for (int i = 0; i < keylist1.Count; i++)
                {
                    getDict.Add(keylist1[i], valueslist2[i]);
                }
            }
            else
            {
                throw new Exception("Both lists does not have same number of elements");
            }
            return getDict;
        }

        /// <summary>
        /// Method is used for execution SQL query (select) and reading each column from row.
        /// </summary>
        /// <param name="command">SQL query</param>
        /// <param name="columns">Name of columns</param>
        /// <returns>
        /// Dictionary of each column existed in raw.
        /// </returns>
        /// <example>How to use it: <code>
        /// var connectionString = "User ID=sqluser;Password=sqluserpassword;server=servername;";
        /// ICollection&lt;string&gt; column = new List&lt;string&gt;();
        /// column.Add("NationalIDNumber");
        /// column.Add("ContactID");
        /// const string SqlQuery = "SELECT [NationalIDNumber] as " + column.ElementAt(0) + " , [ContactID] as " + column.ElementAt(1) + " from [AdventureWorks].[HumanResources].[Employee] where EmployeeID=1";
        /// Dictionary&lt;string, string&gt; results = SqlHelper.ExecuteSqlCommand(command, GetConnectionString(server), column);
        /// </code></example>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Exception when there is not given column in results from SQL query</exception>
        public static Dictionary<string, string> GetTwoColumnValues(string command, IEnumerable<string> columns)
        {

            Logger.Trace(CultureInfo.CurrentCulture, "Execute sql query '{0}'", command);

            var resultList = new Dictionary<string, string>();
            var resultTemp = new Dictionary<string, string>();

            using (var connection = new SqlConnection(SqlConnectionString()))
            {
                connection.Open();

                using (var sqlCommand = new SqlCommand(command, connection))
                {
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (!sqlDataReader.HasRows)
                        {
                            Logger.Error(CultureInfo.CurrentCulture, "No result for: {0} \n {1}", command, SqlConnectionString());
                            return resultList;
                        }

                        while (sqlDataReader.Read())
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                resultTemp[sqlDataReader.GetName(i)] = sqlDataReader.GetSqlValue(i).ToString();
                            }
                        }
                    }
                }
            }

            foreach (string column in columns)
            {
                string keyValue;

                if (resultTemp.TryGetValue(column, out keyValue))
                {
                    resultList[column] = keyValue;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(CultureInfo.CurrentCulture, " Exception while trying to get results from sql query, lack of column '{0}'", column));
                }
            }

            foreach (KeyValuePair<string, string> entry in resultList)
            {
                Logger.Trace(CultureInfo.CurrentCulture, "Sql results: {0} = {1}", entry.Key, entry.Value);
            }

            return resultList;
        }

        public static List<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable CompareTwoDatatables(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            //Create Empty Table   
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public static DataSet ExecuteSqlCommandQuery(string Query)
        {
            DataSet data = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlConnectionString()))
                {
                    connection.Open();
                    StringBuilder querybuilder = new StringBuilder();
                    querybuilder.Append(Query);
                    using (SqlCommand cmd = new SqlCommand(querybuilder.ToString(), connection))
                    {
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlAdapter.Fill(data);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return data;
        }



         public static string GetCountOnSQlQuery(string Query)
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlConnectionString()))
                {
                    //DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify Connection with Database ", "Connecting to Database Started");
                    connection.Open();
                    // DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify Connection with Database ", "Connected to Database successfully");
                    StringBuilder querybuilder = new StringBuilder();
                    querybuilder.Append(Query);
                    using (SqlCommand cmd = new SqlCommand(querybuilder.ToString(), connection))
                    {
                        result = Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                //DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify (" + message + " ) data from database ", "Sql results for (" + message + ") : " + result);
            }
            catch (SqlException e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Connection with Database ", "Failed to connect to database due to : " + e.InnerException);
                Logger.Info("Failed to connect to database");
                throw;
            }
            return result;
        }

        public static List<string> GetSingleColumnInListFromMultipleTables(DataTable dt, string ColumnName)
        {
            List<string> list = new List<string>();
            foreach (DataColumn db in dt.Columns)
            {
                if (db.ColumnName.ToLower() == ColumnName.ToLower())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(dr[db].ToString());
                    }
                }
            }
            return list;
        }

        public static string SqlConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            try
            {
                builder.DataSource = BaseConfiguration.SqlServerName;
                builder.UserID = BaseConfiguration.SqlUserId;
                builder.Password = BaseConfiguration.SqlPassword;
                builder.InitialCatalog = BaseConfiguration.SqlDatabaseName;
                builder.ConnectTimeout = Convert.ToInt32(BaseConfiguration.SqlConnectionTimeOut);
                builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return builder.ToString();
        }

        public static int GetRowsCountOnSQlQuery(DataSet dataSet)
        {
            int rowCount;
            try
            {
                rowCount = dataSet.Tables[0].Rows.Count;
                return rowCount;
            }
            catch (SqlException e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify table rows in Database ", "Failed to get the row count in database due to : " + e.InnerException);
                Logger.Info("Failed to get the row count in database");
                throw;
            }
        }
    }
}
