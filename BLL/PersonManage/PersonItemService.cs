using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class PersonItemService
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
        private static IQueryable<Model.BS_Steel> qq = from x in db.BS_Steel
                                                       orderby x.STE_Code
                                                       select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="applyTypeId"></param>
        /// <param name="txtUnitcode"></param>
        /// <param name="redesignLevel"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_Steel> q = qq;

           
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
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="applyTypeId"></param>
        /// <param name="txtUnitcode"></param>
        /// <param name="redesignLevel"></param>
        /// <returns></returns>
        public static int getListCount()
        {
            return count;
        }

        /// <summary>
        /// 根据人员id和材质id判断是否在明细中
        /// </summary>
        /// <param name="year"></param>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public static bool IsInBS_WelderItemBS_Steel(string wenid, string steelId)
        {
            Model.HJGLDB db = Funs.DB;
            bool isIn = false;
            var welderItem = db.BS_WelderItem.FirstOrDefault(x => x.WED_ID == wenid && x.STE_ID == steelId);
            if (welderItem != null)
            {
                isIn = true;                      
            }

            return isIn;
        }

        /// <summary>
        /// 根据人员id和材质id获取明细信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public static Model.BS_WelderItem GetWelderToSteel(string wenid, string steelId)
        {
            Model.HJGLDB db = Funs.DB;
            var welderItem = db.BS_WelderItem.FirstOrDefault(x => x.WED_ID == wenid && x.STE_ID == steelId);
            if (welderItem != null)
            {
                return welderItem;
            }

            else
            {
                return null;
            }
        }
       
        /// <summary>
        /// 根据人员Id删除人员详细
        /// </summary>
        /// <param name="shortListId"></param>
        public static void DeleteItemByWenId(string id)
        {
            Model.HJGLDB db = Funs.DB;
            var q = (from x in db.BS_WelderItem where x.WED_ID == id select x).ToList();
            db.BS_WelderItem.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 人员明细表增加
        /// </summary>
        /// <param name="unitShortList"></param>
        public static void AddWelderItem(Model.BS_WelderItem item)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WelderItem newItem = new Model.BS_WelderItem();
            newItem.WEDItem_ID = item.WEDItem_ID;
            newItem.WED_ID = item.WED_ID;
            newItem.STE_ID = item.STE_ID;
            newItem.ThicknessMin = item.ThicknessMin;
            newItem.ThicknessMax = item.ThicknessMax;
            newItem.SizesMin = item.SizesMin;
            newItem.SizesMax = item.SizesMax;

            Funs.DB.BS_WelderItem.InsertOnSubmit(newItem);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 获取所有材质信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.BS_Steel> GetSteelList()
        {
            return (from x in Funs.DB.BS_Steel orderby x.STE_Code select x).ToList();
        }
    }
}
