using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 预制安装进度
    /// </summary>
    public static class PrefabricatedInstallService
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
        /// <param name="areaNo"></param>
        /// <param name="steel"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitcode, string areaNo, string steel, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {
            if (unitcode=="0")
            {
                unitcode = null;
            }
            if (areaNo=="0")
            {
                areaNo = null;
            }
            if (steel=="0")
            {
                steel = null;
            }

            if (flag == "0" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IEnumerable<Model.SpRptIsoYzazjd> qq = Funs.DB.SpRptIsoYzazjd(unitcode, areaNo, steel,projectId,supervisorUnitId);
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
                           x.baw_areano,
                           x.ste_stecode,
                           x.ste_stename,
                           x.max_din,
                           x.total_din,
                           x.finished_total_din,
                           finisedrate_din = x.finisedrate_din * 100,
                           x.total_Sdin,
                           x.finished_total_Sdin,
                           finisedrate_din_s = x.finisedrate_din_s * 100,
                           x.total_Fdin,
                           x.finished_total_Fdin,
                           finisedrate_din_f = x.finisedrate_din_f * 100
                       };
            }
        }
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="areaNo"></param>
        /// <param name="steel"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string unitcode, string areaNo, string steel, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }
    }
}
