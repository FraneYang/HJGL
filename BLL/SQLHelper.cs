using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// �������ݷ��ʲ���Ļ��࣬��װ��һЩ���ݿ���ʵı�������
    /// </summary>
    /// 
    public class SQLHelper
    {

        private static string connectionString = Funs.ConnString;
        
        private static SqlConnection Connection = new SqlConnection(connectionString);
        
        public static SqlConnection GetConn()
        {
            return Connection;
        }
        /// <summary>
        ///��RunProcedure���� ���� SqlCommand ����.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure in the DB, eg. sp_DoTask</param>
        /// <param name="parameters">Array of IDataParameter objects containing parameters to the stored proc</param>
        /// <returns>Newly instantiated SqlCommand instance</returns>
        private static SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(storedProcName, parameters);
           
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int,
                4, /* Size */
                ParameterDirection.ReturnValue,
                false, /* is nullable */
                0, /* byte precision */
                0, /* byte scale */
                string.Empty,
                DataRowVersion.Default,
                null));

            return command;
        }


        /// <summary>
        /// ��BuildIntCommand�б����õ�˽�к��������ڹ��� SqlCommand ����.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of IDataParameter objects</param>
        /// <returns></returns>
        private static SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, Connection);           
            command.CommandType = CommandType.StoredProcedure;//ִ�д洢����          
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }


        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;
            if (Connection.State == ConnectionState.Open) Connection.Close();
            Connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            rowsAffected = command.ExecuteNonQuery();//ִ����洢���̺󷵻صĽ��
            result = (int)command.Parameters["ReturnValue"].Value;
            Connection.Close();
            return result;
        }

        /// <summary>
        /// ͨ���洢���̻����ID���洢�����з���ֵ�и�output������
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <returns></returns>
        public static string RunProcNewId(string storedProcName)
        {
            string str = "";
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(storedProcName, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@returnVal", SqlDbType.VarChar, 50));
                command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                str = command.Parameters["@returnVal"].Value.ToString();
            }
            finally
            {
                Connection.Close();
            }
            return str;
        }

        /// <summary>
        /// ͨ���洢�������ͱ����Լ����������ID���洢�����з���ֵ�и�output������
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="tableName">����</param>
        /// <param name="columnName">����</param>
        /// <returns></returns>
        public static string RunProcNewId(string storedProcName, string tableName, string columnName)
        {
            string str = "";
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(storedProcName, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter[] values = new SqlParameter[]
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@columnName", columnName),
                new SqlParameter("@returnVal", SqlDbType.VarChar, 10)
            };
                command.Parameters.AddRange(values);
                command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                str = command.Parameters["@returnVal"].Value.ToString();
            }
            finally
            {
                Connection.Close();
            }
            return str;
        }

        /// <summary>
        ///  ͨ���洢�������ͱ����Լ����������ID���洢�����з���ֵ�и�output������
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="tableName">����</param>
        /// <param name="columnName">����</param>
        /// <param name="perfix">ǰ׺</param>
        /// <returns>��������ID</returns>
        public static string RunProcNewId(string storedProcName, string tableName, string columnName, string projectId, string prefix)
        {
            string str = "";
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(storedProcName, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter[] values = new SqlParameter[]
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@columnName", columnName),
                new SqlParameter("@projectId", projectId),
                new SqlParameter("@prefix", prefix),
                new SqlParameter("@returnVal", SqlDbType.VarChar, 30)
            };
                command.Parameters.AddRange(values);
                command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                str = command.Parameters["@returnVal"].Value.ToString();
            }
            finally
            {
                Connection.Close();
            }
            return str;
        }

        /// <summary>
        ///  ͨ���洢�������ͱ����Լ����������ID���洢�����з���ֵ�и�output������
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="tableName">����</param>
        /// <param name="columnName">����</param>
        /// <param name="perfix">ǰ׺</param>
        /// <returns>��������ID</returns>
        public static string RunProcNewId(string storedProcName, string tableName, string columnName, string prefix)
        {
            string str = "";
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(storedProcName, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter[] values = new SqlParameter[]
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@columnName", columnName),
                new SqlParameter("@prefix", prefix),
                new SqlParameter("@returnVal", SqlDbType.VarChar, 30)
            };
                command.Parameters.AddRange(values);
                command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                str = command.Parameters["@returnVal"].Value.ToString();
            }
            finally
            {
                Connection.Close();
            }
            return str;
        }

        /// <summary>
        /// ͨ���洢���̻��DataTable
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <returns></returns>
        public static DataTable GetDataTableRunProc(string storedProcName, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(storedProcName, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            finally
            {
                Connection.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Will run a stored procedure, can only be called by those classes deriving
        /// from this base. It returns a SqlDataReader containing the result of the stored
        /// procedure.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters to be passed to the procedure</param>
        /// <returns>A newly instantiated SqlDataReader object</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlDataReader returnReader;
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = BuildQueryCommand(storedProcName, parameters);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader();
            }
            finally { Connection.Close(); }
            return returnReader;
        }

        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>�ַ���</returns>
        public static string getStr(string sql)
        {
            string str = "";
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(sql, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                str = command.ExecuteScalar().ToString();
            }
            finally
            {
                Connection.Close();
            }
            return str;

        }

        public static int getIntValue(string sql)
        {
            int i = 0;
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(sql, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                i = Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                Connection.Close();
            }
            return i;

        }

        /// <summary>
        /// ִ��SQL���
        /// </summary>
        /// <param name="sql">sql���</param>
        public static void ExecutSql(string sql)
        {
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(sql, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a DataSet by running the stored procedure and placing the results
        /// of the query/proc into the given tablename.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
            }
            finally
            {
                Connection.Close();
            }
            return dataSet;
        }

        /// <summary>
        /// Takes an -existing- dataset and fills the given table name with the results
        /// of the stored procedure.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildIntCommand(storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
            }
            finally
            {
                Connection.Close();
            }
        }
        /// <summary>
        /// ���ڼ򵥵�Sql��ѯ������DataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet RunSqlString(string strSql, string tableName)
        {
            DataSet dataSet = new DataSet();
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(strSql, Connection);
                command.CommandTimeout = 600;
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = command;
                sqlDA.Fill(dataSet, tableName);
            }
            finally
            {
                Connection.Close();
            }

            return dataSet;
        }
        /// <summary>
        /// ���ڼ򵥵�Sql��ѯ
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        public static void RunProcedure(string strSql, DataSet dataSet, string tableName)
        {
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(strSql, Connection);
                command.CommandTimeout = 600;
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = command;
                sqlDA.Fill(dataSet, tableName);
            }
            finally
            {
                Connection.Close();
            }
        }
        /// <summary>
        /// ���ڼ򵥵�Sqlִ�У�������ֵ���������ݲ������͵�
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="result"></param>
        public static void RunSqlString(string strSql, out int result)
        {
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlCommand command = new SqlCommand(strSql, Connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;

                result = command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
        }


        /*	public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName )
            {
                DataSet dataSet = new DataSet();
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand( storedProcName, parameters );
                sqlDA.Fill( dataSet, tableName );
                Connection.Close();

                return dataSet;
            }*/

        /// ���ش洢����ִ�еĽ����
        /// </summary>
        /// <param name="pur_name">�洢��������</param>
        /// <param name="parmList">�洢���̵�{����--ֵ}�ļ���</param>
        //  ���ش洢����ִ�еĽ������
        public static DataTable RunProcedureGetTable(string pur_name, Hashtable parmList)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            // ���ô洢����pur_name��
            SqlCommand sqlCmd = new SqlCommand(pur_name, Connection);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // Ϊ���������ֵ��
            foreach (string parm in parmList.Keys)
            {
                sqlCmd.Parameters.Add(new SqlParameter("@" + parm, parmList[parm]));
            }
            // �����ӡ�
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
            sqlCmd.Connection.Open();
            SqlDataReader dreader = null;
            try
            {
                dreader = sqlCmd.ExecuteReader();
                for (int i = 0; i < dreader.FieldCount; i++)
                {
                    DataColumn myDataColumn;
                    myDataColumn = new DataColumn();
                    myDataColumn.DataType = System.Type.GetType(dreader.GetFieldType(i).ToString());
                    myDataColumn.ColumnName = dreader.GetName(i);
                    myDataColumn.Caption = dreader.GetName(i);
                    dt.Columns.Add(myDataColumn);
                }
                while (dreader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < dreader.FieldCount; i++)
                    {
                        dr[i] = dreader[i];
                    }
                    dt.Rows.Add(dr);
                }
                dreader.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
                // ���ɹ��������ʾ��
                throw new Exception(ex.ToString());
            }
            finally
            {
                // �ر����ӡ�
                sqlCmd.Connection.Close();
            }

            return dt;
        }

        /// </summary>
        /// <param name="strSql">sql��</param>
        /// <param name="parmList">sql{����--ֵ}�ļ���</param>
        public static DataTable RunSqlGetTable(string strSql)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            // ���ô洢����pur_name��
            SqlCommand sqlCmd = new SqlCommand(strSql, Connection);
            sqlCmd.CommandTimeout = 0;
            // �����ӡ�
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
            sqlCmd.Connection.Open();
            SqlDataReader dreader = null;
            try
            {
                dreader = sqlCmd.ExecuteReader();
                for (int i = 0; i < dreader.FieldCount; i++)
                {
                    DataColumn myDataColumn;
                    myDataColumn = new DataColumn();
                    myDataColumn.DataType = System.Type.GetType(dreader.GetFieldType(i).ToString());
                    myDataColumn.ColumnName = dreader.GetName(i);
                    myDataColumn.Caption = dreader.GetName(i);
                    dt.Columns.Add(myDataColumn);
                }
                while (dreader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < dreader.FieldCount; i++)
                    {
                        dr[i] = dreader[i];
                    }
                    dt.Rows.Add(dr);
                }
                dreader.Close();
            }
            catch (Exception ex)
            {
                // ���ɹ��������ʾ��
                throw new Exception(ex.ToString());
            }
            finally
            {
                // �ر����ӡ�
                sqlCmd.Connection.Close();
            }

            return dt;
        }


        /// ���ش洢����ִ�еĽ����
        /// </summary>
        /// <param name="pur_name">�洢��������</param>
        /// <param name="parmList">�洢���̵�{����--ֵ}�ļ���</param>
        public static void RunProcedure(string pur_name, Hashtable parmList)
        {
            // ���ô洢����pur_name��
            SqlCommand sqlCmd = new SqlCommand(pur_name, Connection);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // Ϊ���������ֵ��
            foreach (string parm in parmList.Keys)
            {
                sqlCmd.Parameters.Add(new SqlParameter("@" + parm, parmList[parm]));
            }
            // �����ӡ�
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
            sqlCmd.Connection.Open();
            try
            {
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // ���ɹ��������ʾ��
                throw new Exception(ex.ToString());
            }
            finally
            {
                // �ر����ӡ�
                sqlCmd.Connection.Close();
            }

        }


        /// ���ش洢����ִ�еĽ����
        /// </summary>
        /// <param name="pur_name">�洢��������</param>
        /// <param name="parmList">�洢���̵�{����--ֵ}�ļ���</param>
        public static void RunProcedure(string pur_name, Hashtable parmList, ref  int returnValue)
        {
            // ���ô洢����pur_name��
            SqlCommand sqlCmd = new SqlCommand(pur_name, Connection);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // Ϊ���������ֵ��
            foreach (string parm in parmList.Keys)
            {
                sqlCmd.Parameters.Add(new SqlParameter("@" + parm, parmList[parm]));
            }
            // �����ӡ�
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
            sqlCmd.Connection.Open();
            try
            {
                returnValue = sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // ���ɹ��������ʾ��
                throw new Exception(ex.ToString());
            }
            finally
            {
                // �ر����ӡ�
                sqlCmd.Connection.Close();
            }

        }

        private static void AddParameterToCommand(SqlCommand cmd, SqlParameter[] param)
        {
            foreach (SqlParameter p in param)
            {
                cmd.Parameters.Add(p);
            }
        }

        private static SqlCommand CreateCommand(CommandType commandType,
            string commandText, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 0;
            cmd.CommandType = commandType;
            cmd.Connection = Connection;

            if (param != null)
            {
                AddParameterToCommand(cmd, param);
            }
            cmd.CommandText = commandText;

            return cmd;
        }


        public static int ExecuteCommand(CommandType commandType,
            string commandText, SqlParameter[] param)
        {
            SqlCommand cmd = CreateCommand(commandType, commandText, param);
            cmd.CommandTimeout = 0;
            SqlTransaction trans = null;

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                cmd.Connection.Open();
                trans = cmd.Connection.BeginTransaction();
                cmd.Transaction = trans;
                int i = cmd.ExecuteNonQuery();
                trans.Commit();

                return i;
            }
            catch (SqlException se)
            {
                trans.Rollback();
                throw se;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// ����һ��������
        /// </summary>
        private static object newIdLocker = new object();

        /// <summary>
        /// ����һ���µ�����ָ������������
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetNewID(Type table)
        {
            lock (newIdLocker)
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// ����һ���µ�����ָ������������
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetNewID()
        {
            lock (newIdLocker)
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// �������е��������ֵ
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="keyColumnName">������</param>
        /// <returns>�������ֵ</returns>
        public static int GetMaxId(string tableName, string ColumnName)
        {
            int maxId = 0;
            string str = "SELECT (ISNULL(MAX(" + ColumnName + "),0)+1) from " + tableName + "";
            maxId = getIntValue(str);
            return maxId;
        }
    }
}