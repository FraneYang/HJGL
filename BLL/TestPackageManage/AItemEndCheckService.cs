using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class AItemEndCheckService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键Id获取用于A项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.TP_AItemEndCheck GetTP_AItemEndCheckByID(string id)
        {
            Model.HJGLDB db = Funs.DB;
            return db.TP_AItemEndCheck.FirstOrDefault(x=>x.EIC_ID == id);
        } 

        /// <summary>
        /// 根据管线Id获取用于A项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.TP_AItemEndCheck> GetTP_AItemEndCheckByISO_ID(string ISO_ID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = from x in db.TP_AItemEndCheck
                       where x.ISO_ID == ISO_ID
                       orderby x.EIC_CheckDate
                       select x;
            return view.ToList();
        } 
        
          /// <summary>
        /// 增加业务_A项尾工检查表
        /// </summary>
        /// <param name="aItemEndCheck">试压实体</param>
        public static void AddTP_AItemEndCheck(Model.TP_AItemEndCheck aItemEndCheck)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_AItemEndCheck newaItemEndCheck = new Model.TP_AItemEndCheck();
            newaItemEndCheck.EIC_ID = SQLHelper.GetNewID(typeof(Model.TP_AItemEndCheck));
            newaItemEndCheck.ISO_ID = aItemEndCheck.ISO_ID;
            newaItemEndCheck.EIC_CheckMan = aItemEndCheck.EIC_CheckMan;
            newaItemEndCheck.EIC_CheckDate = aItemEndCheck.EIC_CheckDate;
            newaItemEndCheck.EIC_DealMan = aItemEndCheck.EIC_DealMan;
            newaItemEndCheck.EIC_DealDate = aItemEndCheck.EIC_DealDate;
            newaItemEndCheck.EIC_Remark = aItemEndCheck.EIC_Remark;
            db.TP_AItemEndCheck.InsertOnSubmit(newaItemEndCheck);
            db.SubmitChanges();
        }

          /// <summary>
        /// 修改业务_A项尾工检查表
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateTP_AItemEndCheck(Model.TP_AItemEndCheck aItemEndCheck)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_AItemEndCheck newaItemEndCheck = db.TP_AItemEndCheck.First(e => e.EIC_ID == aItemEndCheck.EIC_ID);
            newaItemEndCheck.ISO_ID = aItemEndCheck.ISO_ID;
            newaItemEndCheck.EIC_CheckMan = aItemEndCheck.EIC_CheckMan;
            newaItemEndCheck.EIC_CheckDate = aItemEndCheck.EIC_CheckDate;
            newaItemEndCheck.EIC_DealMan = aItemEndCheck.EIC_DealMan;
            newaItemEndCheck.EIC_DealDate = aItemEndCheck.EIC_DealDate;
            newaItemEndCheck.EIC_Remark = aItemEndCheck.EIC_Remark;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除业务_A项尾工检查表
        /// </summary>
        /// <param name="id">业务_A项尾工检查表主键</param>
        public static void DeleteTP_AItemEndCheckByID(string id)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_AItemEndCheck aItemEndCheck = db.TP_AItemEndCheck.First(e => e.EIC_ID == id);
            db.TP_AItemEndCheck.DeleteOnSubmit(aItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据管线Id判断是否存在A项尾工
        /// </summary>
        /// <param name="isono"></param>
        /// <returns></returns>
        public static bool IsExistAItemEndCheck(string iso_id)
        {
            var q = from x in Funs.DB.TP_AItemEndCheck where x.ISO_ID == iso_id select x;
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
