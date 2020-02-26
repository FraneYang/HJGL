using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public static class EWeldRHRecordService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.EWeldRHRecord> qq = from x in db.EWeldRHRecord orderby x.EWeldRHRecordCode select x;

        private static IQueryable<Model.EWeldRHRecordItem> qqitem = from x in db.EWeldRHRecordItem select x;

        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.EWeldRHRecord> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.EWeldRHRecordId,
                       x.EWeldRHRecordCode,
                       x.EWeldRHRecordDate,
                       UserName = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                       x.CompileDate,
                       UnitName = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                       ProjectName = (from y in db.Base_Project where y.ProjectId == x.ProjectId select y.ProjectName).First()
                   };
        }

        /// <summary>
        /// 查询分页列表数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string projectId)
        {
            return count;
        }
        /// <summary>
        /// 焊材库温湿度记录明细分页列表 
        /// </summary>
        /// <param name="eWeldRHRecordId"></param>
        /// <returns></returns>
        public static IEnumerable GetEWeldRHRecordItemList(string eWeldRHRecordId)
        {
            return from x in Funs.DB.EWeldRHRecordItem
                   where x.EWeldRHRecordId == eWeldRHRecordId
                   select new
                       {
                           x.EWeldRHRecordItemId,
                           x.EWeldRHRecordId,
                           x.EWeldRHRecordMonth,
                           x.EWeldRHRecordDay,
                           x.EWeldRHRecordHours,
                           x.RoomTemperature,
                           x.Humidity,
                           x.RecordMan,
                           x.Remark
                       };
        }
        /// <summary>
        /// 根据焊材库温湿度记录Id获取焊材库温湿度记录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Model.EWeldRHRecord GetEWeldRHRecordByID(string id)
        {
            Model.HJGLDB db = Funs.DB;
            var eWeldRHRecord = db.EWeldRHRecord.FirstOrDefault(e => e.EWeldRHRecordId == id);
            return eWeldRHRecord;
        }
        /// <summary>
        /// 根据焊材库温湿度记录ID获取明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Model.EWeldRHRecordItem> GetEWeldRHRecordItemByID(string id)
        {
            return (from x in Funs.DB.EWeldRHRecordItem where x.EWeldRHRecordId == id select x).ToList();
        }
        /// <summary>
        /// 添加焊材库温湿度记录
        /// </summary>
        /// <param name="eWeldRHRecord"></param>
        public static void AddEWeldRHRecord(Model.EWeldRHRecord eWeldRHRecord)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EWeldRHRecord newEWeldRHRecord = new Model.EWeldRHRecord();

            newEWeldRHRecord.EWeldRHRecordId = eWeldRHRecord.EWeldRHRecordId;
            newEWeldRHRecord.EWeldRHRecordCode = eWeldRHRecord.EWeldRHRecordCode;
            newEWeldRHRecord.EWeldRHRecordDate = eWeldRHRecord.EWeldRHRecordDate;
            newEWeldRHRecord.CompileMan = eWeldRHRecord.CompileMan;
            newEWeldRHRecord.CompileDate = eWeldRHRecord.CompileDate;
            newEWeldRHRecord.ProjectId = eWeldRHRecord.ProjectId;
            newEWeldRHRecord.UnitId = eWeldRHRecord.UnitId;

            db.EWeldRHRecord.InsertOnSubmit(newEWeldRHRecord);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改焊材库温湿度记录
        /// </summary>
        /// <param name="eWeldRHRecord"></param>
        public static void UpdateEWeldRHRecord(Model.EWeldRHRecord eWeldRHRecord)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EWeldRHRecord newEWeldRHRecord = db.EWeldRHRecord.First(e => e.EWeldRHRecordId == eWeldRHRecord.EWeldRHRecordId);

            newEWeldRHRecord.EWeldRHRecordCode = eWeldRHRecord.EWeldRHRecordCode;
            newEWeldRHRecord.EWeldRHRecordDate = eWeldRHRecord.EWeldRHRecordDate;
            newEWeldRHRecord.CompileMan = eWeldRHRecord.CompileMan;
            newEWeldRHRecord.CompileDate = eWeldRHRecord.CompileDate;
            newEWeldRHRecord.ProjectId = eWeldRHRecord.ProjectId;
            newEWeldRHRecord.UnitId = eWeldRHRecord.UnitId;

            db.SubmitChanges();
        }
        /// <summary>
        /// 删除焊材库温湿度记录
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteEWeldRHRecord(string id)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EWeldRHRecord eWeldRHRecord = db.EWeldRHRecord.First(e => e.EWeldRHRecordId == id);
            db.EWeldRHRecord.DeleteOnSubmit(eWeldRHRecord);
            db.SubmitChanges();
        }
        /// <summary>
        /// 添加焊材库温湿度记录明细
        /// </summary>
        /// <param name="item"></param>
        public static void AddEWeldRHRecordItem(Model.EWeldRHRecordItem item)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EWeldRHRecordItem newItem = new Model.EWeldRHRecordItem();
            newItem.EWeldRHRecordItemId = SQLHelper.GetNewID(typeof(Model.EWeldRHRecordItem));
            newItem.EWeldRHRecordId = item.EWeldRHRecordId;
            newItem.EWeldRHRecordMonth = item.EWeldRHRecordMonth;
            newItem.EWeldRHRecordDay = item.EWeldRHRecordDay;
            newItem.EWeldRHRecordHours = item.EWeldRHRecordHours;
            newItem.RoomTemperature = item.RoomTemperature;
            newItem.Humidity = item.Humidity;
            newItem.RecordMan = item.RecordMan;
            newItem.Remark = item.Remark;

            db.EWeldRHRecordItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据焊材库温湿度记录ID删除所有相关明细信息
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteEWeldRHRecordItem(string id)
        {
            Model.HJGLDB db = Funs.DB;
            var q = (from x in db.EWeldRHRecordItem where x.EWeldRHRecordId == id select x).ToList();
            db.EWeldRHRecordItem.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable GetListDataPrint(int startDate, int endDate)
        {
            return from x in db.EWeldRHRecordItem
                   join y in db.EWeldRHRecord on x.EWeldRHRecordId equals y.EWeldRHRecordId
                   where x.EWeldRHRecordMonth >= startDate && x.EWeldRHRecordMonth <= endDate && y.EWeldRHRecordDate.Value.Year == DateTime.Now.Year
                   select new
                       {
                           x.EWeldRHRecordItemId,
                           x.EWeldRHRecordId,
                           x.EWeldRHRecordMonth,
                           x.EWeldRHRecordDay,
                           x.EWeldRHRecordHours,
                           x.RoomTemperature,
                           x.Humidity,
                           x.RecordMan,
                           x.Remark
                       };
        }
    }
}
