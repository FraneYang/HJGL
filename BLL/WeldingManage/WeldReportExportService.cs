using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class WeldReportExportService
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
        /// 获取列表
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string projectId, string unitId, string workareaId,string iso_IsoNo, DateTime? date1, DateTime? date2, int startRowIndex, int maximumRows)
        {
            if (string.IsNullOrEmpty(workareaId) || workareaId == "0")
            {
                workareaId = null;
            }
            if (string.IsNullOrEmpty(iso_IsoNo))
            {
                iso_IsoNo = null;
            }
            if (string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IEnumerable<Model.SpRpWeldReportExport> qq = Funs.DB.SpRpWeldReportExport(projectId, unitId, workareaId, iso_IsoNo, date1, date2);

                var q = qq.ToList();
                count = q.Count();

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.WED_Code,
                           x.ISO_IsoNo,
                           x.JOT_JointNo,
                           x.JOT_JointDesc,
                           x.STE_Name,
                           x.ISO_IsoNumber,
                           x.weldLocal,
                           x.WME_Name,
                           x.WMT_MatName,
                           x.JOT_PrepareTemp,
                           x.ReportMonth,
                           x.Reportday,
                           x.NDTT_CheckCode,
                           x.JOT_HotRpt
                       };
            }
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string projectId, string unitId, string workareaId, string iso_IsoNo, DateTime? date1, DateTime? date2)
        {
            return count;
        }
    }
}
