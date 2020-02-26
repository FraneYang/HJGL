using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class BItemEndCheckService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键Id获取用于B项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.TP_BItemEndCheck GetTP_BItemEndCheckByID(string id)
        {
            Model.HJGLDB db = Funs.DB;
            return db.TP_BItemEndCheck.FirstOrDefault(x=>x.EIC_ID == id);
        } 

        /// <summary>
        /// 根据管线Id获取用于B项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.TP_BItemEndCheck> GetTP_BItemEndCheckByISO_ID(string ISO_ID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = from x in db.TP_BItemEndCheck
                       where x.ISO_ID == ISO_ID
                       orderby x.EIC_CheckDate
                       select x;
            return view.ToList();
        } 
        
          /// <summary>
        /// 增加业务_B项尾工检查表
        /// </summary>
        /// <param name="bItemEndCheck">试压实体</param>
        public static void AddTP_BItemEndCheck(Model.TP_BItemEndCheck bItemEndCheck)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_BItemEndCheck newbItemEndCheck = new Model.TP_BItemEndCheck();
            newbItemEndCheck.EIC_ID = SQLHelper.GetNewID(typeof(Model.TP_BItemEndCheck));
            newbItemEndCheck.ISO_ID = bItemEndCheck.ISO_ID;
            newbItemEndCheck.EIC_CheckMan = bItemEndCheck.EIC_CheckMan;
            newbItemEndCheck.EIC_CheckDate = bItemEndCheck.EIC_CheckDate;
            newbItemEndCheck.EIC_DealMan = bItemEndCheck.EIC_DealMan;
            newbItemEndCheck.EIC_DealDate = bItemEndCheck.EIC_DealDate;
            newbItemEndCheck.EIC_Remark = bItemEndCheck.EIC_Remark;
            db.TP_BItemEndCheck.InsertOnSubmit(newbItemEndCheck);
            db.SubmitChanges();
        }

          /// <summary>
        /// 修改业务_B项尾工检查表
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateTP_BItemEndCheck(Model.TP_BItemEndCheck bItemEndCheck)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_BItemEndCheck newbItemEndCheck = db.TP_BItemEndCheck.First(e => e.EIC_ID == bItemEndCheck.EIC_ID);
            newbItemEndCheck.ISO_ID = bItemEndCheck.ISO_ID;
            newbItemEndCheck.EIC_CheckMan = bItemEndCheck.EIC_CheckMan;
            newbItemEndCheck.EIC_CheckDate = bItemEndCheck.EIC_CheckDate;
            newbItemEndCheck.EIC_DealMan = bItemEndCheck.EIC_DealMan;
            newbItemEndCheck.EIC_DealDate = bItemEndCheck.EIC_DealDate;
            newbItemEndCheck.EIC_Remark = bItemEndCheck.EIC_Remark;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除业务_B项尾工检查表
        /// </summary>
        /// <param name="id">业务_B项尾工检查表主键</param>
        public static void DeleteTP_BItemEndCheckByID(string id)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_BItemEndCheck bItemEndCheck = db.TP_BItemEndCheck.First(e => e.EIC_ID == id);
            db.TP_BItemEndCheck.DeleteOnSubmit(bItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据管线获取B项尾工
        /// </summary>
        /// <param name="isono"></param>
        /// <returns></returns>
        public static bool IsExistBItemEndCheck(string iso_id)
        {
            var q = from x in Funs.DB.TP_BItemEndCheck where x.ISO_ID == iso_id select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
