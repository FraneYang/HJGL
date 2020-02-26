namespace BLL
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Net;

    public static class ErrLogInfoService
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
        //private static IQueryable<Model.Sys_ErrLogInfo> qq = from x in db.Sys_ErrLogInfo 
        //                                                     where x.ErrMessage != null
        //                                                     orderby x.ErrTime descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string startTime, string endTime, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_ErrLogInfoList> q = from x in db.View_ErrLogInfoList
                                                 where x.ErrMessage != null
                                                 orderby x.ErrTime descending
                                                 select x;
            if (!String.IsNullOrEmpty(startTime))
            {
                q = q.Where(e => e.ErrTime > Convert.ToDateTime(startTime).AddDays(-1));
            }

            if (!String.IsNullOrEmpty(endTime))
            {
                q = q.Where(e => e.ErrTime < Convert.ToDateTime(endTime).AddDays(1));
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }

            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.ErrLogId,
                       x.ErrType,
                       x.ErrMessage,
                       ErrMessageShort = Funs.GetSubStr(x.ErrMessage, 10),
                       x.ErrStackTrace,
                       ErrStackTraceShort = Funs.GetSubStr(x.ErrStackTrace, 10),
                       x.ErrTime,
                       ErrUrlShort =Funs.GetSubStr(x.ErrUrl,60),
                       x.ErrUrl,
                       x.ErrIP,
                       x.ProjectName,
                       x.UnitName,
                       x.UserName,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string startTime, string endTime)
        {
            return count;
        }
    }
}
