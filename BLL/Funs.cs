namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Data.Linq;

    /// <summary>
    /// 通用方法类。
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// 维护一个DB集合
        /// </summary>
        private static Dictionary<int, Model.HJGLDB> dataBaseLinkList = new System.Collections.Generic.Dictionary<int, Model.HJGLDB>();
        

        /// <summary>
        /// 维护一个DB集合
        /// </summary>
        public static System.Collections.Generic.Dictionary<int, Model.HJGLDB> DBList
        {
            get
            {                
                return dataBaseLinkList;
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string connString;
       
        /// <summary>
        /// 数据库连结字符串。
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (connString == null)
                {
                    throw new NotSupportedException("请设置连接字符串！");
                }

                return connString;
            }

            set
            {
                if (connString != null)
                {
                    throw new NotSupportedException("连接已设置！");
                }

                connString = value;
            }
        }

        /// <summary>
        /// 单位设置
        /// </summary>
        public static string UnitSet
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库上下文。
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
        /// 为目标下拉框加上 "请选择" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void PleaseSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- 请选择 -", "0"));
            return;
        }

        /// <summary>
        /// 为目标下拉框加上 "重新编制" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void ReCompileSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("重新编制", "0"));
            return;
        }

        /// <summary>
        /// 字符串是否为浮点数
        /// </summary>
        /// <param name="decimalStr">要检查的字符串</param>
        /// <returns>返回是或否</returns>
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
        /// 判断一个字符串是否是整数
        /// </summary>
        /// <param name="integerStr">要检查的字符串</param>
        /// <returns>返回是或否</returns>
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
        /// 获取新的数字
        /// </summary>
        /// <param name="number">要转换的数字</param>
        /// <returns>新的数字</returns>
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
        /// 判断字符串从第n位开始以后是否都为0
        /// </summary>
        /// <param name="number">要判断的字符串</param>
        /// <param name="n">开始的位数</param>
        /// <returns>false不都为0，true都为0</returns>
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
        /// 截取字符串长度
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="n">长度</param>
        /// <returns>截取后字符串</returns>
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
        /// 根据标识返回字符串list
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<string> GetStrListByStr(string str, char n)
        {
            List<string> strList = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                strList.AddRange(str.Split(n));
            }

            return strList;
        }

        /// <summary>
        /// 指定上传文件的名称
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            Random rm = new Random(System.Environment.TickCount);
            return System.DateTime.Now.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
        }

        #region 时间转换
        /// <summary>
        /// 输入文本转换时间类型
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
        /// 输入文本转换时间类型（空时：默认当前时间）
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

        #region 数字转换
        /// <summary>
        /// 输入文本转换数字类型
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
        /// 输入文本转换数字类型
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
        /// 输入文本转换数字类型
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
        /// 输入文本转换数字类型
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

