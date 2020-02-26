using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class HotProessManageEditService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数2
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量:焊接日报
        /// </summary>
        private static IQueryable<Model.View_HotProessItem> qq = from x in db.View_HotProessItem where x.JOT_HotRpt == null && x.DReportID != null && x.IS_Proess=="1" orderby x.JOT_JointNo select x;

        public static IEnumerable GetListData(string iso_id, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_HotProessItem> q = qq;

            if (!string.IsNullOrEmpty(iso_id))
            {
                q = q.Where(e => e.ISO_ID == iso_id);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.JOT_ID,
                       x.ISO_ID,
                       x.JOT_JointNo,
                       x.JOT_WeldDate
                   };
        }

        public static int GetListCount(string iso_id)
        {
            return count;
        }

        /// <summary>
        /// 根据热处理Id获取用于热处理信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.HotProess GetHotProessByID(string HotProessId)
        {
            Model.HJGLDB db = Funs.DB;
            var view = db.HotProess.FirstOrDefault(e => e.HotProessId == HotProessId);
            return view;
        }

        /// <summary>
        /// 根据热处理Id获取用于热处理信息明细
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_HotProessItem> GetHotProessItemByID(string HotProessId)
        {
            Model.HJGLDB db = Funs.DB;
            var view = (from x in db.View_HotProessItem where x.HotProessId == HotProessId select x).ToList();
            return view;
        }
       
        /// <summary>
        /// 增加热处理信息
        /// </summary>
        /// <param name="hotProess">热处理实体</param>
        public static void AddHotProess(Model.HotProess hotProess)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotProess newTestPackage = new Model.HotProess();
            newTestPackage.HotProessId = hotProess.HotProessId;
            newTestPackage.HotProessNo = hotProess.HotProessNo;
            newTestPackage.ProessDate = hotProess.ProessDate;           
            newTestPackage.ProjectId = hotProess.ProjectId;
            newTestPackage.InstallationId = hotProess.InstallationId;
            newTestPackage.UnitId = hotProess.UnitId;
            newTestPackage.Tabler = hotProess.Tabler;
            newTestPackage.Remark = hotProess.Remark;
            newTestPackage.ProessMethod = hotProess.ProessMethod;
            newTestPackage.ProessEquipment = hotProess.ProessEquipment;
            db.HotProess.InsertOnSubmit(newTestPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改热处理信息
        /// </summary>
        /// <param name="weldReport">热处理实体</param>
        public static void UpdateHotProess(Model.HotProess hotProess)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotProess newTestPackage = db.HotProess.First(e => e.HotProessId == hotProess.HotProessId);
            newTestPackage.HotProessId = hotProess.HotProessId;
            newTestPackage.HotProessNo = hotProess.HotProessNo;
            newTestPackage.ProessDate = hotProess.ProessDate;
            newTestPackage.ProjectId = hotProess.ProjectId;
            newTestPackage.InstallationId = hotProess.InstallationId;
            newTestPackage.UnitId = hotProess.UnitId;
            newTestPackage.Tabler = hotProess.Tabler;
            newTestPackage.Remark = hotProess.Remark;
            newTestPackage.ProessMethod = hotProess.ProessMethod;
            newTestPackage.ProessEquipment = hotProess.ProessEquipment;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除热处理信息
        /// </summary>
        /// <param name="hotProessID">热处理主键</param>
        public static void DeleteHotProessByHotProessID(string hotProessID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotProess hotProess = db.HotProess.First(e => e.HotProessId == hotProessID);
            db.HotProess.DeleteOnSubmit(hotProess);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除热处理信息明细
        /// </summary>
        /// <param name="hotProessID">热处理主键</param>
        public static void DeleteHotProessItemByHotProessId(string HotProessId)
        {
            Model.HJGLDB db = Funs.DB;
            var items = from x in db.HotProessItem where x.HotProessId == HotProessId select x;
            if (items != null)
            {
                foreach (var item in items)
                {
                    Model.PW_JointInfo info = new Model.PW_JointInfo();
                    info.JOT_ID = item.JOT_ID;
                    info.JOT_ProessDate = null;
                    info.JOT_HotRpt = null;
                    UpdateJointInfo(info);
                }

                db.HotProessItem.DeleteAllOnSubmit(items);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加热处理信息明细
        /// </summary>
        /// <param name="HotProessItem">热处理明细实体</param>
        public static void AddHotProessItem(Model.HotProessItem HotProessItem,string HotProessNo,string date)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotProessItem newHotProessItem = new Model.HotProessItem();
            string itemId = SQLHelper.GetNewID(typeof(Model.HotProessItem));
            newHotProessItem.HotProessItemId = itemId;
            newHotProessItem.HotProessId = HotProessItem.HotProessId;
            newHotProessItem.JOT_ID = HotProessItem.JOT_ID;
            newHotProessItem.PointCount = HotProessItem.PointCount;
            newHotProessItem.RequiredT = HotProessItem.RequiredT;
            newHotProessItem.ActualT = HotProessItem.ActualT;
            newHotProessItem.RequestTime = HotProessItem.RequestTime;
            newHotProessItem.ActualTime = HotProessItem.ActualTime;
            newHotProessItem.RecordChartNo = HotProessItem.RecordChartNo;
            newHotProessItem.HardnessReportNo = HotProessItem.HardnessReportNo;
           
            db.HotProessItem.InsertOnSubmit(newHotProessItem);
            db.SubmitChanges();
            ////更新焊口信息
            Model.PW_JointInfo info = new Model.PW_JointInfo();
            info.JOT_ID = HotProessItem.JOT_ID;
            info.JOT_HotRpt = HotProessNo;
            if (!String.IsNullOrEmpty(date))
            {
                info.JOT_ProessDate = DateTime.Parse(date);
            }
            UpdateJointInfo(info);
        }

         /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void UpdateJointInfo(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo newJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jointInfo.JOT_ID);
            newJointInfo.JOT_HotRpt = jointInfo.JOT_HotRpt;
            newJointInfo.JOT_ProessDate = jointInfo.JOT_ProessDate;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位Id获取热处理数
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetHotProessByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.HotProess where x.UnitId == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据装置Id获取热处理数
        /// </summary>
        /// <param name="installationId"></param>
        /// <returns></returns>
        public static int GetHotProessByInstallationId(int installationId)
        {
            var q = (from x in Funs.DB.HotProess where x.InstallationId == installationId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据焊口获取热处理
        /// </summary>
        /// <param name="jotno"></param>
        /// <returns></returns>
        public static int GetHotProessByJotId(string jotno)
        {
            var q = (from x in Funs.DB.HotProessItem join y in Funs.DB.PW_JointInfo on x.JOT_ID equals y.JOT_ID where y.JOT_JointNo==jotno select x).ToList();
            return q.Count();
        }
    }
}
