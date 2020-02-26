using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 管线综合分析
    /// </summary>
    public static class IsoCmprehensiveService
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
        /// <param name="unitNo"></param>
        /// <param name="isoNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="steel"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitNo, string isoNo, string areaNo, string steel, string projectId, string flag,string supervisorUnitId,  int startRowIndex, int maximumRows)
        {
            if (unitNo=="0")
            {
                unitNo = null;
            }
            if (isoNo=="")
            {
                isoNo = null;
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
                IEnumerable<Model.SpRptIsoAnalyze> qq = Funs.DB.SpRptIsoAnalyze(unitNo, isoNo, areaNo, steel,projectId, supervisorUnitId);
                var q = qq.ToList();
                count = q.Count();
                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.iso_id,
                           x.iso_isono,
                           x.baw_areano,
                           x.bsu_unitcode,
                           x.bsu_unitname,
                           maxdate = string.Format("{0:yyyy-MM-dd}", x.maxdate),
                           x.total_jot,
                           x.total_sjot,
                           x.total_fjot,
                           x.finished_total_jot,
                           x.finished_total_sjot,
                           x.finished_total_fjot,
                           x.cut_total_jot,
                           finisedrate = x.finisedrate * 100,
                           finisedrate_s = x.finisedrate_s * 100,
                           finisedrate_f = x.finisedrate_f * 100,
                           x.total_din,
                           x.total_Sdin,
                           x.total_Fdin,
                           x.finished_total_din,
                           x.finished_total_Sdin,
                           x.finished_total_Fdin,
                           finisedrate_din = x.finisedrate_din * 100,
                           finisedrate_din_s = x.finisedrate_din_s * 100,
                           finisedrate_din_f = x.finisedrate_din_f * 100,
                           x.total_film,
                           x.pass_film,
                           passreate = x.passreate * 100,
                           x.ext_total_film,
                           x.ext_pass_film,
                           ext_passreate = x.ext_passreate * 100,
                           x.point_total_film,
                           x.point_pass_film,
                           point_passreate = x.point_passreate * 100,
                           x.cut_total_film,
                           x.cut_pass_film,
                           x.ext_jot,
                           x.point_jot,
                           x.trust_total_jot,
                           x.check_total_jot,
                           x.total_repairjot,
                           x.source_rate,
                           trustrate = x.trustrate * 100,
                           checkrate = x.checkrate * 100,
                           FixedCheckRate = x.FixedCheckRate * 100
                       };
            }
        }
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="unitNo"></param>
        /// <param name="isoNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="steel"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string unitNo, string isoNo, string areaNo, string steel, string projectId, string flag,string supervisorUnitId)
        {
            return count;
        }
    }
}
