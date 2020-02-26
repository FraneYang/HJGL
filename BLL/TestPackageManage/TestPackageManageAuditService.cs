using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TestPackageManageAuditService
    {
        public static Model.HJGLDB db = Funs.DB;

     
        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_TestPackageManageAudit> GetTestPackageManageAuditByPTP_ID(string PTP_ID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = from x in db.View_TestPackageManageAudit                    
                       where x.PTP_ID == PTP_ID select x;
            return view.ToList();
        }
        
        /// <summary>
        /// 审核试压信息
        /// </summary>
        /// <param name="testPackage">试压实体</param>
        public static void AuditTP_TestPackage(Model.TP_TestPackage testPackage)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_TestPackage newtestPackage = db.TP_TestPackage.First(e => e.PTP_ID == testPackage.PTP_ID);
            newtestPackage.PTP_ID = testPackage.PTP_ID;
            newtestPackage.PTP_Auditer = testPackage.PTP_Auditer;
            newtestPackage.PTP_AduditDate = testPackage.PTP_AduditDate;
            db.SubmitChanges();
        }

        /// <summary>
        /// 试压完工审核信息
        /// </summary>
        /// <param name="testPackage">试压实体</param>
        public static void AuditFinishDef(Model.TP_TestPackage testPackage)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_TestPackage newtestPackage = db.TP_TestPackage.First(e => e.PTP_ID == testPackage.PTP_ID);
            newtestPackage.PTP_ID = testPackage.PTP_ID;
            newtestPackage.PTP_Finisher = testPackage.PTP_Finisher;
            newtestPackage.PTP_FinishDate = testPackage.PTP_FinishDate;
            newtestPackage.FinishDef = testPackage.FinishDef;
            db.SubmitChanges();
        }
    }
}
