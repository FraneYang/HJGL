using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 材质
    /// </summary>
    public static class MaterialService
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
        private static IQueryable<Model.BS_Steel> qq = from x in db.BS_Steel orderby x.STE_Code select x;

        /// <summary>
        /// 获取材质列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_Steel> q = qq;
            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.STE_Code)
                    {
                        q = q.Where(e => e.STE_Code.Contains(searchValue));
                    }
                    if (searchItem == BLL.Const.STE_Name)
                    {
                        q = q.Where(e => e.STE_Name.Contains(searchValue));
                    }

                    if (searchItem == BLL.Const.STE_SteType)
                    {
                      var ste = (from x in  GetSteTypeList() where x.Text.Contains(searchValue) select x.Value).ToList();

                      q = q.Where(e => ste.Contains(e.STE_SteelType.ToString()));
                    }
                }
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.STE_ID,
                       x.STE_Code,
                       x.STE_Name,
                       x.STE_SteelType,
                       x.STE_Remark,
                       x.MaterialType,
                       x.MaterialGroup
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string searchItem, string searchValue)
        {
            return count;
        }

        /// <summary>
        /// 根据材质ID获取材质
        /// </summary>
        /// <param name="steId"></param>
        /// <returns></returns>
        public static Model.BS_Steel GetSteelBySteID(string steId)
        {
            return Funs.DB.BS_Steel.FirstOrDefault(e => e.STE_ID == steId);
        }

        /// <summary>
        /// 根据材质类别获取材质
        /// </summary>
        /// <param name="steId"></param>
        /// <returns></returns>
        public static List<Model.BS_Steel> GetSteelByMaterialType(string type)
        {
            var steel = from x in Funs.DB.BS_Steel where x.MaterialType == type select x;
            if (steel.Count() > 0)
            {
                return steel.ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据材质组别获取材质
        /// </summary>
        /// <param name="steId"></param>
        /// <returns></returns>
        public static List<Model.BS_Steel> GetSteelByMaterialGroup(string groups)
        {
            var steel = from x in Funs.DB.BS_Steel where x.MaterialGroup == groups select x;
            if (steel.Count() > 0)
            {
                return steel.ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加材质
        /// </summary>
        /// <param name="steel"></param>
        public static void AddSteel(Model.BS_Steel steel)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_Steel newSteel = new Model.BS_Steel();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_Steel));

            newSteel.STE_ID = newKeyID;
            newSteel.STE_Code = steel.STE_Code;
            newSteel.STE_Name = steel.STE_Name;
            newSteel.STE_SteelType = steel.STE_SteelType;
            newSteel.STE_Remark = steel.STE_Remark;
            newSteel.MaterialType = steel.MaterialType;
            newSteel.MaterialGroup = steel.MaterialGroup;

            db.BS_Steel.InsertOnSubmit(newSteel);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改材质
        /// </summary>
        /// <param name="steel"></param>
        public static void UpdateSteel(Model.BS_Steel steel)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_Steel newSteel = db.BS_Steel.FirstOrDefault(e => e.STE_ID == steel.STE_ID);
            newSteel.STE_Code = steel.STE_Code;
            newSteel.STE_Name = steel.STE_Name;
            newSteel.STE_SteelType = steel.STE_SteelType;
            newSteel.STE_Remark = steel.STE_Remark;
            newSteel.MaterialType = steel.MaterialType;
            newSteel.MaterialGroup = steel.MaterialGroup;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ste_Id"></param>
        public static void DeleteSteel(string ste_Id)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_Steel steel = db.BS_Steel.FirstOrDefault(e => e.STE_ID == ste_Id);
            db.BS_Steel.DeleteOnSubmit(steel);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的材质代码
        /// </summary>
        /// <param name="ste_code"></param>
        /// <returns></returns>
        public static bool IsExitSteelCode(string ste_code)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_Steel where x.STE_Code == ste_code select x;

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
        /// 查询下拉列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchItem()
        {
            ListItem[] list = new ListItem[3];
            list[0] = new ListItem("材质代号", BLL.Const.STE_Code);
            list[1] = new ListItem("材质名称", BLL.Const.STE_Name);
            list[2] = new ListItem("钢材类型", BLL.Const.STE_SteType);

            return list;
        }

        /// <summary>
        /// 查询钢材类型下拉列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSteTypeList()
        {
            ListItem[] list = new ListItem[4];
            list[0] = new ListItem("碳钢", "1");
            list[1] = new ListItem("不锈钢", "2");
            list[2] = new ListItem("鉻目钢", "3");
            list[3] = new ListItem("其他", "4");

            return list;
        }

        /// <summary>
        /// 根据材质代码获取材质信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_Steel GetSteelBySteelCode(string steelCode)
        {
            return Funs.DB.BS_Steel.FirstOrDefault(x => x.STE_Code == steelCode);
        }

        /// <summary>
        /// 获取材质信息项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSteelList()
        {
            var q = (from x in Funs.DB.BS_Steel orderby x.STE_Code select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].STE_Name ?? "", q[i].STE_ID.ToString());
            }
            return item;
        }
    }
}
