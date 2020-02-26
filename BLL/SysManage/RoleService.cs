namespace BLL
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Data.Linq;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using Model;
    using BLL;

    public static class RoleService
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
        private static IQueryable<Model.Sys_Role> qq = from x in db.Sys_Role orderby x.SortIndex select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Sys_Role> q = qq;
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
                       x.RoleId,
                       x.RoleName,
                       x.SortIndex,
                       x.Def
                   };
        }

        /// <summary>
        /// 获取列表数

        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId)
        {
            return count;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public static Model.Sys_Role GetRole(string roleId)
        {
            return Funs.DB.Sys_Role.First(x => x.RoleId == roleId);
        }

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="def"></param>
        public static void AddRole(string roleName, string def, string roleType, int? sortIndex, string projectId)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_Role));
            Model.Sys_Role role = new Model.Sys_Role();
            role.RoleId = newKeyID;
            role.RoleName = roleName;
            role.RoleType = roleType;
            role.Def = def;
            role.SortIndex = sortIndex;
            role.ProjectId = projectId;

            db.Sys_Role.InsertOnSubmit(role);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="def"></param>
        public static void UpdateRole(string roleId, string roleName, string def, int? sortIndex)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_Role role = db.Sys_Role.First(e => e.RoleId == roleId);
            role.RoleName = roleName;
            role.Def = def;
            role.SortIndex = sortIndex;
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteRole(string roleId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_Role role = db.Sys_Role.First(e => e.RoleId == roleId);
            db.Sys_Role.DeleteOnSubmit(role);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在角色
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <returns>true:存在；false:不存在</returns>
        public static bool IsExistRole(string roleId)
        {
            Model.Sys_Role m = Funs.DB.Sys_Role.FirstOrDefault(e => e.RoleId == roleId);
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
        /// 根据角色判断用户是否存在
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <returns>true:存在；false:不存在</returns>
        public static bool IsExistUserByRole(string roleId, string userId)
        {
            bool isExist = false;
            if (userId == BLL.Const.AdminId)
            {
                isExist = true;
            }
            else
            {
                var m = from x in Funs.DB.Sys_User where x.RoleId == roleId select x;
                if (m.Count() > 0)
                {

                    if ((m.Where(z => z.UserId == userId) != null))
                    {
                        if (m.Where(z => z.UserId == userId).Count() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return isExist;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetRoleList(string projectId)
        {
            var q = (from x in Funs.DB.Sys_Role where x.ProjectId == projectId orderby x.SortIndex select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];

            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].RoleName ?? "", q[i].RoleId.ToString());
            }

            return lis;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetRoleList(bool isAuditFlow)
        {
            var q = (from x in Funs.DB.Sys_Role where x.IsAuditFlow == isAuditFlow orderby x.SortIndex select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];

            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].RoleName ?? "", q[i].RoleId.ToString());
            }

            return lis;
        }

    }
}
