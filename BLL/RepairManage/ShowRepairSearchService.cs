using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class ShowRepairSearchService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.View_CH_RepairSearch> qq = from x in db.View_CH_RepairSearch                                                                   
                                                                   orderby x.ISO_IsoNo ,x.JOT_JointNo 
                                                                   select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string[] checkList, string CH_TrustID)
        {
            IQueryable<Model.View_CH_RepairSearch> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            if (!string.IsNullOrEmpty(CH_TrustID))
            {
                q = q.Where(e => e.RepairTrustId == null || e.RepairTrustId == CH_TrustID);
            }
            else
            {
                q = q.Where(e => e.RepairTrustId == null);
            }
            if (checkList != null)
            {
                q = q.Where(e => checkList.ToList().Contains(e.CHT_CheckID));
            }

            if (q.Count() == 0)
            {
                return new object[] { "" };
            }
            return from x in q
                   select new
                   {
                       x.JOT_ID,
                       x.ISO_ID,
                       x.ISO_IsoNo,
                       x.BSU_ID,
                       x.WorkAreaId,
                       x.ProjectId,                     
                       x.JOT_JointNo,
                       x.JOT_JointStatus,
                   };
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData2(string projectId, string[] jotList, bool ckISO, bool ckWeld, string workAreaId, string unitId)
        {
            IEnumerable<Model.PW_JointInfo> q = GetSpRepairSearchItems(projectId, jotList, ckISO, ckWeld, workAreaId, unitId);           
            return from x in q
                   select new
                   {
                       x.JOT_ID,                       
                       x.ISO_ID,
                       x.JOT_JointNo,
                   };
        }

       /// <summary>
        /// 查询出满足条件的焊口集合
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="jotList"></param>
       /// <param name="ckISO"></param>
       /// <param name="ckWeld"></param>
       /// <returns></returns>
        private static IEnumerable<Model.PW_JointInfo> GetSpRepairSearchItems(string projectId, string[] jotList, bool ckISO, bool ckWeld, string workAreaId, string unitId)
        {
            Model.HJGLDB db = Funs.DB;
            List<Model.PW_JointInfo> SpRepairSearchItems = new List<Model.PW_JointInfo>();
            if (jotList != null)
            {
                var IsoInfo = from x in db.PW_IsoInfo where x.ProjectId == projectId && x.BAW_ID == workAreaId && x.BSU_ID == unitId select x;
                if (ckISO)
                {
                    var isoList = (from x in db.PW_JointInfo where jotList.ToList().Contains(x.JOT_ID) select x.ISO_ID).Distinct().ToList();
                    IsoInfo = IsoInfo.Where(x => isoList.Contains(x.ISO_ID));         
                }

                if (IsoInfo.Count() > 0)
                {
                    foreach (var isoItem in IsoInfo)
                    {
                        var jots = from x in db.PW_JointInfo where x.ISO_ID == isoItem.ISO_ID && x.JOT_JointStatus == "100" && x.DReportID != null select x;
                        if (ckWeld)
                        {
                            var weldList = (from x in db.PW_JointInfo where jotList.ToList().Contains(x.JOT_ID) select x.JOT_CellWelder).Distinct().ToList();
                            jots = jots.Where(x => weldList.Contains(x.JOT_CellWelder)).Distinct();
                        }

                        if (jots.Count() > 0)
                        {
                            foreach (var jotItem in jots)
                            {
                                if (!jotList.Contains(jotItem.JOT_ID))
                                {
                                    Model.PW_JointInfo SpRepairSearchItem = new Model.PW_JointInfo();
                                    SpRepairSearchItem.JOT_ID = jotItem.JOT_ID;
                                    SpRepairSearchItem.JOT_JointNo = jotItem.JOT_JointNo;
                                    SpRepairSearchItem.ISO_ID = jotItem.ISO_ID;
                                    SpRepairSearchItem.JOT_CellWelder = jotItem.JOT_CellWelder;
                                    SpRepairSearchItems.Add(SpRepairSearchItem);
                                }
                            }
                        }
                    }
                }
            }
            return SpRepairSearchItems;
        }

        /// <summary>
        /// 根据单位Id获取返修数
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public static int GetRepairByUnitId(string unitid)
        {
            var q = (from x in Funs.DB.CH_Repair where x.BSU_ID == unitid select x).ToList();
            return q.Count();
        }
    }
}
