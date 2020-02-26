using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 材料库存记录
    /// </summary>
    public static class EMInventoryRecordsService
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
        private static IQueryable<Model.EMInventoryRecords> qq = from x in db.EMInventoryRecords orderby x.UpdateTime select x;
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="materialName"></param>
        /// <param name="models"></param>
        /// <param name="specifications"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string projectId, string materialid, string models, string specifications, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.EMInventoryRecords> q = qq;
            q = q.Where(e => e.ProjectId == projectId);

            if (!string.IsNullOrEmpty(materialid) && materialid != "0")
            {
                q = q.Where(e => e.WMT_ID == materialid);
            }
            if (!string.IsNullOrEmpty(models))
            {
                q = q.Where(e => e.Model.Contains(models));
            }
            if (!string.IsNullOrEmpty(specifications))
            {
                q = q.Where(e => e.Specification.Contains(specifications));
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.InventoryId,
                       MaterialName = (from p in db.BS_WeldMaterial where p.WMT_ID == x.WMT_ID select p.WMT_MatName).First(),
                       x.Model,
                       x.Specification,
                       x.MaterialCount
                   };
        }
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="materialName"></param>
        /// <param name="models"></param>
        /// <param name="specifications"></param>
        /// <returns></returns>
        public static int GetListCount(string projectId, string materialid, string models, string specifications)
        {
            return count;
        }

        /// <summary>
        /// 库存记录数据处理
        /// </summary>
        /// <param name="eMInventoryRecords"></param>
        public static void UpdateEMInventoryRecords(string projectId, string WMT_ID, string Model, string Specification, int MaterialCount)
        {
            Model.HJGLDB db = Funs.DB;
            Model.EMInventoryRecords emInventoryRecords = db.EMInventoryRecords.FirstOrDefault(x => x.ProjectId == projectId && x.WMT_ID == WMT_ID && x.Model == Model && x.Specification == Specification);
            if (emInventoryRecords != null)
            {
                emInventoryRecords.MaterialCount += MaterialCount;

            }
            else
            {
                Model.EMInventoryRecords newEMInventoryRecords = new Model.EMInventoryRecords();
                newEMInventoryRecords.InventoryId = SQLHelper.GetNewID(typeof(Model.EMInventoryRecords));
                newEMInventoryRecords.ProjectId = projectId;
                newEMInventoryRecords.WMT_ID = WMT_ID;
                newEMInventoryRecords.Model = Model;
                newEMInventoryRecords.Specification = Specification;
                newEMInventoryRecords.MaterialCount = MaterialCount;
                newEMInventoryRecords.UpdateTime = System.DateTime.Now;
                db.EMInventoryRecords.InsertOnSubmit(newEMInventoryRecords);
            }

            db.SubmitChanges();
        }
    }
}
