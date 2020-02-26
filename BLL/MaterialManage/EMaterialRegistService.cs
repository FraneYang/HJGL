using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 材料到货登记及验收记录
    /// </summary>
    public class EMaterialRegistService
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
        private static IQueryable<Model.EMaterialRegist> qq = from x in db.EMaterialRegist orderby x.EMaterialRegistCode select x;

        /// <summary>
        /// 分页查询列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.EMaterialRegist> q = qq;
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
                       x.EMaterialRegistId,
                       x.EMaterialRegistCode,
                       x.EMaterialRegistDate,
                       x.DeliveryMan,
                       CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                       x.CompileDate,
                       x.UnitName,
                       ProjectName = (from y in db.Base_Project where y.ProjectId == x.ProjectId select y.ProjectName).First()
                   };
        }

        /// <summary>
        /// 分页查询数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string projectId)
        {
            return count;
        }

        /// <summary>
        /// 材料到货登记及验收记录明细分页列表
        /// </summary>
        /// <param name="eMaterialRegistId"></param>
        /// <returns></returns>
        public static IEnumerable GetEMaterialRegistItemList(string eMaterialRegistId)
        {
            return from x in Funs.DB.EMaterialRegistItem
                   where x.EMaterialRegistId == eMaterialRegistId
                   select new
                       {
                           x.EMaterialRegistItemId,
                           x.EMaterialRegistId,
                           MatName = (from y in Funs.DB.BS_WeldMaterial where y.WMT_ID == x.WMT_ID select y.WMT_MatName).First(),
                           x.SpecificationsModel,                          
                           x.UnitName,
                           x.MaterialCount,
                           x.ItemCode,
                           x.Testrecords,
                           x.Models
                       };
        }

        /// <summary>
        /// 根据材料到货登记及验收记录Id获取材料到货登记及验收记录信息
        /// </summary>
        /// <param name="eMaterialRegistId"></param>
        /// <returns></returns>
        public static Model.EMaterialRegist GetEMaterialRegistByID(string eMaterialRegistId)
        {
            Model.HJGLDB db = Funs.DB;
            var eMaterialRegist = db.EMaterialRegist.FirstOrDefault(e => e.EMaterialRegistId == eMaterialRegistId);
            return eMaterialRegist;
        }

        /// <summary>
        /// 根据材料到货登记及验收记录Id获取明细信息
        /// </summary>
        /// <param name="eMaterialRegistId"></param>
        /// <returns></returns>
        public static List<Model.EMaterialRegistItem> GetEMaterialRegistItemByRegistId(string eMaterialRegistId)
        {
            return (from x in Funs.DB.EMaterialRegistItem where x.EMaterialRegistId == eMaterialRegistId select x).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Model.EMaterialRegistItem> GetRegistItem()
        {
            return (from x in Funs.DB.EMaterialRegistItem select x).ToList();
        }

        /// <summary>
        /// 添加材料到货登记及验收记录
        /// </summary>
        /// <param name="eMaterialRegist"></param>
        public static void AddEMaterialRegist(Model.EMaterialRegist eMaterialRegist)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EMaterialRegist newEMaterialRegist = new Model.EMaterialRegist();

            newEMaterialRegist.EMaterialRegistId = eMaterialRegist.EMaterialRegistId;
            newEMaterialRegist.EMaterialRegistCode = eMaterialRegist.EMaterialRegistCode;
            newEMaterialRegist.EMaterialRegistDate = eMaterialRegist.EMaterialRegistDate;
            newEMaterialRegist.DeliveryMan = eMaterialRegist.DeliveryMan;
            newEMaterialRegist.UnitName = eMaterialRegist.UnitName;
            newEMaterialRegist.CompileMan = eMaterialRegist.CompileMan;
            newEMaterialRegist.CompileDate = eMaterialRegist.CompileDate;
            newEMaterialRegist.ProjectId = eMaterialRegist.ProjectId;

            db.EMaterialRegist.InsertOnSubmit(newEMaterialRegist);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改材料到货登记及验收记录
        /// </summary>
        /// <param name="eMaterialRegist"></param>
        public static void UpdateEMaterialRegist(Model.EMaterialRegist eMaterialRegist)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EMaterialRegist newEMaterialRegist = db.EMaterialRegist.First(e => e.EMaterialRegistId == eMaterialRegist.EMaterialRegistId);

            newEMaterialRegist.EMaterialRegistCode = eMaterialRegist.EMaterialRegistCode;
            newEMaterialRegist.EMaterialRegistDate = eMaterialRegist.EMaterialRegistDate;
            newEMaterialRegist.DeliveryMan = eMaterialRegist.DeliveryMan;
            newEMaterialRegist.UnitName = eMaterialRegist.UnitName;
            newEMaterialRegist.CompileMan = eMaterialRegist.CompileMan;
            newEMaterialRegist.CompileDate = eMaterialRegist.CompileDate;
            newEMaterialRegist.ProjectId = eMaterialRegist.ProjectId;

            db.SubmitChanges();
        }
        /// <summary>
        /// 删除材料到货登记及验收记录
        /// </summary>
        /// <param name="eMaterialRegistId"></param>
        public static void DeleteEMaterialRegist(string eMaterialRegistId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EMaterialRegist eMaterialRegist = db.EMaterialRegist.First(e => e.EMaterialRegistId == eMaterialRegistId);

            db.EMaterialRegist.DeleteOnSubmit(eMaterialRegist);
            db.SubmitChanges();
        }

        /// <summary>
        /// 添加材料到货登记及验收记录明细
        /// </summary>
        /// <param name="item"></param>
        public static void AddEMaterialRegistItem(Model.EMaterialRegistItem item)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EMaterialRegistItem newItem = new Model.EMaterialRegistItem();

            newItem.EMaterialRegistItemId = item.EMaterialRegistItemId;
            newItem.EMaterialRegistId = item.EMaterialRegistId;
            newItem.WMT_ID = item.WMT_ID;
            newItem.SpecificationsModel = item.SpecificationsModel;           
            newItem.UnitName = item.UnitName;
            newItem.MaterialCount = item.MaterialCount;
            newItem.ItemCode = item.ItemCode;
            newItem.Testrecords = item.Testrecords;
            newItem.Models = item.Models;

            db.EMaterialRegistItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据材料到货登记及验收记录Id删除所有相关明细
        /// </summary>
        /// <param name="eMaterialRegistId"></param>
        public static void DeleteEMaterialRegistItem(string projectId, string eMaterialRegistId)
        {
            Model.HJGLDB db = Funs.DB;
            var deleteItem = from x in db.EMaterialRegistItem where x.EMaterialRegistId == eMaterialRegistId select x;
            /// 取到货明细  库存减去明细值

            if (deleteItem.Count() > 0)
            {
                foreach (var ditem in deleteItem)
                {
                    int count = 0;
                    if (ditem.MaterialCount.HasValue)
                    {
                        count = count - ditem.MaterialCount.Value;
                    }

                    if (count < 0)
                    {
                        BLL.EMInventoryRecordsService.UpdateEMInventoryRecords(projectId, ditem.WMT_ID, ditem.Models, ditem.SpecificationsModel, count);
                    }
                }

                db.EMaterialRegistItem.DeleteAllOnSubmit(deleteItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable GetListDataPrint(string eMaterialRegistId)
        {
            return from x in db.EMaterialRegistItem
                   join y in db.EMaterialRegist on x.EMaterialRegistId equals y.EMaterialRegistId
                   where y.EMaterialRegistId == eMaterialRegistId
                   orderby y.EMaterialRegistCode
                   select new
                       {
                           x.EMaterialRegistItemId,
                           x.EMaterialRegistId,
                           MatName = (from m in db.BS_WeldMaterial where m.WMT_ID == x.WMT_ID select m.WMT_MatName).First(),
                           x.SpecificationsModel,                          
                           x.UnitName,
                           x.MaterialCount,
                           x.ItemCode,
                           x.Testrecords,
                           x.Models
                       };
        }
    }
}
