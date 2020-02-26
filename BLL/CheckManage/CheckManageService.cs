using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
   public class CheckManageService
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
       public static IQueryable<Model.CH_Check> qq = from x in db.CH_Check orderby x.CHT_CheckCode select x;

       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <param name="cht_checkId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string projectId, string startTime, string endTime, string cht_checkId, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.CH_Check> q = qq;
           if (!string.IsNullOrEmpty(projectId))
           {
               q = q.Where(e => e.ProjectId == projectId);
           }
           if (!string.IsNullOrEmpty(startTime))
           {
               q = q.Where(e => e.CHT_CheckDate >= Convert.ToDateTime(startTime));
           }
           if (!string.IsNullOrEmpty(endTime))
           {
               q = q.Where(e => e.CHT_CheckDate <= Convert.ToDateTime(endTime));
           }
           if (!string.IsNullOrEmpty(cht_checkId))
           {
               q = q.Where(e => e.CHT_CheckID == cht_checkId);
           }
           count = q.Count();
           if (count==0)
           {
               return new object[] { "" };
           }
           return from x in q.Skip(startRowIndex).Take(maximumRows)
                  select new
                  {
                      x.CHT_CheckID,
                      x.ProjectId,
                      UnitId = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                      x.CHT_CheckCode,
                      x.CHT_CheckDate,
                      x.CHT_CheckType,
                      x.CHT_CheckMan,
                      x.CHT_Tabler,
                      x.CHT_TableDate,
                      x.CHT_AuditMan,
                      x.CHT_AuditDate,
                      x.CHT_Remark
                  };
       }

       /// <summary>
       /// 获取列表数
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <param name="cht_checkId"></param>
       /// <returns></returns>
       public static int GetListData(string projectId, string startTime, string endTime, string cht_checkId)
       {
           return count;
       }

       /// <summary>
       /// 根据检测ID获取检测信息
       /// </summary>
       /// <param name="cht_checkId"></param>
       /// <returns></returns>
       public static List<Model.CH_Check> GetCheckByCheckId(string cht_checkId)
       {
           return (from x in Funs.DB.CH_Check where x.CHT_CheckID == cht_checkId select x).ToList();
       }

       /// <summary>
       /// 根据检测Id获取检测信息
       /// </summary>
       /// <param name="cht_checkId"></param>
       /// <returns></returns>
       public static Model.CH_Check GetCheckByCHT_CheckID(string cht_checkId)
       {
           return Funs.DB.CH_Check.FirstOrDefault(e => e.CHT_CheckID == cht_checkId);
       }

       /// <summary>
       /// 检测单号是否存在
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="checkCode"></param>
       /// <returns></returns>
       public static bool IsExistCheckCode(string projectId, string checkCode)
       {
           Model.HJGLDB db = Funs.DB;
           var q = from x in db.CH_Check where x.ProjectId == projectId && x.CHT_CheckCode == checkCode select x;
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
       /// 添加
       /// </summary>
       /// <param name="check"></param>
       public static void AddCheck(Model.CH_Check check)
       {
           Model.HJGLDB db = Funs.DB;
           Model.CH_Check newCheck = new Model.CH_Check();

           newCheck.CHT_CheckID = check.CHT_CheckID;
           newCheck.CH_TrustID = check.CH_TrustID;
           newCheck.ProjectId = check.ProjectId;
           newCheck.UnitId = check.UnitId;
           newCheck.InstallationId = check.InstallationId;
           newCheck.CHT_CheckCode = check.CHT_CheckCode;
           newCheck.CHT_CheckDate = check.CHT_CheckDate;
           newCheck.CHT_CheckType = check.CHT_CheckType;
           newCheck.CHT_CheckMan = check.CHT_CheckMan;
           newCheck.CHT_Tabler = check.CHT_Tabler;
           newCheck.CHT_TableDate = check.CHT_TableDate;
           newCheck.CHT_AuditMan = check.CHT_AuditMan;
           newCheck.CHT_AuditDate = check.CHT_AuditDate;
           newCheck.CHT_Remark = check.CHT_Remark;        

           db.CH_Check.InsertOnSubmit(newCheck);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="check"></param>
       public static void UpdateCheck(Model.CH_Check check)
       { 
           Model.HJGLDB db=Funs.DB;
           Model.CH_Check newCheck = db.CH_Check.FirstOrDefault(e => e.CHT_CheckID == check.CHT_CheckID);

           newCheck.UnitId = check.UnitId;
           newCheck.InstallationId = check.InstallationId;
           newCheck.CHT_CheckCode = check.CHT_CheckCode;
           newCheck.CHT_CheckDate = check.CHT_CheckDate;
           newCheck.CHT_CheckType = check.CHT_CheckType;
           newCheck.CHT_CheckMan = check.CHT_CheckMan;
           newCheck.CHT_Tabler = check.CHT_Tabler;
           newCheck.CHT_TableDate = check.CHT_TableDate;
           newCheck.CHT_AuditMan = check.CHT_AuditMan;
           newCheck.CHT_AuditDate = check.CHT_AuditDate;
           newCheck.CHT_Remark = check.CHT_Remark;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="checkId"></param>
       public static void DeleteCheck(string checkId)
       {
           Model.HJGLDB db = Funs.DB;
           Model.CH_Check check = db.CH_Check.FirstOrDefault(e => e.CHT_CheckID == checkId);

           db.CH_Check.DeleteOnSubmit(check);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改审核人和审核日期
       /// </summary>
       /// <param name="check"></param>
       public static void UpdateCheckAudit(Model.CH_Check check)
       {
           Model.HJGLDB db = Funs.DB;
           Model.CH_Check newCheck = db.CH_Check.FirstOrDefault(e => e.CHT_CheckID == check.CHT_CheckID);
           newCheck.CHT_AuditMan = check.CHT_AuditMan;
           newCheck.CHT_AuditDate = check.CHT_AuditDate;

           db.SubmitChanges();
       }


       /// <summary>
       /// 修改检测单 是否返修
       /// </summary>
       /// <param name="jot_ID">焊口id</param>
       /// <param name="repairTrustId">返修委托单号</param>
       /// <param name="isUpdate">是否更新</param>
       public static void UpdateCheckIsRepair(string jot_ID, string repairTrustId, bool isUpdate)
       {
           Model.HJGLDB db = Funs.DB;

           var checkItem = from x in db.CH_CheckItem
                           where x.JOT_ID == jot_ID && x.CHT_TotalFilm != x.CHT_PassFilm
                           select x.CHT_CheckID;
           if (checkItem.Count() > 0)
           {
               var checks = from x in db.CH_Check
                            where checkItem.Distinct().Contains(x.CHT_CheckID) && x.CHT_AuditDate != null
                            select x;
               if (isUpdate)
               {
                   checks = checks.Where(x => x.RepairTrustId == null);
                   if (checks.Count() > 0)
                   {
                       foreach (var check in checks)
                       {
                           check.RepairTrustId = repairTrustId;
                           db.SubmitChanges();
                       }
                   }
               }
               else
               {
                   checks = checks.Where(x => x.RepairTrustId == repairTrustId);
                   if (checks.Count() > 0)
                   {
                       foreach (var check in checks)
                       {
                           check.RepairTrustId = null;
                           db.SubmitChanges();
                       }
                   }
               }
           }
       }

       /// <summary>
       /// 根据单位Id获取检测数
       /// </summary>
       /// <param name="UnitId"></param>
       /// <returns></returns>
       public static int GetCheckByUnitId(string unitId)
       {
           var q = (from x in Funs.DB.CH_Check where x.UnitId == unitId select x).ToList();
           return q.Count();
       }

       /// <summary>
       /// 根据装置Id获取检测数
       /// </summary>
       /// <param name="installationId"></param>
       /// <returns></returns>
       public static int GetCheckByInstallationId(int installationId)
       {
           var q = (from x in Funs.DB.CH_Check where x.InstallationId == installationId select x).ToList();
           return q.Count();
       }
       /// <summary>
       /// 根据委托Id获取检测数
       /// </summary>
       /// <param name="trustId"></param>
       /// <returns></returns>
       public static int GetCheckByTrustId(string trustId)
       {
           var q = (from x in Funs.DB.CH_Check where x.CH_TrustID == trustId select x).ToList();
           return q.Count();
       }
    }
}
