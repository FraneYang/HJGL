using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 管线综合信息
    /// </summary>
    public class IsoCompreInfoService
    {

        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        private static IQueryable<Model.View_IsoinfoView> qq = from x in db.View_IsoinfoView select x;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="workarea"></param>
        /// <param name="isono"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string workarea, string isono, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {
            if (flag != "1" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IQueryable<Model.View_IsoinfoView> q = qq;
                q = q.Where(e => e.ProjectId == projectId);
                if (!string.IsNullOrEmpty(workarea) && workarea != "0")
                {
                    q = q.Where(e => e.WorkAreaId == workarea);
                }
                if (!string.IsNullOrEmpty(isono))
                {
                    q = q.Where(e => e.Iso_isono == isono);
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
                           x.UnitName,
                           x.Iso_isono,
                           x.ISO_TotalDin,
                           x.Jot_count,
                           x.Ser_name,
                           x.Ndtr_rate,
                           x.Ndt_name,
                           x.ISO_NDTClass,
                           x.Ste_name,
                           x.ISO_Specification,
                           x.ISO_DesignPress,
                           x.ISO_DesignTemperature,
                           x.ISO_TestPress,
                           x.ISO_TestTemperature,
                           x.WorkAreaCode,
                           x.ISO_SysNo,
                           x.ISO_SubSysNo,
                           x.ISO_CwpNo,
                           x.ISO_IsoNumber,
                           x.Is_proess,
                           x.PTP_TestPackageNo,
                           x.PTP_TableDate
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
        public static int GetListCount(string workarea, string isono, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }
    }
}
