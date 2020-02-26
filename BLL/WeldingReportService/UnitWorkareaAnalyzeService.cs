using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 单位工区进度分析
    /// </summary>
    public static class UnitWorkareaAnalyzeService
    {
        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 集合
        /// </summary>
        private static List<Model.SpRptUnitBawAnalyze> UnitBawAnalyze
        {
            get;
            set;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="unitNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="installationCode"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="ste_steeltype"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitNo, string areaNo, int? installationId, string ste_steeltype, DateTime? startTime, DateTime? endTime, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
        {
            if (unitNo == "0")
            {
                unitNo = null;
            }
            if (areaNo == "0")
            {
                areaNo = null;
            }
            if (installationId == 0)
            {
                installationId = null;
            }
            if (ste_steeltype == "0")
            {
                ste_steeltype = null;
            }

            if (flag == "0" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                UnitBawAnalyze = (Funs.DB.SpRptUnitBawAnalye(unitNo, areaNo, installationId, ste_steeltype, startTime, endTime, projectId,supervisorUnitId)).ToList();
                IEnumerable<Model.SpRptUnitBawAnalyze> qq = UnitBawAnalyze;
                var q = qq.ToList();
                if (q.Count() == 0)
                {
                    count = 0;
                    return new object[] { "" };
                }
                count = q.Count();
              
                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.baw_areano,
                           x.bsu_unitcode,
                           x.bsu_unitname,
                           x.InstallationCode,
                           x.InstallationName,
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
                           finisedrate_bq = x.finisedrate_bq * 100,
                           finisedrate_s_bq = x.finisedrate_s_bq * 100,
                           finisedrate_f_bq = x.finisedrate_f_bq * 100,
                           x.finished_total_din_bq,
                           x.finished_total_Sdin_bq,
                           x.finished_total_Fdin_bq,
                           finisedrate_din_bq = x.finisedrate_din_bq * 100,
                           finisedrate_din_s_bq = x.finisedrate_din_s_bq * 100,
                           finisedrate_din_f_bq = x.finisedrate_din_f_bq * 100,
                           x.finished_total_jot_bq,
                           x.finished_total_sjot_bq,
                           x.finished_total_fjot_bq
                       };
            }
        }


        /// <summary>
        /// 列表数量
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetListCount(string unitNo, string areaNo, int? installationId, string ste_steeltype, DateTime? startTime, DateTime? endTime, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }

        /// <summary>
        /// 查询结果集合
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<Model.SpRptUnitBawAnalyze> GetUnitBawAnalyze()
        {
            return UnitBawAnalyze;
        }
    }

}
