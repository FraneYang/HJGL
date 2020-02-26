using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 焊工业绩分析
    /// </summary>
    public static class WelderPerformanceService
    {
        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="workareacode"></param>
        /// <param name="steel"></param>
        /// <param name="wloName"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitcode, string workareacode, string steel, string wloName, DateTime? date1, DateTime? date2, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {
            if (unitcode=="0")
            {
                unitcode = null;
            }
            if (workareacode =="0")
            {
                workareacode = null;
            }
            if (steel=="0")
            {
                steel = null;
            }
            if (wloName=="0")
            {
                wloName = null; 
            }

            if (flag == "0" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IEnumerable<Model.SpRpWelderPerformance> qq = Funs.DB.SpRptWelderPerformance(unitcode, workareacode, steel, wloName, date1, date2, projectId, supervisorUnitId);

                var q = qq.ToList();
                count = q.Count();

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.wed_code,
                           x.wed_name,
                           WED_Sex = (x.WED_Sex == "2" ? "女" : "男"),
                           x.nowtotal_din,
                           x.nowtotal_jot,
                           x.nowtotal_repairjot,
                           nowrepairrate = x.nowrepairrate * 100,
                           nowfinishedrate = x.nowfinishedrate * 100,
                           x.current_count_film,
                           x.current_pass_film,
                           current_passrate = x.current_passrate * 100,
                           x.nowtotalfilm,
                           x.nowpassfilm,
                           x.nownotpassfilm,
                           nowpassrate = x.nowpassrate * 100,
                           nowunpassrate = x.nowunpassrate * 100,
                           x.totaldin,
                           x.total_jot,
                           x.total_repairjot,
                           repairrate = x.repairrate * 100,
                           finishedrate = x.finishedrate * 100,
                           x.totalfilm,
                           x.passfilm,
                           x.notpassfilm,
                           passrate =x.passrate * 100,
                           unpassrate = x.unpassrate * 100,
                           x.education,
                           WED_IfOnGuard = (x.WED_IfOnGuard == true ? "是" : "否")
                       };
            }
        }
        /// <summary>
        ///获取列表数
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="workareacode"></param>
        /// <param name="steel"></param>
        /// <param name="wloName"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string unitcode, string workareacode, string steel, string wloName, DateTime? date1, DateTime? date2, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }

    }
}
