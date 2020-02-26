using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 委托检测数据类
    /// </summary>
    public static class TrustCheckService
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
        public static IEnumerable GetListData(string unitId, string workAreaId, string projectId, string flag, int startRowIndex, int maximumRows)
        {
            if (unitId == "0")
            {
                unitId = null;
            }

            if (workAreaId=="0")
            {
                workAreaId = null;
            }
            if (flag == "0" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IEnumerable<Model.TrustCheckReport> qq = Funs.DB.SpTrustCheckReport(unitId, workAreaId, projectId);

                var q = qq.ToList();
                count = q.Count();

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.bsu_unitcode,
                           x.bsu_unitname,
                           x.devicename,
                           x.WorkAreaCode,
                           x.trust_Audit_total,
                           x.trust_NoAudit_total,
                           x.check_Audit_total,
                           x.check_NoAudit_total
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
        public static int GetListCount(string unitId, string workAreaId, string projectId, string flag)
        {
            return count;
        }
    }
}