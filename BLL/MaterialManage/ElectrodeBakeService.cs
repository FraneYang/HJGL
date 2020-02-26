using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
   public static class ElectrodeBakeService
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
       private static IQueryable<Model.ElectrodeBake> qq = from x in db.ElectrodeBake orderby x.ElectrodeCode select x;

       /// <summary>
       /// 分页查询列表
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string projectId, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.ElectrodeBake> q = qq;
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
              x.ElectrodeID,
              x.ElectrodeCode,
              x.ElectrodeDate,
              UnitName = (from y in db.Base_Unit where y.UnitId==x.UnitId select y.UnitName).First(),
              UserName = (from y in db.Sys_User where y.UserId==x.CompileMan select y.UserName).First(),
              x.CompileDate,
              ProjectName = (from y in db.Base_Project where y.ProjectId==x.ProjectId select y.ProjectName).First()
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
       /// 获取焊丝烘烤记录明细分页列表
       /// </summary>
       /// <param name="electrodeId"></param>
       /// <returns></returns>
       public static IEnumerable GetElectrodeItemList(string electrodeId)
       {
           return from x in Funs.DB.ElectrodeBakeItem
                  where x.ElectrodeID == electrodeId
                  select new
                  {
                      x.ElectrodeItemID,
                      x.ElectrodeID,
                      x.CardCode,
                      x.BatchCode,
                      x.InLibCode,
                      x.Specifications,
                      x.ElectrodeCount,
                      x.OvenElectricHours,
                      x.OvenElectricMinute ,
                      x.OvenElectricTemperature,
                      x.ConstantTemperature,
                      x.ConstantStartHours,
                      x.ConstantStartMinute,
                      x.ConstantEndHours,
                      x.ConstantEndMinute,
                      x.MoveInBoxHours,
                      x.MoveInBoxMinute,
                      x.MoveInTemperature,
                      x.BakeNumber,
                      x.BakeHead,
                      x.ElectrodeModel
                  };
       }

       /// <summary>
       /// 根据焊条烘烤记录Id获取焊条烘烤记录信息
       /// </summary>
       /// <param name="electrodeId">焊条烘烤记录Id</param>
       /// <returns></returns>
       public static Model.ElectrodeBake GetElecrodeBakeByElectrodeId(string electrodeId)
       {
           Model.HJGLDB db = Funs.DB;
           var elecrodeBake = db.ElectrodeBake.FirstOrDefault(e => e.ElectrodeID == electrodeId);
           return elecrodeBake;
       }

       /// <summary>
       /// 根据焊条烘烤记录获取明细信息
       /// </summary>
       /// <param name="electrodeId"></param>
       /// <returns></returns>
       public static List<Model.ElectrodeBakeItem> GetElecrodeBakeItemByElecrodeId(string electrodeId)
       {
           return (from x in Funs.DB.ElectrodeBakeItem where x.ElectrodeID == electrodeId select x).ToList();
       }

       /// <summary>
       /// 添加焊条烘烤记录
       /// </summary>
       /// <param name="electrodeBake"></param>
       public static void AddElectrodeBake(Model.ElectrodeBake electrodeBake)
       {
           Model.HJGLDB db = Funs.DB;
           Model.ElectrodeBake newElectrodeBake = new Model.ElectrodeBake();

           newElectrodeBake.ElectrodeID = electrodeBake.ElectrodeID;
           newElectrodeBake.ElectrodeCode = electrodeBake.ElectrodeCode;
           newElectrodeBake.ElectrodeDate = electrodeBake.ElectrodeDate;
           newElectrodeBake.UnitId = electrodeBake.UnitId;
           newElectrodeBake.CompileMan = electrodeBake.CompileMan;
           newElectrodeBake.CompileDate = electrodeBake.CompileDate;
           newElectrodeBake.ProjectId = electrodeBake.ProjectId;

           db.ElectrodeBake.InsertOnSubmit(newElectrodeBake);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改焊丝烘烤记录
       /// </summary>
       /// <param name="electrodeBake"></param>
       public static void UpdateElectrodeBake(Model.ElectrodeBake electrodeBake)
       {
           Model.HJGLDB db = Funs.DB;
           Model.ElectrodeBake newElectrodeBake = db.ElectrodeBake.First(e => e.ElectrodeID == electrodeBake.ElectrodeID);

           newElectrodeBake.ElectrodeCode = electrodeBake.ElectrodeCode;
           newElectrodeBake.ElectrodeDate = electrodeBake.ElectrodeDate;
           newElectrodeBake.UnitId = electrodeBake.UnitId;
           newElectrodeBake.CompileMan = electrodeBake.CompileMan;
           newElectrodeBake.CompileDate = electrodeBake.CompileDate;
           newElectrodeBake.ProjectId = electrodeBake.ProjectId;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除焊丝烘烤记录
       /// </summary>
       /// <param name="eletrodeId"></param>
       public static void DeleteElectrodeBake(string eletrodeId)
       {
           Model.HJGLDB db = Funs.DB;
           Model.ElectrodeBake electrodeBake = db.ElectrodeBake.First(e => e.ElectrodeID == eletrodeId);
           db.ElectrodeBake.DeleteOnSubmit(electrodeBake);
           db.SubmitChanges();
       }

       /// <summary>
       /// 添加焊丝烘烤记录明细
       /// </summary>
       /// <param name="electrodeBakeItem"></param>
       public static void AddElectrodeBakeItem(Model.ElectrodeBakeItem electrodeBakeItem)
       {
           Model.HJGLDB db = Funs.DB;
           Model.ElectrodeBakeItem newElectrodeBakeItem = new Model.ElectrodeBakeItem();

           newElectrodeBakeItem.ElectrodeItemID = SQLHelper.GetNewID(typeof(Model.ElectrodeBakeItem));
           newElectrodeBakeItem.ElectrodeID = electrodeBakeItem.ElectrodeID;
           newElectrodeBakeItem.CardCode = electrodeBakeItem.CardCode;
           newElectrodeBakeItem.BatchCode = electrodeBakeItem.BatchCode;
           newElectrodeBakeItem.InLibCode = electrodeBakeItem.InLibCode;
           newElectrodeBakeItem.Specifications = electrodeBakeItem.Specifications;
           newElectrodeBakeItem.ElectrodeCount = electrodeBakeItem.ElectrodeCount;
           newElectrodeBakeItem.OvenElectricHours = electrodeBakeItem.OvenElectricHours;
           newElectrodeBakeItem.OvenElectricMinute = electrodeBakeItem.OvenElectricMinute;
           newElectrodeBakeItem.OvenElectricTemperature = electrodeBakeItem.OvenElectricTemperature;
           newElectrodeBakeItem.ConstantTemperature = electrodeBakeItem.ConstantTemperature;
           newElectrodeBakeItem.ConstantStartHours = electrodeBakeItem.ConstantStartHours;
           newElectrodeBakeItem.ConstantStartMinute = electrodeBakeItem.ConstantStartMinute;
           newElectrodeBakeItem.ConstantEndHours = electrodeBakeItem.ConstantEndHours;
           newElectrodeBakeItem.ConstantEndMinute = electrodeBakeItem.ConstantEndMinute;
           newElectrodeBakeItem.MoveInBoxHours = electrodeBakeItem.MoveInBoxHours;
           newElectrodeBakeItem.MoveInBoxMinute = electrodeBakeItem.MoveInBoxMinute;
           newElectrodeBakeItem.MoveInTemperature = electrodeBakeItem.MoveInTemperature;
           newElectrodeBakeItem.BakeNumber = electrodeBakeItem.BakeNumber;
           newElectrodeBakeItem.BakeHead = electrodeBakeItem.BakeHead;
           newElectrodeBakeItem.ElectrodeModel = electrodeBakeItem.ElectrodeModel;

           db.ElectrodeBakeItem.InsertOnSubmit(newElectrodeBakeItem);
           db.SubmitChanges();
       }

       /// <summary>
       /// 根据焊丝烘烤记录Id删除所有相关的明细
       /// </summary>
       /// <param name="electrodeId">焊丝烘烤记录Id</param>
       public static void DeleteElectrodeBakeItem(string electrodeId)
       {
           Model.HJGLDB db = Funs.DB;
           var q = (from x in db.ElectrodeBakeItem where x.ElectrodeID == electrodeId select x).ToList();
           db.ElectrodeBakeItem.DeleteAllOnSubmit(q);
           db.SubmitChanges();
       }

          /// <summary>
      /// 获取打印分页列表
      /// </summary>
      /// <returns></returns>
       public static IEnumerable GetListDataPrint(DateTime startDate, DateTime endDate)
       {
           return from x in db.ElectrodeBakeItem
                  join y in db.ElectrodeBake on x.ElectrodeID equals y.ElectrodeID
                  where y.ElectrodeDate >= startDate && y.ElectrodeDate <= endDate
                  orderby y.ElectrodeCode
                  select new
                  {
                      x.ElectrodeItemID,
                      x.ElectrodeID,
                      x.CardCode,
                      x.BatchCode,
                      x.InLibCode,
                      x.Specifications,
                      x.ElectrodeCount,
                      x.OvenElectricHours,
                      x.OvenElectricMinute,
                      x.OvenElectricTemperature,
                      x.ConstantTemperature,
                      x.ConstantStartHours,
                      x.ConstantStartMinute,
                      x.ConstantEndHours,
                      x.ConstantEndMinute,
                      x.MoveInBoxHours,
                      x.MoveInBoxMinute,
                      x.MoveInTemperature,
                      x.BakeNumber,
                      x.BakeHead,
                      y.ElectrodeDate,
                      x.ElectrodeModel
                  };                  
       }
    }
}
