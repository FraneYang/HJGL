using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class HotHardManageEditService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 根据委托Id获取用于委托的委托信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.HotHard GetHotHardByID(string HotHardID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = db.HotHard.FirstOrDefault(e => e.HotHardID == HotHardID);
            return view;
        }       

        /// <summary>
        /// 根据委托Id获取用于委托的焊口视图信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_HotHardItem> GetView_HotHardItemByHotHardID(string HotHardID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = (from x in db.View_HotHardItem where x.HotHardID == HotHardID select x).ToList();
            return view;
        }

        /// <summary>
        /// 根据焊口Id获取用于委托的焊口信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.View_HotHardItem GetView_HotHardItemByJotID(string jot_id, string projectId)
        {
            var view = Funs.DB.View_HotHardItem.FirstOrDefault(e => e.JOT_ID == jot_id && e.ProjectId == projectId);
            return view;
        }

        /// <summary>
        /// 增加委托信息
        /// </summary>
        /// <param name="HotHard">委托实体</param>
        public static void AddHotHard(Model.HotHard HotHard)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotHard newHotHard = new Model.HotHard();
            newHotHard.HotHardID = HotHard.HotHardID;
            newHotHard.HotHardCode = HotHard.HotHardCode;
            newHotHard.HotHardUnit = HotHard.HotHardUnit;
            newHotHard.HotHardDate = HotHard.HotHardDate;
            newHotHard.HotHardMan = HotHard.HotHardMan;           
           
            newHotHard.NDTMethod = HotHard.NDTMethod;           
            newHotHard.CheckUnit = HotHard.CheckUnit;
            newHotHard.ProjectId = HotHard.ProjectId;
            newHotHard.InstallationId = HotHard.InstallationId;           
            newHotHard.DetectionTime = HotHard.DetectionTime;

            newHotHard.NDTRate = HotHard.NDTRate;
            newHotHard.Sendee = HotHard.Sendee;
            newHotHard.Standards = HotHard.Standards;
            newHotHard.InspectionNum = HotHard.InspectionNum;
            newHotHard.CheckNum = HotHard.CheckNum;
            newHotHard.TestWeldNum = HotHard.TestWeldNum;

            db.HotHard.InsertOnSubmit(newHotHard);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接信息
        /// </summary>
        /// <param name="weldReport">焊接实体</param>
        public static void UpdateHotHard(Model.HotHard HotHard)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotHard newHotHard = db.HotHard.First(e => e.HotHardID == HotHard.HotHardID);
            newHotHard.HotHardID = HotHard.HotHardID;
            newHotHard.HotHardCode = HotHard.HotHardCode;
            newHotHard.HotHardUnit = HotHard.HotHardUnit;
            newHotHard.HotHardDate = HotHard.HotHardDate;
            newHotHard.HotHardMan = HotHard.HotHardMan;            
           
            newHotHard.NDTMethod = HotHard.NDTMethod;         
            newHotHard.CheckUnit = HotHard.CheckUnit;
            newHotHard.ProjectId = HotHard.ProjectId;
            newHotHard.InstallationId = HotHard.InstallationId;
            newHotHard.DetectionTime = HotHard.DetectionTime;

            newHotHard.NDTRate = HotHard.NDTRate;
            newHotHard.Sendee = HotHard.Sendee;
            newHotHard.Standards = HotHard.Standards;
            newHotHard.InspectionNum = HotHard.InspectionNum;
            newHotHard.CheckNum = HotHard.CheckNum;
            newHotHard.TestWeldNum = HotHard.TestWeldNum;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除委托信息
        /// </summary>
        /// <param name="HotHardID">委托主键</param>
        public static void DeleteHotHardByHotHardID(string HotHardID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotHard HotHard = db.HotHard.First(e => e.HotHardID == HotHardID);
            db.HotHard.DeleteOnSubmit(HotHard);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除委托信息明细
        /// </summary>
        /// <param name="HotHardID">委托主键</param>
        public static void DeleteHotHardItemByHotHardID(string HotHardID)
        {
            Model.HJGLDB db = Funs.DB;
            var HotHard = from x in db.HotHardItem where x.HotHardID == HotHardID select x;
            if (HotHard != null)
            {
                db.HotHardItem.DeleteAllOnSubmit(HotHard);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加委托信息明细
        /// </summary>
        /// <param name="HotHardItem">委托明细实体</param>
        public static void AddHotHardItem(Model.HotHardItem HotHardItem)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotHardItem newHotHardItem = new Model.HotHardItem();

            newHotHardItem.HotHardItemID = SQLHelper.GetNewID(typeof(Model.HotHardItem));
            newHotHardItem.HotHardID = HotHardItem.HotHardID;
            newHotHardItem.JOT_ID = HotHardItem.JOT_ID;

            newHotHardItem.Remark = HotHardItem.Remark;
            db.HotHardItem.InsertOnSubmit(newHotHardItem);
            db.SubmitChanges();
        }


        /// <summary>
        /// 审核委托信息
        /// </summary>
        /// <param name="weldReport">焊接实体</param>
        public static void AuditHotHard(Model.HotHard HotHard)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HotHard newHotHard = db.HotHard.First(e => e.HotHardID == HotHard.HotHardID);
            newHotHard.HotHardID = HotHard.HotHardID;
            newHotHard.AuditMan = HotHard.AuditMan;
            newHotHard.AuditDate = HotHard.AuditDate; 
            db.SubmitChanges();
        }
    }
}
