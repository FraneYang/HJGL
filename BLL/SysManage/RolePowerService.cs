using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class RolePowerService
    {
        /// <summary>
        /// 增加角色权限
        /// </summary>
        /// <param name="power"></param>
        public static void SaveRolePower(Model.Sys_RolePower power)
        {
            Model.HJGLDB db = Funs.DB;
            string newRolePower = BLL.SQLHelper.GetNewID(typeof(Model.Sys_RolePower));
            Model.Sys_RolePower newPower = new Model.Sys_RolePower();
            newPower.RolePowerId = newRolePower;
            newPower.RoleId = power.RoleId;
            newPower.MenuId = power.MenuId;

            db.Sys_RolePower.InsertOnSubmit(newPower);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断菜单是否存在
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static bool IsExistMenu(string menuId)
        {
            Model.Sys_Menu m = Funs.DB.Sys_Menu.FirstOrDefault(e => e.MenuId == menuId);
            if (m != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="postId"></param>
        public static void DeleteRolePower(string roleId)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.Sys_RolePower where x.RoleId == roleId select x;

            if (q != null)
            {
                db.Sys_RolePower.DeleteAllOnSubmit(q);

            }

            db.SubmitChanges();
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleId"></param>
        public static List<Model.Sys_RolePower> GetRolePower(string roleId)
        {
            List<Model.Sys_RolePower> powerList = Funs.DB.Sys_RolePower.Where(e => e.RoleId == roleId).ToList();
            return powerList;
        }

        /// <summary>
        /// 根据角色Id查询所有系统功能授权信息的数量
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>授权的数量</returns>
        public static int GetPostPowerCountByPostId(string roleId)
        {
            var q = (from x in Funs.DB.Sys_RolePower where x.RoleId == roleId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据角色主键获得角色权限数量
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <returns></returns>
        public static int GetPostPowerCountByRoleId(string roleId)
        {
            var q = (from x in Funs.DB.Sys_RolePower where x.RoleId == roleId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据角色主键获得对应的菜单权限

        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <returns>菜单ID数组</returns>
        public static string[] GetMenuIdByRoleId(string roleId)
        {
            var q = Funs.DB.Sys_RolePower.Where(e => e.RoleId == roleId);
            string[] menuId = new string[q.Count()];
            if (q.Count() > 0)
            {
                int i = 0;
                foreach (var menu in q)
                {
                    menuId[i] = menu.MenuId;
                    i++;
                }

                return menuId;
            }
            else
            {
                return null;
            }
        }
    }
}
