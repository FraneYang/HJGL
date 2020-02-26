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
    using System.Collections.Generic;

    public static class UserService
    {
        public static Model.HJGLDB db1 = Funs.DB;

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
        private static IQueryable<Model.Sys_User> qq = from x in db1.Sys_User orderby x.UserCode select x;

        /// <summary>
        /// 用户登录成功方法
        /// </summary>
        /// <param name="loginname">登录成功名</param>
        /// <param name="password">未加密密码</param>
        /// <param name="rememberMe">记住我开关</param>
        /// <param name="page">调用页面</param>
        /// <returns>是否登录成功</returns>
        public static bool UserLogOn(string account, string password, string projectId, bool rememberMe, System.Web.UI.Page page)
        {
            List<Model.Sys_User> x = null;
            List<string> mainUserAccounts = (from a in Funs.DB.Sys_User where a.ProjectId == null select a.Account).ToList();
            if (account == Const.GLY || mainUserAccounts.Contains(account))
            {
                x = (from y in Funs.DB.Sys_User
                     where y.Account == account && y.IsPost == true && y.Password == EncryptionPassword(password)
                     select y).ToList();
            }
            else
            {
                x = (from y in Funs.DB.Sys_User
                     where y.Account == account && y.IsPost == true && y.Password == EncryptionPassword(password) && y.ProjectId == projectId
                     select y).ToList();
            }
            if (x.Any())
            {
                FormsAuthentication.SetAuthCookie(account, false);
                page.Session[SessionName.CurrUser] = x.First();
                if (rememberMe)
                {
                    System.Web.HttpCookie u = new System.Web.HttpCookie("UserInfo");
                    u["username"] = account;
                    u["password"] = password;
                    u["projectId"] = projectId;
                    // Cookies过期时间设置为一年.
                    u.Expires = DateTime.Now.AddYears(1);
                    page.Response.Cookies.Add(u);
                }
                else
                {
                    // 当选择不保存用户名时,Cookies过期时间设置为昨天.
                    page.Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">加密前的密码</param>
        /// <returns>加密后的密码</returns>
        public static string EncryptionPassword(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>用户信息</returns>
        public static Model.Sys_User GetUserName(string userId)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// 根据用户主键获取角色主键
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>角色主键</returns>
        public static string GetRoleIdByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.RoleId;
        }

        /// <summary>
        /// 根据部门Id筛选用户信息,用户名模糊查询所有用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUsersByRoleId(string userName, string roleId)
        {
            return (from x in Funs.DB.Sys_User where x.UserName.Contains(userName) && x.RoleId == roleId select x).ToList();
        }

        /// <summary>
        /// 根据用户名模糊查询所有用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUsersByUserName(string userName, string projectId)
        {
            return (from x in Funs.DB.Sys_User where x.UserName.Contains(userName) && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 根据角色,用户名模糊查询所有用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUsersByUserName(string userName, string roleId, string projectId)
        {
            return (from x in Funs.DB.Sys_User where x.UserName.Contains(userName) && x.RoleId == roleId && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 根据用户Id查询所有用户的数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>用户的数量</returns>
        public static int GetUserCount(string userId)
        {
            var q = (from x in Funs.DB.Sys_User where x.UserId == userId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据帐号获取用户信息
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns>用户信息</returns>
        public static Model.Sys_User GetUserByAccount(string account, string projectId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.Account == account && e.ProjectId == projectId);
            return m;
        }

        /// <summary>
        /// 根据用户获取密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetPasswordByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.Password;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static void UpdatePassword(string userId, string password)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_User m = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
            m.Password = EncryptionPassword(password);
            db.SubmitChanges();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string searchItem, string searchValue, string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Sys_User> q = qq;
            q = q.Where(e => !e.Account.Contains("admin") && !e.Account.Contains("gly"));

            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.UserName)
                    {
                        q = q.Where(e => e.UserName.Contains(searchValue));
                    }
                }
            }
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            else
            {
                q = q.Where(e => e.ProjectId == null);
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }

            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.UserId,
                       x.Account,
                       x.UserCode,
                       x.Password,
                       x.UserName,
                       Role = (from y in db1.Sys_Role where y.RoleId == x.RoleId select y.RoleName).First(),
                       UnitName = (from y in db1.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                       x.IsPost,
                       x.Remark,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string searchItem, string searchValue, string projectId)
        {
            return count;
        }

        /// <summary>
        /// 增加人员信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddUser(Model.Sys_User user)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_User));
            Model.Sys_User newUser = new Model.Sys_User();
            newUser.UserId = newKeyID;
            newUser.Account = user.Account;
            newUser.UserCode = user.UserCode;
            newUser.Password = user.Password;
            newUser.UserName = user.UserName;
            newUser.UnitId = user.UnitId;
            newUser.RoleId = user.RoleId;
            newUser.IsPost = user.IsPost;
            newUser.ProjectId = user.ProjectId;
            newUser.Remark = user.Remark;

            db.Sys_User.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void UpdateUser(Model.Sys_User user)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_User newUser = db.Sys_User.First(e => e.UserId == user.UserId);
            newUser.Account = user.Account;
            newUser.UserCode = user.UserCode;
            newUser.Password = user.Password;
            newUser.UserName = user.UserName;
            newUser.UnitId = user.UnitId;
            newUser.RoleId = user.RoleId;
            newUser.IsPost = user.IsPost;
            newUser.ProjectId = user.ProjectId;
            newUser.Remark = user.Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteUser(string userId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_User user = db.Sys_User.First(e => e.UserId == userId);
            var q = from x in db.Sys_Log where x.UserId == userId select x;
            db.Sys_Log.DeleteAllOnSubmit(q);
            db.Sys_User.DeleteOnSubmit(user);
            db.SubmitChanges();
        }

        /// <summary>
        /// 人员排序下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchList()
        {
            ListItem[] lis = new ListItem[1];
            lis[0] = new ListItem("人员姓名", BLL.Const.UserName);
            return lis;
        }

        /// <summary>
        /// 查询所有在岗的用户
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetUserList(string projectId)
        {
            var q = (from x in Funs.DB.Sys_User where x.IsPost == true && x.ProjectId == projectId orderby x.UserId select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].UserName ?? "", q[i].UserId.ToString());
            }
            return lis;
        }

        /// <summary>
        /// 查询所有在岗的用户
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetGly()
        {
            var q = (from x in Funs.DB.Sys_User where x.IsPost == true && (x.Account == Const.GLY || x.ProjectId == null) orderby x.UserId select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].UserName ?? "", q[i].UserId.ToString());
            }
            return lis;
        }

        /// <summary>
        /// 根据角色获得所有对应在岗的用户
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetUserListByRoleId(string roleId, string projectId)
        {
            var q = (from x in Funs.DB.Sys_User where x.IsPost == true && x.RoleId == roleId && x.ProjectId == projectId orderby x.UserId select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].UserName ?? "", q[i].UserId.ToString());
            }
            return lis;
        }

        /// <summary>
        /// 根据角色获得所有对应在岗的总包用户
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetLPECUserListByRoleId(string roleId, string projectId)
        {
            var q = (from x in Funs.DB.Sys_User join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId where x.IsPost == true && x.RoleId == roleId && y.UnitType == "5" && x.ProjectId == projectId orderby x.UserId select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].UserName ?? "", q[i].UserId.ToString());
            }
            return lis;
        }

        /// <summary>
        /// 获取具有流程审批的用户
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetUserListHasApprove()
        {
            var q = (from x in Funs.DB.Sys_User where x.IsPost == true && (from y in Funs.DB.Sys_Role where y.IsAuditFlow == true select y.RoleId).Contains(x.RoleId) orderby x.UserId select x).ToList();
            ListItem[] lis = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                lis[i] = new ListItem(q[i].UserName ?? "", q[i].UserId.ToString());
            }
            return lis;
        }

        /// <summary>
        /// 根据角色获得用户数
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public static int GetUserCountByRole(string role)
        {
            var q = (from x in Funs.DB.Sys_User where x.RoleId == role select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据单位主键获得用户的数量
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <returns></returns>
        public static int GetUserCountByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.Sys_User where x.UnitId == unitId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据角色获得最大编号的在岗用户
        /// </summary>
        /// <returns></returns>
        public static Model.Sys_User GetMaxUserByRoleId(string roleId)
        {
            return (from x in Funs.DB.Sys_User where x.IsPost == true && x.RoleId == roleId orderby x.UserCode descending select x).FirstOrDefault();
        }
    }
}
