namespace BLL
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Net;

    public static class LogService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.Sys_Log> qq = from x in db.Sys_Log orderby x.OperationTime descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string userId, string startTime, string endTime, string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Sys_Log> q = qq;
            if (userId != "0")
            {
                q = q.Where(e => e.UserId == userId);
            }

            if (!String.IsNullOrEmpty(startTime))
            {
                q = q.Where(e => e.OperationTime >= Convert.ToDateTime(startTime));
            }

            if (!String.IsNullOrEmpty(endTime))
            {
                q = q.Where(e => e.OperationTime <= Convert.ToDateTime(endTime));
            }
            if (!string.IsNullOrEmpty(projectId))
            {
                q = from x in q join y in db.Sys_User on x.UserId equals y.UserId where y.ProjectId == projectId orderby x.OperationTime descending select x;
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }

            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       UserName = (from u in db.Sys_User where u.UserId == x.UserId select u.UserName).First(),
                       x.OperationTime,
                       x.Ip,
                       x.HostName,
                       x.OperationLog
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string userId, string startTime, string endTime, string projectId)
        {
            return count;
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="opLog"></param>
        public static void AddLog(string userId, string opLog)
        {
            //SetOvertime(userId);
            Model.HJGLDB db = Funs.DB;
            Model.Sys_Log log = new Model.Sys_Log();

            log.LogId = SQLHelper.GetNewID(typeof(Model.Sys_Log));
            log.HostName = Dns.GetHostName();
            IPAddress[] ips = Dns.GetHostAddresses(log.HostName);
            if (ips.Length > 0)
            {
                foreach (IPAddress ip in ips)
                {
                    if (ip.ToString().IndexOf('.') != -1)
                    {
                        log.Ip = ip.ToString();
                    }
                }
            }
            log.OperationTime = DateTime.Now;
            log.OperationLog = opLog;
            log.UserId = userId;
            db.Sys_Log.InsertOnSubmit(log);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据人员Id查询所有日志信息的数量
        /// </summary>
        /// <param name="userId">人员Id</param>
        /// <returns>日志信息的数量</returns>
        public static int GetLogCountByUserId(string userId)
        {
            var q = (from x in Funs.DB.Sys_Log where x.UserId == userId select x).ToList();
            return q.Count();
        }
    }
}
