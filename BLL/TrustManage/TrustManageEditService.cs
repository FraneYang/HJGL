using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TrustManageEditService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.View_CH_TrustItem> qq = from x in db.View_CH_TrustItem
                                                                orderby x.ISO_IsoNo, x.JOT_JointNo 
                                                                select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string startTime, string endTime, string CH_TrustID, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_CH_TrustItem> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            if (!String.IsNullOrEmpty(startTime))
            {
                q = q.Where(e => e.CH_TrustDate >= Convert.ToDateTime(startTime));
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                q = q.Where(e => e.CH_TrustDate <= Convert.ToDateTime(endTime));
            }
            if (!string.IsNullOrEmpty(CH_TrustID))
            {
                q = q.Where(e => e.CH_TrustID.Contains(CH_TrustID));
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.CH_TrustItemID,
                       x.CH_TrustID,
                       x.JOT_ID,
                       x.BAW_ID,
                       x.InstallationId,
                       x.ProjectId,
                       x.CH_TrustDate,
                       x.ISO_IsoNo,
                       x.JOT_JointNo,
                       x.CH_Remark,
                       x.JOT_Dia,
                       x.JOT_Sch,
                       x.WLO_Code,
                       x.WME_Name
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId, string startTime, string endTime, string DReportID)
        {
            return count;
        }

        /// <summary>
        /// 根据委托Id获取用于委托的委托信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.CH_Trust GetCH_TrustByID(string CH_TrustID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = db.CH_Trust.FirstOrDefault(e => e.CH_TrustID == CH_TrustID);
            return view;
        }

        /// <summary>
        /// 委托单号是否存在
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="trustCode"></param>
        /// <returns></returns>
        public static bool IsExistTrustCode(string projectId, string trustCode)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.CH_Trust where x.ProjectId == projectId && x.CH_TrustCode == trustCode select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据委托Id获取用于委托的焊口视图信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_CH_TrustItem> GetView_CH_TrustItemByCH_TrustID(string CH_TrustID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = (from x  in db.View_CH_TrustItem where x.CH_TrustID == CH_TrustID select x).ToList();
            return view;
        }

        /// <summary>
        /// 根据焊口Id获取用于委托的焊口视图信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.CH_TrustItem> GetCH_TrustItemByJOT_ID(string jot_id)
        {
            Model.HJGLDB db = Funs.DB;
            var view = (from x in db.CH_TrustItem where x.JOT_ID == jot_id select x).ToList();
            return view;
        }

        /// <summary>
        /// 根据焊口Id和委托id获取用于委托明细是否存在
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.CH_TrustItem GetCH_TrustItemByJOT_IDAndJotId(string CH_TrustID,string jot_id)
        {
            Model.HJGLDB db = Funs.DB;
            var view = db.CH_TrustItem.FirstOrDefault(x=>x.CH_TrustID == CH_TrustID && x.JOT_ID == jot_id );
            return view;
        }

        /// <summary>
        /// 根据委托Id获取用于委托的焊口视图信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.CH_TrustItem> GetCH_TrustItemByCH_TrustID(string CH_TrustID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = (from x in db.CH_TrustItem where x.CH_TrustID == CH_TrustID select x).ToList();
            return view;
        }

        /// <summary>
        /// 根据焊口Id获取用于委托的焊口信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.View_CH_TrustItem GetView_CH_TrustItemByJotID(string jot_id, string projectId)
        {
            var view = Funs.DB.View_CH_TrustItem.FirstOrDefault(e => e.JOT_ID == jot_id && e.ProjectId == projectId);
            return view;
        }

        /// <summary>
        /// 增加委托信息
        /// </summary>
        /// <param name="cH_Trust">委托实体</param>
        public static void AddCH_Trust(Model.CH_Trust cH_Trust)
        {
            Model.HJGLDB db = Funs.DB;
            Model.CH_Trust newCH_Trust = new Model.CH_Trust();
            newCH_Trust.CH_TrustID = cH_Trust.CH_TrustID;
            newCH_Trust.CH_TrustCode = cH_Trust.CH_TrustCode;
            newCH_Trust.CH_TrustUnit = cH_Trust.CH_TrustUnit;
            newCH_Trust.CH_TrustDate = cH_Trust.CH_TrustDate;
            newCH_Trust.CH_TrustType = cH_Trust.CH_TrustType;
            newCH_Trust.CH_TrustMan = cH_Trust.CH_TrustMan;
            newCH_Trust.CH_Tabler = cH_Trust.CH_Tabler;
            newCH_Trust.CH_TableDate = cH_Trust.CH_TableDate;            
            newCH_Trust.CH_UnitName = cH_Trust.CH_UnitName;
            newCH_Trust.CH_WorkNo = cH_Trust.CH_WorkNo;
            newCH_Trust.CH_ItemName = cH_Trust.CH_ItemName;
            newCH_Trust.CH_SlopeType = cH_Trust.CH_SlopeType;
            newCH_Trust.CH_ServiceTemp = cH_Trust.CH_ServiceTemp;
            newCH_Trust.CH_Press = cH_Trust.CH_Press;
            newCH_Trust.CH_WeldMethod = cH_Trust.CH_WeldMethod;
            newCH_Trust.CH_NDTRate = cH_Trust.CH_NDTRate;
            newCH_Trust.CH_NDTMethod = cH_Trust.CH_NDTMethod;
            newCH_Trust.CH_NDTCriteria = cH_Trust.CH_NDTCriteria;
            newCH_Trust.CH_AcceptGrade = cH_Trust.CH_AcceptGrade;
            newCH_Trust.CH_Remark = cH_Trust.CH_Remark;
            newCH_Trust.CH_CheckUnit = cH_Trust.CH_CheckUnit;
            newCH_Trust.ProjectId = cH_Trust.ProjectId;
            newCH_Trust.InstallationId = cH_Trust.InstallationId;
            newCH_Trust.CH_RequestDate = cH_Trust.CH_RequestDate;
            newCH_Trust.ToIso_Id = cH_Trust.ToIso_Id;

            db.CH_Trust.InsertOnSubmit(newCH_Trust);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接信息
        /// </summary>
        /// <param name="weldReport">焊接实体</param>
        public static void UpdateCH_Trust(Model.CH_Trust cH_Trust)
        {
            Model.HJGLDB db = Funs.DB;
            Model.CH_Trust newCH_Trust = db.CH_Trust.First(e => e.CH_TrustID == cH_Trust.CH_TrustID);
            newCH_Trust.CH_TrustID = cH_Trust.CH_TrustID;
            newCH_Trust.CH_TrustCode = cH_Trust.CH_TrustCode;
            newCH_Trust.CH_TrustUnit = cH_Trust.CH_TrustUnit;
            newCH_Trust.CH_TrustDate = cH_Trust.CH_TrustDate;
            newCH_Trust.CH_TrustType = cH_Trust.CH_TrustType;
            newCH_Trust.CH_TrustMan = cH_Trust.CH_TrustMan;
            newCH_Trust.CH_Tabler = cH_Trust.CH_Tabler;
            newCH_Trust.CH_TableDate = cH_Trust.CH_TableDate;           
            newCH_Trust.CH_UnitName = cH_Trust.CH_UnitName;
            newCH_Trust.CH_WorkNo = cH_Trust.CH_WorkNo;
            newCH_Trust.CH_ItemName = cH_Trust.CH_ItemName;
            newCH_Trust.CH_SlopeType = cH_Trust.CH_SlopeType;
            newCH_Trust.CH_ServiceTemp = cH_Trust.CH_ServiceTemp;
            newCH_Trust.CH_Press = cH_Trust.CH_Press;
            newCH_Trust.CH_WeldMethod = cH_Trust.CH_WeldMethod;
            newCH_Trust.CH_NDTRate = cH_Trust.CH_NDTRate;
            newCH_Trust.CH_NDTMethod = cH_Trust.CH_NDTMethod;
            newCH_Trust.CH_NDTCriteria = cH_Trust.CH_NDTCriteria;
            newCH_Trust.CH_AcceptGrade = cH_Trust.CH_AcceptGrade;
            newCH_Trust.CH_Remark = cH_Trust.CH_Remark;
            newCH_Trust.CH_CheckUnit = cH_Trust.CH_CheckUnit;
            newCH_Trust.ProjectId = cH_Trust.ProjectId;
            newCH_Trust.InstallationId = cH_Trust.InstallationId;
            newCH_Trust.CH_RequestDate = cH_Trust.CH_RequestDate;
            newCH_Trust.ToIso_Id = cH_Trust.ToIso_Id;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除委托信息
        /// </summary>
        /// <param name="cH_TrustID">委托主键</param>
        public static void DeleteCH_TrustByCH_TrustID(string cH_TrustID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.CH_Trust cH_Trust = db.CH_Trust.First(e => e.CH_TrustID == cH_TrustID);
            db.CH_Trust.DeleteOnSubmit(cH_Trust);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除委托信息明细
        /// </summary>
        /// <param name="cH_TrustID">委托主键</param>
        public static void DeleteCH_TrustItemByCH_TrustID(string cH_TrustID)
        {           
            Model.HJGLDB db = Funs.DB;
            var cH_Trust = from x in db.CH_TrustItem where x.CH_TrustID == cH_TrustID select x;
            if (cH_Trust != null)
            {
                foreach (var item in cH_Trust)
                {
                    var jo = Funs.DB.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == item.JOT_ID);
                    if (jo != null && jo.JOT_JointStatus == "102")
                    {
                        var ch = Funs.DB.CH_CheckItem.FirstOrDefault(x=>x.JOT_ID == jo.JOT_ID);
                        if (ch == null)
                        {
                            jo.JOT_JointStatus = "100";
                            BLL.PW_JointInfoService.UpdateJointPoint(jo);
                        }
                    }
                }

                db.CH_TrustItem.DeleteAllOnSubmit(cH_Trust);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加委托信息明细
        /// </summary>
        /// <param name="trustItem">委托明细实体</param>
        public static void AddCH_TrustItem(Model.CH_TrustItem trustItem)
        {
            Model.HJGLDB db = Funs.DB;
            Model.CH_TrustItem newTrustItem = new Model.CH_TrustItem();

            newTrustItem.CH_TrustItemID = SQLHelper.GetNewID(typeof(Model.CH_TrustItem));
            newTrustItem.CH_TrustID = trustItem.CH_TrustID;
            newTrustItem.JOT_ID = trustItem.JOT_ID;
            
            newTrustItem.CH_Remark = trustItem.CH_Remark;
            db.CH_TrustItem.InsertOnSubmit(newTrustItem);
            db.SubmitChanges();
        }


        /// <summary>
        /// 审核委托信息
        /// </summary>
        /// <param name="weldReport">焊接实体</param>
        public static void AuditCH_Trust(Model.CH_Trust cH_Trust)
        {
            Model.HJGLDB db = Funs.DB;
            Model.CH_Trust newCH_Trust = db.CH_Trust.First(e => e.CH_TrustID == cH_Trust.CH_TrustID);
            newCH_Trust.CH_TrustID = cH_Trust.CH_TrustID;
            newCH_Trust.CH_AuditMan = cH_Trust.CH_AuditMan;
            newCH_Trust.CH_AuditDate = cH_Trust.CH_AuditDate; 
            db.SubmitChanges();
        }

        /// <summary>
        /// 合格等级下拉列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetAcceptGradeList()
        {
            ListItem[] list = new ListItem[5];
            list[0] = new ListItem("Ⅰ", "1");
            list[1] = new ListItem("Ⅱ", "2");
            list[2] = new ListItem("Ⅲ", "3");
            list[3] = new ListItem("Ⅳ", "4");
            list[4] = new ListItem("Ⅴ", "5");
            return list;
        }

        /// <summary>
        /// 更新焊口委托情况
        /// </summary>
        /// <param name="type">type1-录入审核2取消删除</param>
        public static void UpdateJOT_TrustFlag(string JOT_ID, string type)
        {
            var jointInfo = Funs.DB.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == JOT_ID);           
            if (jointInfo != null)
            {
                if (type == "1")
                {
                    if (String.IsNullOrEmpty(jointInfo.JOT_TrustFlag) || jointInfo.JOT_TrustFlag =="00")
                    {
                        jointInfo.JOT_TrustFlag ="01";
                    }
                    else if (jointInfo.JOT_TrustFlag == "01")
                    {
                        jointInfo.JOT_TrustFlag = "02";
                    }
                    else if (jointInfo.JOT_TrustFlag == "02")
                    {
                        jointInfo.JOT_TrustFlag = "11";
                    }
                    else if (jointInfo.JOT_TrustFlag == "11")
                    {
                        jointInfo.JOT_TrustFlag = "12";
                    }
                    else if (jointInfo.JOT_TrustFlag == "12")
                    {
                        jointInfo.JOT_TrustFlag = "21";
                    }
                    else if (jointInfo.JOT_TrustFlag == "21")
                    {
                        jointInfo.JOT_TrustFlag = "22";
                    }
                }
                else
                {
                    if (jointInfo.JOT_TrustFlag == "22")
                    {
                        jointInfo.JOT_TrustFlag = "21";
                    }
                    else if (jointInfo.JOT_TrustFlag == "21")
                    {
                        jointInfo.JOT_TrustFlag = "12";
                    }
                    else if (jointInfo.JOT_TrustFlag == "12")
                    {
                        jointInfo.JOT_TrustFlag = "11";
                    }
                    else if (jointInfo.JOT_TrustFlag == "11")
                    {
                        jointInfo.JOT_TrustFlag = "02";
                    }
                    else if (jointInfo.JOT_TrustFlag == "02")
                    {
                        jointInfo.JOT_TrustFlag = "01";
                    }
                    else
                    {
                        jointInfo.JOT_TrustFlag = "00";
                    }
                }
            }            

            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据装置Id获取委托数
        /// </summary>
        /// <param name="installationId"></param>
        /// <returns></returns>
        public static int GetTrustByInstallationId(int installationId)
        {
            var q = (from x in Funs.DB.CH_Trust where x.InstallationId == installationId select x).ToList();
            return q.Count();
        }
    }
}
