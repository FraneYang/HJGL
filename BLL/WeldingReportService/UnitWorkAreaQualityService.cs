using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 单位工区质量分析
    /// </summary>
    public static class UnitWorkAreaQualityService
    {
        public static Model.HJGLDB db = Funs.DB;

        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 集合
        /// </summary>
        private static List<Model.SpRptBawZlfx> RptBawZlfx
        {
            get;
            set;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="unitNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="installationId"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string unitNo, string areaNo, int? installationId, DateTime? date1, DateTime? date2, string ste_steeltype, string projectId, string flag, string supervisorUnitId, int startRowIndex, int maximumRows)
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
               // getNewSp(unitNo, areaNo, installationId, date1, date2, ste_steeltype, projectId);
                RptBawZlfx = (Funs.DB.SpRptBawZlfx(unitNo, areaNo, installationId, date1, date2, ste_steeltype, projectId, supervisorUnitId)).ToList();
                IEnumerable<Model.SpRptBawZlfx> qq = RptBawZlfx;
                var q = qq.ToList();
                count = q.Count();

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.bsu_unitcode,
                           x.bsu_unitname,
                           x.devicecode,
                           x.devicename,
                           x.baw_areano,
                           x.total_jot,
                           x.total_sjot,
                           x.total_fjot,
                           x.finished_total_jot,
                           x.finished_total_sjot,
                           x.finished_total_fjot,
                           x.current_total_film,
                           x.current_pass_film,
                           current_passreate = x.current_passreate * 100,
                           x.current_point_total_film,
                           x.current_point_pass_film,
                           cuurent_point_passreate = x.cuurent_point_passreate * 100,
                           x.current_ext_total_film,
                           x.current_ext_pass_film,
                           current_ext_passreate = x.current_ext_passreate * 100,
                           x.current_trust_count_total,
                           x.current_check_count_total,
                           x.total_film,
                           x.pass_film,
                           passreate = x.passreate * 100,
                           x.point_total_film,
                           x.point_pass_film,
                           point_passreate = x.point_passreate * 100,
                           x.ext_total_film,
                           x.ext_pass_film,
                           ext_passreate = x.ext_passreate * 100,
                           x.trust_count_total,
                           x.point_count_total,
                           x.extend_count_total,
                           x.repair_count_total,
                           x.trust_check_total
                       };
            }
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="unitNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="installationId"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string unitNo, string areaNo, int? installationId, DateTime? date1, DateTime? date2, string ste_steeltype, string projectId, string flag, string supervisorUnitId)
        {
            return count;
        }

        /// <summary>
        /// 查询结果集合
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<Model.SpRptBawZlfx> GetRptBawZlfx()
        {
            return RptBawZlfx;
        }
    }
}
