using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 检测明细
    /// </summary>
   public class CheckItemManageService
    {
       public static Model.HJGLDB db = Funs.DB;

       public static int count
       {
           get;
           set;
       }


       /// <summary>
       /// 根据检测单号获取相关的所有明细
       /// </summary>
       /// <param name="cht_checkId"></param>
       /// <returns></returns>
       public static List<Model.View_CH_CheckItem> GetCheckItemByCheckID(string cht_checkId)
       {
           return (from x in Funs.DB.View_CH_CheckItem where x.CHT_CheckID==cht_checkId select x).ToList();
       }

       /// <summary>
       /// 根据焊口ID获取委托明细信息
       /// </summary>
       /// <param name="jotId"></param>
       /// <returns></returns>
       public static Model.View_CH_CheckItem GetTrustItemByCheckItem(string checkItemID)
       {
           return Funs.DB.View_CH_CheckItem.FirstOrDefault(e => e.CHT_CheckItemID == checkItemID);
       }

       /// <summary>
       /// 根据焊口ID获取委托明细信息
       /// </summary>
       /// <param name="jotId"></param>
       /// <returns></returns>
       public static Model.View_CH_CheckItem GetViewCheckItemByJOTID(string jotId)
       {
           return Funs.DB.View_CH_CheckItem.FirstOrDefault(e => e.JOT_ID == jotId);
       }

       /// <summary>
       /// 根据焊口ID获取检测明细信息
       /// </summary>
       /// <param name="jotId"></param>
       /// <returns></returns>
       public static List<Model.CH_CheckItem> GetTrustItemByCheck(string checkId)
       {
           return Funs.DB.CH_CheckItem.Where(e => e.CHT_CheckID == checkId).ToList();
       }

       /// <summary>
       /// 根据检测ID获取焊口信息
       /// </summary>
       /// <param name="checkId"></param>
       /// <returns></returns>
       public static List<Model.PW_JointInfo> GetJointInfoByCheckId(string checkId)
       {
           return (from x in Funs.DB.PW_JointInfo join y in Funs.DB.CH_CheckItem on x.JOT_ID equals y.JOT_ID join z in Funs.DB.CH_Check on y.CHT_CheckID equals z.CHT_CheckID where z.CHT_CheckID == checkId select x).ToList();
       }

       /// <summary>
       /// 根据焊口ID和检测id获取检测明细信息
       /// </summary>
       /// <param name="jotId"></param>
       /// <returns></returns>
       public static Model.CH_CheckItem GetTrustItemByCheckAndJotId(string checkId, string jotId)
       {
           return Funs.DB.CH_CheckItem.FirstOrDefault(e => e.CHT_CheckID == checkId && e.JOT_ID == jotId);
       }

       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="checkItem"></param>
       public static void AddCheckItem(Model.CH_CheckItem checkItem)
       {
           Model.HJGLDB db = Funs.DB;
           Model.CH_CheckItem newCheckItem = new Model.CH_CheckItem();
           string newKeyID = SQLHelper.GetNewID(typeof(Model.CH_CheckItem));

           newCheckItem.CHT_CheckItemID = newKeyID;
           newCheckItem.CHT_CheckID = checkItem.CHT_CheckID;
           newCheckItem.JOT_ID = checkItem.JOT_ID;
           newCheckItem.CH_TrustItemID = checkItem.CH_TrustItemID;
           newCheckItem.CHT_CheckMethod = checkItem.CHT_CheckMethod;
           newCheckItem.CHT_RequestDate = checkItem.CHT_RequestDate;
           newCheckItem.CHT_RepairLocation = checkItem.CHT_RepairLocation;
           newCheckItem.CHT_TotalFilm = checkItem.CHT_TotalFilm;
           newCheckItem.CHT_PassFilm = checkItem.CHT_PassFilm;
           newCheckItem.CHT_CheckResult = checkItem.CHT_CheckResult;
           newCheckItem.CHT_CheckNo = checkItem.CHT_CheckNo;
           newCheckItem.CHT_FilmDate = checkItem.CHT_FilmDate;
           newCheckItem.CHT_ReportDate = checkItem.CHT_ReportDate;
           newCheckItem.CHT_Remark = checkItem.CHT_Remark;
           newCheckItem.CHT_FloorWelder1 = checkItem.CHT_FloorWelder1;
           newCheckItem.CHT_FloorWelder2 = checkItem.CHT_FloorWelder2;

           db.CH_CheckItem.InsertOnSubmit(newCheckItem);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改检测明细审核日期
       /// </summary>
       /// <param name="cht_CheckID">主表ID</param>
       /// <param name="flag">1表示更新为当前时间，2表示为取消审核日期</param>
       public static void UpdateCheckItemAudioTime(string cht_CheckID,string flag)
       {
           Model.HJGLDB db = Funs.DB;
           var q = GetCheckItemByCheck(cht_CheckID);
           foreach (var item in q)
           {
               Model.CH_CheckItem newCheck = item;
               if (flag == "1") 
               {
                   newCheck.CHT_AuditTime = DateTime.Now;
               }
               else
               {
                   newCheck.CHT_AuditTime = null;
               }

               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 根据主表Id删除所有明细信息
       /// </summary>
       /// <param name="checkId"></param>
       public static void DeleteCheckItemByCheckId(string checkId)
       {
           Model.HJGLDB db = Funs.DB;
           var checkItem = from x in db.CH_CheckItem where x.CHT_CheckID == checkId select x;
           db.CH_CheckItem.DeleteAllOnSubmit(checkItem);
           db.SubmitChanges();
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="trustId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListDataTrust(string trustId, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.View_CH_CheckSerch> q = from x in db.View_CH_CheckSerch 
                                                    where x.CH_TrustID == trustId
                                                    orderby x.CH_TrustCode select x;          
           count = q.Count();
           if (count==0)
           {
               return new object[] { "" };
           }
           return from x in q.Skip(startRowIndex).Take(maximumRows)
                  select new
                  {
                      x.ProjectId,
                      x.JOT_ID,
                      x.CH_TrustID,
                      x.CH_TrustCode,
                      x.BSU_ID,
                      x.UnitName,
                      x.ISO_IsoNo,
                      x.JOT_JointNo,
                      x.NDT_ID,
                      x.NDT_Name
                  };
       }

       /// <summary>
       /// 获取列表数
       /// </summary>
       /// <param name="trustId"></param>
       /// <param name="auditMan"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public static int GetListCount2(string trustId)
       {
           return count;
       }

       /// <summary>
       /// 更改焊口检测情况
       /// </summary>
       /// <param name="jot_id"></param>
       /// <param name="type"></param>
       public static void UpdateJointCheckFlag(string jot_id, string type)
       {
           var jointinfo = Funs.DB.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == jot_id);
           if (jointinfo!=null)
           {
               if (type=="1")
               {
                   if (string.IsNullOrEmpty(jointinfo.JOT_CheckFlag)||jointinfo.JOT_CheckFlag=="00")
                   {
                       jointinfo.JOT_CheckFlag = "01";
                   }
                   else if (jointinfo.JOT_CheckFlag=="01")
                   {
                       jointinfo.JOT_CheckFlag = "02";
                   }
                   else if(jointinfo.JOT_CheckFlag=="02")
                   {
                       jointinfo.JOT_CheckFlag = "11";
                   }
                   else if (jointinfo.JOT_CheckFlag == "11")
                   {
                       jointinfo.JOT_CheckFlag="12";
                   }
                   else if (jointinfo.JOT_CheckFlag=="12")
                   {
                       jointinfo.JOT_CheckFlag = "21";
                   }
                   else if (jointinfo.JOT_CheckFlag == "21")
                   {
                       jointinfo.JOT_CheckFlag = "22";
                   }
               }
               else
               {
                   if (jointinfo.JOT_CheckFlag=="22")
                   {
                       jointinfo.JOT_CheckFlag = "21";
                   }
                   else if (jointinfo.JOT_CheckFlag=="21")
                   {
                       jointinfo.JOT_CheckFlag = "12";
                   }
                   else if (jointinfo.JOT_CheckFlag=="12")
                   {
                       jointinfo.JOT_CheckFlag = "11";
                   }
                   else if(jointinfo.JOT_CheckFlag=="11")
                   {
                       jointinfo.JOT_CheckFlag = "02";
                   }
                   else if (jointinfo.JOT_CheckFlag=="02")
                   {
                       jointinfo.JOT_CheckFlag = "01";
                   }
                   else 
                   {
                       jointinfo.JOT_CheckFlag = "00";
                   }
               }
           }
           Funs.DB.SubmitChanges();
       }

       /// <summary>
       /// 根据检测单号获取相关的所有明细
       /// </summary>
       /// <param name="cht_checkId"></param>
       /// <returns></returns>
       public static List<Model.CH_CheckItem> GetCheckItemByCheck(string cht_checkId)
       {
           return (from x in Funs.DB.CH_CheckItem where x.CHT_CheckID == cht_checkId select x).ToList();
       }

       /// <summary>
       /// 根据焊口信息获取检测记录
       /// </summary>
       /// <param name="jotId"></param>
       /// <returns></returns>
       public static Model.CH_CheckItem GetCheckItemByJotId(string jotId)
       {
           return Funs.DB.CH_CheckItem.FirstOrDefault(e => e.JOT_ID == jotId);
       }
    }
}
