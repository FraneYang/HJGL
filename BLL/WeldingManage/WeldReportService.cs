using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class WeldReportService
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
        private static IQueryable<Model.BO_WeldReportMain> qq = from x in db.BO_WeldReportMain orderby x.JOT_WeldDate descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string startTime, string endTime, string DReportID, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BO_WeldReportMain> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            if (!String.IsNullOrEmpty(startTime))
            {
                q = q.Where(e => e.CHT_TableDate >= Convert.ToDateTime(startTime));
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                q = q.Where(e => e.CHT_TableDate <= Convert.ToDateTime(endTime));
            }
            if (!string.IsNullOrEmpty(DReportID))
            {
                q = q.Where(e => e.JOT_DailyReportNo.Contains(DReportID));
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.DReportID,
                       x.ProjectId,
                       x.BSU_ID,
                       x.JOT_WeldDate,
                       x.JOT_DailyReportNo,
                       x.CHT_Tabler,
                       x.CHT_TableDate,
                       x.JOT_Remark,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId, string startTime, string endTime, string DReportID)
        {
            return count;
        }

        /// <summary>
        /// 根据检查编号查询焊接日报信息
        /// </summary>
        /// <param name="checkCode">检查编号</param>
        /// <returns>焊接日报信息</returns>
        public static Model.BO_WeldReportMain GetWeldReportByDReportID(string dReportID)
        {
            return Funs.DB.BO_WeldReportMain.FirstOrDefault(x => x.DReportID == dReportID);
        }

        /// <summary>
        /// 根据焊接日报主键获取焊接日报信息
        /// </summary>
        /// <param name="weldReportNo">焊接日报编号</param>
        /// <returns>焊接日报信息</returns>
        public static bool IsExistDailyReportNO(string weldReportNo)
        {
            var q = from x in Funs.DB.BO_WeldReportMain where x.JOT_DailyReportNo == weldReportNo select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 增加焊接日报信息
        /// </summary>
        /// <param name="weldReport">焊接日报实体</param>
        public static void AddWeldReport(Model.BO_WeldReportMain weldReport)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BO_WeldReportMain newWeldReport = new Model.BO_WeldReportMain();
            newWeldReport.DReportID = weldReport.DReportID;
            newWeldReport.ProjectId = weldReport.ProjectId;
            newWeldReport.InstallationId = weldReport.InstallationId;
            newWeldReport.BSU_ID = weldReport.BSU_ID;
            newWeldReport.JOT_WeldDate = weldReport.JOT_WeldDate;
            newWeldReport.JOT_DailyReportNo = weldReport.JOT_DailyReportNo;
            newWeldReport.CHT_Tabler = weldReport.CHT_Tabler;
            newWeldReport.CHT_TableDate = weldReport.CHT_TableDate;
            newWeldReport.JOT_Remark = weldReport.JOT_Remark;

            db.BO_WeldReportMain.InsertOnSubmit(newWeldReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接日报信息
        /// </summary>
        /// <param name="weldReport">焊接日报实体</param>
        public static void UpdateWeldReport(Model.BO_WeldReportMain weldReport)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BO_WeldReportMain newWeldReport = db.BO_WeldReportMain.First(e => e.DReportID == weldReport.DReportID);
            newWeldReport.BSU_ID = weldReport.BSU_ID;
            newWeldReport.JOT_WeldDate = weldReport.JOT_WeldDate;
            newWeldReport.JOT_DailyReportNo = weldReport.JOT_DailyReportNo;
            newWeldReport.CHT_Tabler = weldReport.CHT_Tabler;
            newWeldReport.CHT_TableDate = weldReport.CHT_TableDate;
            newWeldReport.JOT_Remark = weldReport.JOT_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据焊接日报主键删除一个焊接日报信息
        /// </summary>
        /// <param name="weldReportCode">焊接日报主键</param>
        public static void DeleteWeldReportByDReportID(string dReportID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BO_WeldReportMain weldReport = db.BO_WeldReportMain.First(e => e.DReportID == dReportID);
            db.BO_WeldReportMain.DeleteOnSubmit(weldReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位Id获取焊接日报数
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetWeldReportByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.BO_WeldReportMain where x.BSU_ID == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据装置Id获取焊接日报数
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetWeldReportByInstallationId(int installationId)
        {
            var q = (from x in Funs.DB.BO_WeldReportMain where x.InstallationId == installationId select x).ToList();
            return q.Count();
        }


        /// <summary>
        /// 获取日报
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWeldReportList()
        {
            var q = (from x in BLL.Funs.DB.BO_WeldReportMain orderby x.JOT_DailyReportNo select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].JOT_DailyReportNo ?? "", q[i].DReportID.ToString());
            }
            return item;
        }
    }
}
