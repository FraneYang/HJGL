 using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Web.UI.WebControls;

namespace BLL
{

    public static class ConsumablesService
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
        private static IQueryable<Model.BS_WeldMaterial> qq = from x in db.BS_WeldMaterial orderby x.WMT_MatCode select x;

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_WeldMaterial> q = qq;
            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.ConsumablesCode)
                    {
                        q = q.Where(e => e.WMT_MatCode.Contains(searchValue));
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
                       x.WMT_ID,
                       x.WMT_MatCode,
                       x.WMT_MatName,
                       x.WMT_MatType,
                       x.WMT_Remark
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int getListCount(string searchItem, string searchValue)
        {
            return count;
        }

        /// <summary>
        /// 根据焊接接口ID获取区域信息
        /// </summary>
        /// <param name="ConsumablesName"></param>
        /// <returns></returns>
        public static Model.BS_WeldMaterial getConsumablesByConsumablesId(string ConsumablesId)
        {
            return Funs.DB.BS_WeldMaterial.FirstOrDefault(e => e.WMT_ID == ConsumablesId);
        }

        /// <summary>
        /// 根据焊接接口Code获取区域信息
        /// </summary>
        /// <param name="ConsumablesName"></param>
        /// <returns></returns>
        public static bool IsExistConsumablesCode(string ConsumablesCode)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.BS_WeldMaterial where x.WMT_MatCode == ConsumablesCode select x;
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
        /// 添加焊接接口
        /// </summary>
        /// <param name="Consumables"></param>
        public static void AddConsumables(Model.BS_WeldMaterial Consumables)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_WeldMaterial));
            Model.BS_WeldMaterial newConsumables = new Model.BS_WeldMaterial();
            newConsumables.WMT_ID = newKeyID;
            newConsumables.WMT_MatCode = Consumables.WMT_MatCode;
            newConsumables.WMT_MatName = Consumables.WMT_MatName;
            newConsumables.WMT_MatType = Consumables.WMT_MatType;
            newConsumables.WMT_Remark = Consumables.WMT_Remark;

            db.BS_WeldMaterial.InsertOnSubmit(newConsumables);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接接口
        /// </summary>
        /// <param name="Consumables"></param>
        public static void updateConsumables(Model.BS_WeldMaterial Consumables)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WeldMaterial newConsumables = db.BS_WeldMaterial.First(e => e.WMT_ID == Consumables.WMT_ID)
;
            newConsumables.WMT_MatCode = Consumables.WMT_MatCode;
            newConsumables.WMT_MatName = Consumables.WMT_MatName;
            newConsumables.WMT_MatType = Consumables.WMT_MatType;
            newConsumables.WMT_Remark = Consumables.WMT_Remark;
            newConsumables.WMT_Remark = Consumables.WMT_Remark;
            db.SubmitChanges();

        }

        /// <summary>
        /// 根据焊接接口Id删除一个焊接接口信息
        /// </summary>
        /// <param name="ConsumablesId"></param>
        public static void DeleteConsumables(string ConsumablesId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WeldMaterial Consumables = db.BS_WeldMaterial.First(e => e.WMT_ID == ConsumablesId);
            db.BS_WeldMaterial.DeleteOnSubmit(Consumables);
            db.SubmitChanges();
        }

        /// <summary>
        /// 焊接接口下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchList()
        {
            ListItem[] lis = new ListItem[1];
            lis[0] = new ListItem("焊材代号", BLL.Const.ConsumablesCode);
            return lis;
        }

        /// <summary>
        /// 焊接接口下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] ConsumablesTypeList()
        {
            ListItem[] lis = new ListItem[2];
            lis[0] = new ListItem("焊丝", "1");
            lis[1] = new ListItem("焊条", "2");
            return lis;
        }

        /// <summary>
        /// 根据焊接接口代码获取焊接接口信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_WeldMaterial GetMaterialByMaterialCode(string materialCode)
        {
            return Funs.DB.BS_WeldMaterial.FirstOrDefault(x => x.WMT_MatCode == materialCode);
        }

        /// <summary>
        /// 获取材料信息名称
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetMaterialList()
        {
            var q = (from x in Funs.DB.BS_WeldMaterial orderby x.WMT_MatCode select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].WMT_MatName ?? "", q[i].WMT_ID.ToString());
            }
            return list;
        }


        /// <summary>
        /// 获取焊丝代号
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetMaterialSilkList()
        {
            var q = (from x in Funs.DB.BS_WeldMaterial where x.WMT_MatType=="1" orderby x.WMT_MatCode select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].WMT_MatCode ?? "", q[i].WMT_ID.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取焊条代号
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetMaterialWeldMatList()
        {
            var q = (from x in Funs.DB.BS_WeldMaterial where x.WMT_MatType == "2" orderby x.WMT_MatCode select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].WMT_MatCode ?? "", q[i].WMT_ID.ToString());
            }
            return list;
        }
    }
}
