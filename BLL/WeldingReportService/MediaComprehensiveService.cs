using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 介质综合分析
    /// </summary>
    public static class MediaComprehensiveService
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
        /// <param name="sername"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitcode, string workareacode, string sername, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {
            if (unitcode=="0")
            {
                unitcode = null;
            }
            if (workareacode=="0")
            {
                workareacode = null;
            }
            if (sername=="0")
            {
                sername = null;
            }

            if (flag == "0" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IEnumerable<Model.SpRptService> qq = Funs.DB.SpRptService(unitcode, workareacode, sername, projectId, supervisorUnitId);
                var q = qq.ToList();
                count = q.Count();

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.bsu_id,
                           x.baw_id,
                           x.ser_id,
                           x.bsu_unitcode,
                           x.bsu_unitname,
                           x.baw_areano,
                           x.ser_code,
                           x.ser_name,
                           x.total_jot,
                           x.total_sjot,
                           x.total_fjot,
                           x.finished_jot,
                           x.finished_sjot,
                           x.finished_fjot,
                           x.cut_jot,
                           finisedrate = x.finisedrate * 100,
                           finisedrate_s = x.finisedrate_s * 100,
                           finisedrate_f = x.finisedrate_f * 100,
                           x.total_din,
                           x.total_sdin,
                           x.total_fdin,
                           x.finished_din,
                           x.finished_sdin,
                           x.finished_fdin,
                           finishedrate_din = x.finishedrate_din * 100,
                           finishedrate_sdin = x.finishedrate_sdin * 100,
                           finishedrate_fdin = x.finishedrate_fdin * 100,
                           x.total_film,
                           x.pass_film,
                           passfilm_rate = x.passfilm_rate * 100,
                           x.ext_totalfilm,
                           x.ext_passfilm,
                           ext_passrate = x.ext_passrate * 100,
                           x.point_totalfilm,
                           x.point_passfilm,
                           point_passrate = x.point_passrate * 100,
                           x.cut_totalfilm,
                           x.cut_passfilm,
                           x.trust_total_jot,
                           x.trust_ext_total_jot,
                           x.trust_point_total_jot,
                           x.check_point_total_jot,
                           x.repair_jot
                       };
            }
        }
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="workareacode"></param>
        /// <param name="sername"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string unitcode, string workareacode, string sername, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }
    }
}
