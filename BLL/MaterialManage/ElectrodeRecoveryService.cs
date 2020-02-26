using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
  public static class ElectrodeRecoveryService
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
      public static IQueryable<Model.ElectrodeRecovery> qq = from x in db.ElectrodeRecovery orderby x.ElectrodeRecoveryCode select x;

      /// <summary>
      /// 获取分页列表
      /// </summary>
      /// <param name="projectId"></param>
      /// <param name="startRowIndex"></param>
      /// <param name="maximumRows"></param>
      /// <returns></returns>
      public static IEnumerable GetListData(string projectId, int startRowIndex, int maximumRows)
      {
          IQueryable<Model.ElectrodeRecovery> q = qq;
          if (!string.IsNullOrEmpty(projectId))
          {
              q = q.Where(e => e.ProjectId == projectId);
          }
          count = q.Count();
          if (count==0)
          {
              return new object[] { "" };
          }
          return from x in q.Skip(startRowIndex).Take(maximumRows)
                 select new
                 {
                     x.ElectrodeRecoveryId,
                     x.ElectrodeRecoveryCode,
                     x.ElectrodeRecoveryDate,
                     CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                     x.CompileDate,
                     ProjectId = (from y in db.Base_Project where y.ProjectId == x.ProjectId select y.ProjectName).First(),                     
                 };
      }
      /// <summary>
      /// 分页查询列表数
      /// </summary>
      /// <param name="projectId"></param>
      /// <returns></returns>
      public static int GetListCount(string projectId)
      {
          return count;
      }

      /// <summary>
      /// 获取焊丝发放回收明细分页列表
      /// </summary>
      /// <param name="electrodeRecoveryId"></param>
      /// <returns></returns>
      public static IEnumerable GetElectrodeRecoveryItemList(string electrodeRecoveryId)
      {
          return from x in Funs.DB.ElectrodeRecoveryItem
                 where x.ElectrodeRecoveryId == electrodeRecoveryId
                 select new
                 {
                     x.ElectrodeRecoveryItemID,
                     x.ElectrodeRecoveryId,
                     x.ElectrodeGrade,
                     x.BatchNumber,
                     x.InLibCode,
                     x.Specifications,
                     x.WelderCode,
                     x.UseSite,
                     x.WeldingMaterial,
                     x.RecipientsCount,
                     x.RecoveryCount,
                     x.GrantMan,
                     x.ElectrodeRecoveryModel
                 };
      }

      /// <summary>
      /// 根据焊条发放回收记录ID获取焊条发放回收记录信息
      /// </summary>
      /// <param name="electrodeRecoveryID">焊条发放回收记录ID</param>
      /// <returns></returns>
      public static Model.ElectrodeRecovery GetElectrodeRecoveryByID(string electrodeRecoveryID)
      {
          Model.HJGLDB db = Funs.DB;
          var electrodeRecovery = db.ElectrodeRecovery.FirstOrDefault(e => e.ElectrodeRecoveryId == electrodeRecoveryID);
          return electrodeRecovery;
      }

      /// <summary>
      ///根据焊条发放回收记录ID获取焊条发放回收记录明细信息
      /// </summary>
      /// <param name="electrodeCoveryId">焊条发放回收记录ID</param>
      /// <returns></returns>
      public static List<Model.ElectrodeRecoveryItem> GetElectrodeRecoveryItemByRecoveryID(string electrodeCoveryId)
      {
          return (from x in Funs.DB.ElectrodeRecoveryItem where x.ElectrodeRecoveryId == electrodeCoveryId select x).ToList();
      }

      /// <summary>
      /// 添加焊条发放回收记录信息
      /// </summary>
      /// <param name="electrodeRecovery"></param>
      public static void AddElectrodeRecovery(Model.ElectrodeRecovery electrodeRecovery)
      {
          Model.HJGLDB db = Funs.DB;
          Model.ElectrodeRecovery newElectrodeRecovery = new Model.ElectrodeRecovery();
          newElectrodeRecovery.ElectrodeRecoveryId = electrodeRecovery.ElectrodeRecoveryId;
          newElectrodeRecovery.ElectrodeRecoveryCode = electrodeRecovery.ElectrodeRecoveryCode;
          newElectrodeRecovery.ElectrodeRecoveryDate = electrodeRecovery.ElectrodeRecoveryDate;
          newElectrodeRecovery.CompileMan = electrodeRecovery.CompileMan;
          newElectrodeRecovery.CompileDate = electrodeRecovery.CompileDate;
          newElectrodeRecovery.ProjectId = electrodeRecovery.ProjectId;
          
          db.ElectrodeRecovery.InsertOnSubmit(newElectrodeRecovery);
          db.SubmitChanges();
      }
      /// <summary>
      /// 修改焊条发放回收记录
      /// </summary>
      /// <param name="electrodeRecovery"></param>
      public static void UpdateElectrodeRecovery(Model.ElectrodeRecovery electrodeRecovery)
      {
          Model.HJGLDB db = Funs.DB;
          Model.ElectrodeRecovery newElectrodeRecovery = db.ElectrodeRecovery.First(e => e.ElectrodeRecoveryId == electrodeRecovery.ElectrodeRecoveryId);

          newElectrodeRecovery.ElectrodeRecoveryCode = electrodeRecovery.ElectrodeRecoveryCode;
          newElectrodeRecovery.ElectrodeRecoveryDate = electrodeRecovery.ElectrodeRecoveryDate;
          newElectrodeRecovery.CompileMan = electrodeRecovery.CompileMan;
          newElectrodeRecovery.CompileDate = electrodeRecovery.CompileDate;
          newElectrodeRecovery.ProjectId = electrodeRecovery.ProjectId;

          db.SubmitChanges();
      }

      /// <summary>
      /// 删除焊条发放回收记录
      /// </summary>
      /// <param name="electrodeRecoveryId"></param>
      public static void DeleteElectrodeRecovery(string electrodeRecoveryId)
      {
          Model.HJGLDB db = Funs.DB;
          Model.ElectrodeRecovery electrodeRecovery = db.ElectrodeRecovery.First(e => e.ElectrodeRecoveryId == electrodeRecoveryId);
          db.ElectrodeRecovery.DeleteOnSubmit(electrodeRecovery);
          db.SubmitChanges();
      }

      /// <summary>
      /// 添加焊条发放回收记录明细信息
      /// </summary>
      /// <param name="item"></param>
      public static void AddElectrodeRecoveryItem(Model.ElectrodeRecoveryItem item)
      {
          Model.HJGLDB db = Funs.DB;
          Model.ElectrodeRecoveryItem newItem = new Model.ElectrodeRecoveryItem();

          newItem.ElectrodeRecoveryItemID = SQLHelper.GetNewID(typeof(Model.ElectrodeRecoveryItem));
          newItem.ElectrodeRecoveryId = item.ElectrodeRecoveryId;
          newItem.ElectrodeGrade = item.ElectrodeGrade;
          newItem.BatchNumber = item.BatchNumber;
          newItem.InLibCode = item.InLibCode;
          newItem.Specifications = item.Specifications;
          newItem.WelderCode = item.WelderCode;
          newItem.UseSite = item.UseSite;
          newItem.WeldingMaterial = item.WeldingMaterial;
          newItem.RecipientsCount = item.RecipientsCount;
          newItem.RecoveryCount = item.RecoveryCount;
          newItem.GrantMan = item.GrantMan;
          newItem.ElectrodeRecoveryModel = item.ElectrodeRecoveryModel;
          newItem.WMT_ID = item.WMT_ID;
          db.ElectrodeRecoveryItem.InsertOnSubmit(newItem);
          db.SubmitChanges();
      }

      /// <summary>
      /// 删除根据焊条发放回收记录Id相关的所有明细信息
      /// </summary>
      /// <param name="electrodeRecoveryId"></param>
      public static void DeleteElectrodeRecoveryItem(string projectId,string electrodeRecoveryId)
      {
          Model.HJGLDB db = Funs.DB;
          var deleteItem = (from x in db.ElectrodeRecoveryItem where x.ElectrodeRecoveryId == electrodeRecoveryId select x).ToList();
          if (deleteItem.Count() > 0)
          {
              foreach (var ditem in deleteItem)
              {
                  int count = 0;
                  if (ditem.RecipientsCount.HasValue)
                  {
                      count = count + ditem.RecipientsCount.Value;
                  }

                  if (ditem.RecoveryCount.HasValue)
                  {
                      count = count - ditem.RecoveryCount.Value;
                  }

                  BLL.EMInventoryRecordsService.UpdateEMInventoryRecords(projectId, ditem.WMT_ID, ditem.ElectrodeRecoveryModel, ditem.Specifications, count);
                  
              }

              db.ElectrodeRecoveryItem.DeleteAllOnSubmit(deleteItem);
              db.SubmitChanges();
          }
      }

      /// <summary>
      /// 获取打印分页列表
      /// </summary>
      /// <returns></returns>
      public static IEnumerable GetListDataPrint(DateTime startDate,DateTime endDate)
      {
          return from x in db.ElectrodeRecoveryItem
                 join y in db.ElectrodeRecovery on x.ElectrodeRecoveryId equals y.ElectrodeRecoveryId
                 where y.ElectrodeRecoveryDate >= startDate && y.ElectrodeRecoveryDate <= endDate
                 orderby y.ElectrodeRecoveryCode
                 select new
                 {
                     x.ElectrodeRecoveryId,
                     x.ElectrodeRecoveryItemID,
                     x.ElectrodeGrade,
                     x.BatchNumber,
                     x.InLibCode,
                     x.Specifications,
                     x.WelderCode,
                     x.UseSite,
                     x.WeldingMaterial,
                     x.RecipientsCount,
                     x.RecoveryCount,
                     x.GrantMan,
                     y.ElectrodeRecoveryDate,
                     x.ElectrodeRecoveryModel
                 };
      }
    }
}
