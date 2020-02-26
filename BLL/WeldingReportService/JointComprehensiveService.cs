using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 焊口综合信息
    /// </summary>
    public static class JointComprehensiveService
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
        /// 获取列表值
        /// </summary>
        /// <param name="workarea"></param>
        /// <param name="isono"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string workarea, string isono, string jointDesc, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {

            if (flag != "1" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {

                IQueryable<Model.V_JOINTVIEW> q = from x in Funs.DB.V_JOINTVIEW
                                                  where x.ProjectId == projectId
                                                  orderby x.WorkAreaCode, x.ISO_ISONO, x.JOT_JointNo
                                                  select x;
                if (!string.IsNullOrEmpty(workarea) && workarea != "0")
                {
                    q = q.Where(e => e.WorkAreaId == workarea);
                }
                if (!string.IsNullOrEmpty(isono))
                {
                    q = q.Where(e => e.ISO_ISONO == isono);
                }
                if (!string.IsNullOrEmpty(jointDesc))
                {
                    q = q.Where(e => e.JOT_JointDesc.Contains(jointDesc));
                }
                if (!string.IsNullOrEmpty(supervisorUnitId))
                {
                    q = q.Where(e => e.SupervisorUnitId == supervisorUnitId);
                }
                count = q.Count();
                if (count == 0)
                {
                    return new object[] { "" };
                }
                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.WorkAreaCode,
                           x.ISO_ISONO,
                           x.JOT_BelongPipe,
                           x.JOT_JointNo,
                           x.JOT_Dia,
                           x.JOT_Sch,
                           x.JOT_FactSch,
                           x.STE_NAME,
                           x.JOT_CellWelder,
                           x.JOT_FloorWelder,
                           x.WME_Name,
                           x.NDTR_Rate,
                           x.SER_NAME,
                           x.JOT_WeldDate,
                           x.JOT_DailyReportNo,
                           x.CH_TRUSTCODE1,
                           x.IS_Proess,
                           x.CHT_CHECKDATE,
                           x.JOT_Size,
                           x.WMT_MatCode,
                           x.WMT_Matname,
                           x.HsCode,
                           x.Hsname,
                           x.JOT_JointDesc,
                           x.If_dk,
                           x.ProessName,
                           x.If_dkName,
                       };
            }
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="workarea"></param>
        /// <param name="isono"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string workarea, string isono, string jointDesc, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }
    }
}
