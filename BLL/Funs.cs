namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Data.Linq;

    /// <summary>
    /// ͨ�÷����ࡣ
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// ά��һ��DB����
        /// </summary>
        private static Dictionary<int, Model.HJGLDB> dataBaseLinkList = new System.Collections.Generic.Dictionary<int, Model.HJGLDB>();
        

        /// <summary>
        /// ά��һ��DB����
        /// </summary>
        public static System.Collections.Generic.Dictionary<int, Model.HJGLDB> DBList
        {
            get
            {                
                return dataBaseLinkList;
            }
        }

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        private static string connString;
       
        /// <summary>
        /// ���ݿ������ַ�����
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (connString == null)
                {
                    throw new NotSupportedException("�����������ַ�����");
                }

                return connString;
            }

            set
            {
                if (connString != null)
                {
                    throw new NotSupportedException("���������ã�");
                }

                connString = value;
            }
        }

        /// <summary>
        /// ��λ����
        /// </summary>
        public static string UnitSet
        {
            get;
            set;
        }

        /// <summary>
        /// ���ݿ������ġ�
        /// </summary>
        public static Model.HJGLDB DB
        {
            get
            {                
                if (!DBList.ContainsKey(System.Threading.Thread.CurrentThread.ManagedThreadId))
                {
                    DBList.Add(System.Threading.Thread.CurrentThread.ManagedThreadId, new Model.HJGLDB(connString));                    
                }

               // DBList[System.Threading.Thread.CurrentThread.ManagedThreadId].CommandTimeout = 1200;
                return DBList[System.Threading.Thread.CurrentThread.ManagedThreadId];
            }
        }

        /// <summary>
        /// ΪĿ����������� "��ѡ��" ��
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void PleaseSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- ��ѡ�� -", "0"));
            return;
        }

        /// <summary>
        /// ΪĿ����������� "���±���" ��
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void ReCompileSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("���±���", "0"));
            return;
        }

        /// <summary>
        /// �ַ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="decimalStr">Ҫ�����ַ���</param>
        /// <returns>�����ǻ��</returns>
        public static bool IsDecimal(string decimalStr)
        {
            if (String.IsNullOrEmpty(decimalStr))
            {
                return false;
            }

            try
            {
                Convert.ToDecimal(decimalStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// �ж�һ���ַ����Ƿ�������
        /// </summary>
        /// <param name="integerStr">Ҫ�����ַ���</param>
        /// <returns>�����ǻ��</returns>
        public static bool IsInteger(string integerStr)
        {
            if (String.IsNullOrEmpty(integerStr))
            {
                return false;
            }

            try
            {
                Convert.ToInt32(integerStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�µ�����
        /// </summary>
        /// <param name="number">Ҫת��������</param>
        /// <returns>�µ�����</returns>
        public static string InterceptDecimal(object number)
        {
            if (number == null)
            {
                return null;
            }
            decimal newNumber = 0;
            string newNumberStr = "";
            int an = -1;
            string numberStr = number.ToString();
            int n = numberStr.IndexOf(".");
            if (n == -1)
            {
                return numberStr;
            }
            for (int i = n + 1; i < numberStr.Length; i++)
            {
                string str = numberStr.Substring(i, 1);
                if (str == "0")
                {
                    if (GetStr(numberStr, i))
                    {
                        an = i;
                        break;
                    }
                }
            }
            if (an == -1)
            {
                newNumber = Convert.ToDecimal(numberStr);
            }
            else if (an == n + 1)
            {

                newNumberStr = numberStr.Substring(0, an - 1);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            else
            {
                newNumberStr = numberStr.Substring(0, an);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            return newNumber.ToString();
        }

        /// <summary>
        /// �ж��ַ����ӵ�nλ��ʼ�Ժ��Ƿ�Ϊ0
        /// </summary>
        /// <param name="number">Ҫ�жϵ��ַ���</param>
        /// <param name="n">��ʼ��λ��</param>
        /// <returns>false����Ϊ0��true��Ϊ0</returns>
        public static bool GetStr(string number, int n)
        {
            for (int i = n; i < number.Length; i++)
            {
                if (number.Substring(i, 1) != "0")
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="str">Ҫ��ȡ���ַ���</param>
        /// <param name="n">����</param>
        /// <returns>��ȡ���ַ���</returns>
        public static string GetSubStr(object str, object n)
        {
            if (str != null)
            {
                if (str.ToString().Length > Convert.ToInt32(n))
                {
                    return str.ToString().Substring(0, Convert.ToInt32(n)) + "....";
                }
                else
                {
                    return str.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// ָ���ϴ��ļ�������
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            Random rm = new Random(System.Environment.TickCount);
            return System.DateTime.Now.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
        }

        #region ʱ��ת��
        /// <summary>
        /// �����ı�ת��ʱ������
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetNewDateTime(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return null;
            }
        }

        /// <summary>
        /// �����ı�ת��ʱ�����ͣ���ʱ��Ĭ�ϵ�ǰʱ�䣩
        /// </summary>
        /// <returns></returns>
        public static DateTime GetNewDateTimeOrNow(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return System.DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return System.DateTime.Now;
            }
        }
        #endregion

        #region ����ת��
        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewDecimalOrZero(string value)
        {
            decimal revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static decimal? GetNewDecimal(string value)
        {
            decimal? revalue = null;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static int? GetNewInt(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    return Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static int GetNewIntOrZero(string value)
        {
            int revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }
        #endregion
    }
}
