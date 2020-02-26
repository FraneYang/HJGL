using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public static class ButtonToMenuService
    {       
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="buttonToMenu"></param>
        public static void AddButtonToMenu(Model.ButtonToMenu buttonToMenu)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.ButtonToMenu));
            Model.ButtonToMenu newButtonToMenu = new Model.ButtonToMenu();

            newButtonToMenu.ButtonToMenuId = newKeyID;
            newButtonToMenu.MenuId = buttonToMenu.MenuId;
            newButtonToMenu.ButtonName = buttonToMenu.ButtonName;
            newButtonToMenu.SortIndex = buttonToMenu.SortIndex;

            db.ButtonToMenu.InsertOnSubmit(newButtonToMenu);
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menuId"></param>
        public static void DeleteButtonToMenu(string menuId)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.ButtonToMenu where x.MenuId == menuId select x;

            if (q != null)
            {
                db.ButtonToMenu.DeleteAllOnSubmit(q);
            }

            db.SubmitChanges();
        }

       /// <summary>
       /// 根据menuId获取按钮权限信息
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
        public static Model.ButtonToMenu GetButtonToMenuByMenuId(string menuId)
        {
            Model.HJGLDB db = Funs.DB;
            return db.ButtonToMenu.FirstOrDefault(e => e.MenuId == menuId);
        }

        /// <summary>
        /// 根据buttonName获取按钮权限信息
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static Model.ButtonToMenu GetButtonToMenuByButtonName(string menuId,string buttonName)
        {
            return Funs.DB.ButtonToMenu.FirstOrDefault(e => e.ButtonName == buttonName && e.MenuId ==menuId);
        }
    }
}
