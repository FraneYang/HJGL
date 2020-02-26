using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
   public static class SysMenuService
    {
       public static Model.HJGLDB db = Funs.DB;

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static ListItem[] GetSupMenuNameList()
       {
           var q = (from x in Funs.DB.Sys_Menu where x.SuperMenu == "0" orderby x.SortIndex select x).ToList();
           ListItem[] list = new ListItem[q.Count()];
           for (int i = 0; i < q.Count(); i++)
           {
               list[i] = new ListItem(q[i].MenuName ?? "", q[i].MenuId.ToString());
           }
           return list;
       }

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static ListItem[] GetMenuNameList(string supMenu)
       {
           var q = (from x in Funs.DB.Sys_Menu where x.SuperMenu==supMenu && x.SuperMenu != "0" orderby x.SortIndex select x).ToList();
           ListItem[] list = new ListItem[q.Count()];
           for (int i = 0; i < q.Count(); i++)
           {
               list[i] = new ListItem(q[i].MenuName ?? "", q[i].MenuId.ToString());
           }
           return list;
       }
    }
}
