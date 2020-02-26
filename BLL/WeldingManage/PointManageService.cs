using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 点口管理
    /// </summary>
   public static class PointManageService
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
       public static IQueryable<Model.BO_Point> qq = from x in db.BO_Point orderby x.PW_PointNo select x;

       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <param name="PointID"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string projectId, string startTime, string endTime, string PointID, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.BO_Point> q = qq;
           if (!string.IsNullOrEmpty(projectId))
           {
               q = q.Where(e => e.ProjectId == projectId);
           }
           if (!string.IsNullOrEmpty(startTime))
           {
               q = q.Where(e => e.PW_TablerDate >= Convert.ToDateTime(startTime));
           }
           if (!string.IsNullOrEmpty(endTime))
           {
               q = q.Where(e => e.PW_TablerDate <= Convert.ToDateTime(endTime));
           }
           if (!string.IsNullOrEmpty(PointID))
           {
               q = q.Where(e => e.PW_PointID.Contains(PointID));
           }
           count = q.Count();
           if (count==0)
           {
               return new object[] { "" };
           }
           return from x in q.Skip(startRowIndex).Take(maximumRows)
                  select new
                  {
                      x.PW_PointID,
                      x.ProjectId,
                      BSU_ID = (from y in db.Base_Unit where y.UnitId == x.BSU_ID select y.UnitName).First(),
                      InstallationId=(from y in db.Base_Installation where y.InstallationId==x.InstallationId select y.InstallationName).First(),
                      x.PW_PointNo,
                      x.PW_PointDate,
                      x.PW_Tabler,
                      x.PW_TablerDate,
                      x.PW_Remark
                  };
       }

       /// <summary>
       /// 获取列表数
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <param name="PointID"></param>
       /// <returns></returns>
       public static int GetListCount(string projectId, string startTime, string endTime, string PointID)
       {
           return count;
       }

       /// <summary>
       /// 根据点口ID获取点口信息
       /// </summary>
       /// <param name="pointId"></param>
       /// <returns></returns>
       public static Model.BO_Point GetPointByPointID(string pointId)
       {
           return Funs.DB.BO_Point.FirstOrDefault(e => e.PW_PointID == pointId);
       }

       /// <summary>
       /// 增加点口信息
       /// </summary>
       /// <param name="point"></param>
       public static void AddPoint(Model.BO_Point point)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BO_Point newPoint = new Model.BO_Point();

           newPoint.PW_PointID = point.PW_PointID;
           newPoint.ProjectId = point.ProjectId;
           newPoint.BSU_ID = point.BSU_ID;
           newPoint.InstallationId = point.InstallationId;
           newPoint.PW_PointNo = point.PW_PointNo;
           newPoint.PW_PointDate = point.PW_PointDate;
           newPoint.PW_Tabler = point.PW_Tabler;
           newPoint.PW_TablerDate = point.PW_TablerDate;
           newPoint.PW_Remark = point.PW_Remark;

           db.BO_Point.InsertOnSubmit(newPoint);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="point"></param>
       public static void UpdatePoint(Model.BO_Point point)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BO_Point newPoint = db.BO_Point.FirstOrDefault(e => e.PW_PointID == point.PW_PointID);

           newPoint.BSU_ID = point.BSU_ID;
           newPoint.InstallationId = point.InstallationId;
           newPoint.PW_PointNo = point.PW_PointNo;
           newPoint.PW_PointDate = point.PW_PointDate;
           newPoint.PW_Tabler = point.PW_Tabler;
           newPoint.PW_TablerDate = point.PW_TablerDate;
           newPoint.PW_Remark = point.PW_Remark;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="pointId"></param>
       public static void DeletePoint(string pointId)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BO_Point point = db.BO_Point.FirstOrDefault(e => e.PW_PointID == pointId);

           db.BO_Point.DeleteOnSubmit(point);
           db.SubmitChanges();
       }


       public static bool IsExistPointNO(string pointNo)
       {
           var q = from x in Funs.DB.BO_Point where x.PW_PointNo == pointNo select x;
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
       /// 根据单位获取点口数
       /// </summary>
       /// <param name="unitId"></param>
       /// <returns></returns>
       public static int GetPointCountByUnitId(string unitId)
       {
           var q = (from x in Funs.DB.BO_Point where x.BSU_ID == unitId select x).ToList();
           return q.Count();
       }
    }
}
