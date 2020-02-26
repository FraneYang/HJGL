using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 探伤综合分析
    /// </summary>
    public static class DetectionAnalyzeService
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
        /// <param name="isono"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitcode, string workAreaCode, string isono, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {
            if (unitcode == "0")
            {
                unitcode = null;
            }

            if (string.IsNullOrEmpty(isono))
            {
                isono = null;
            }

            if (string.IsNullOrEmpty(supervisorUnitId))
            {
                supervisorUnitId = null;
            }

            if (flag == "0" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IEnumerable<Model.SpRptndtReport> qq = Funs.DB.SpRptndtReport(unitcode, isono, projectId, supervisorUnitId);

                //if (!string.IsNullOrEmpty(workAreaCode))
                //{
                //    qq = qq.Where(e => e.WorkAreaCode == workAreaCode);
                //}

                var q = qq.ToList();
                count = q.Count();

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.iso_id,
                           x.iso_isono,
                           x.bsu_unitcode,
                           x.bsu_unitname,
                           x.WorkAreaCode,
                           x.source_rate,
                           x.total_jot,
                           x.finished_total_jot,
                           x.trust_total_jot,
                           x.ext_jot,
                           x.check_total_jot,
                           x.total_repairjot,
                           x.cut_jot
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
        public static int GetListCount(string unitcode, string workAreaCode, string isono, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }
    }
}
