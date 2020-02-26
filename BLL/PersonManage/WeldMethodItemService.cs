using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
   public static class WeldMethodItemService
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
       /// 定义项
       /// </summary>
       private static IQueryable<Model.BS_WeldMethod> qq = from x in db.BS_WeldMethod
                                                           orderby x.WME_Code
                                                           select x;

       /// <summary>
       /// 获取分页列表
       /// </summary>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable getListData(int startRowIndex, int maximumRows)
       {
           IQueryable<Model.BS_WeldMethod> q = qq;
           count = q.Count();
           if (count ==0)
           {
               return new object[] { "" };
           }
           return from x in q.Skip(startRowIndex).Take(maximumRows)
                  select new
                  {
                      x.WME_ID,
                      x.WME_Code,
                      x.WME_Name,
                      x.WME_Remark
                  };
       }

       /// <summary>
       /// 获取列表数
       /// </summary>
       /// <returns></returns>
       public static int getListCount()
       {
           return count;
       }

       /// <summary>
       /// 根据人员Id和焊接方法Id判断是否在焊接方法明细中
       /// </summary>
       /// <param name="wenId"></param>
       /// <param name="wmeId"></param>
       /// <returns></returns>
       public static bool IsInBS_WeldMethodItem(string wenId, string wmeId)
       {
           Model.HJGLDB db = Funs.DB;
           bool isIn = false;
           var weldMethod = db.BS_WeldMethodItem.FirstOrDefault(x => x.WED_ID == wenId && x.WME_ID == wmeId);
           if (weldMethod != null)
           {
               isIn = true;
           }
           return isIn;
       }

       /// <summary>
       /// 根据焊接人员Id删除焊接方法明细
       /// </summary>
       /// <param name="wenId"></param>
       public static void DeleteWeldMethodItem(string wenId)
       {
           Model.HJGLDB db = Funs.DB;
           var q = (from x in db.BS_WeldMethodItem where x.WED_ID == wenId select x).ToList();
           db.BS_WeldMethodItem.DeleteAllOnSubmit(q);
           db.SubmitChanges();
       }

       /// <summary>
       /// 添加焊接方法明细
       /// </summary>
       /// <param name="weldMethodItem"></param>
       public static void AddWeldMethodItem(Model.BS_WeldMethodItem weldMethodItem)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BS_WeldMethodItem newWeldMethodItem = new Model.BS_WeldMethodItem();
           newWeldMethodItem.WeldMethodItemId = weldMethodItem.WeldMethodItemId;
           newWeldMethodItem.WED_ID = weldMethodItem.WED_ID;
           newWeldMethodItem.WME_ID = weldMethodItem.WME_ID;

           db.BS_WeldMethodItem.InsertOnSubmit(newWeldMethodItem);
           db.SubmitChanges();
       }


       /// <summary>
       /// 获取所有焊接方法信息
       /// </summary>
       /// <returns></returns>
       public static List<Model.BS_WeldMethod> GetWeldMethodList()
       {
           return (from x in Funs.DB.BS_WeldMethod orderby x.WME_Code select x).ToList();
       }
    }
}
