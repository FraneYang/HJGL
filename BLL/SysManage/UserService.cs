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
        /// ��¼��
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// �������
        /// </summary>
        private static IQueryable<Model.Sys_User> qq = from x in db1.Sys_User orderby x.UserCode select x;

        /// <summary>
        /// �û���¼�ɹ�����
        /// </summary>
        /// <param name="loginname">��¼�ɹ���</param>
        /// <param name="password">δ��������</param>
        /// <param name="rememberMe">��ס�ҿ���</param>
        /// <param name="page">����ҳ��</param>
        /// <returns>�Ƿ��¼�ɹ�</returns>
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
                    // Cookies����ʱ������Ϊһ��.
                    u.Expires = DateTime.Now.AddYears(1);
                    page.Response.Cookies.Add(u);
                }
                else
                {
                    // ��ѡ�񲻱����û���ʱ,Cookies����ʱ������Ϊ����.
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
        /// ��������
        /// </summary>
        /// <param name="password">����ǰ������</param>
        /// <returns>���ܺ������</returns>
        public static string EncryptionPassword(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û���Ϣ</returns>
        public static Model.Sys_User GetUserName(string userId)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// �����û�������ȡ��ɫ����
        /// </summary>
        /// <param name="userId">�û�����</param>
        /// <returns>��ɫ����</returns>
        public static string GetRoleIdByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.RoleId;
        }

        /// <summary>
        /// ���ݲ���Idɸѡ�û���Ϣ,�û���ģ����ѯ�����û���Ϣ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUsersByRoleId(string userName, string roleId)
        {
            return (from x in Funs.DB.Sys_User where x.UserName.Contains(userName) && x.RoleId == roleId select x).ToList();
        }

        /// <summary>
        /// �����û���ģ����ѯ�����û���Ϣ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUsersByUserName(string userName, string projectId)
        {
            return (from x in Funs.DB.Sys_User where x.UserName.Contains(userName) && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// ���ݽ�ɫ,�û���ģ����ѯ�����û���Ϣ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUsersByUserName(string userName, string roleId, string projectId)
        {
            return (from x in Funs.DB.Sys_User where x.UserName.Contains(userName) && x.RoleId == roleId && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// �����û�Id��ѯ�����û�������
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û�������</returns>
        public static int GetUserCount(string userId)
        {
            var q = (from x in Funs.DB.Sys_User where x.UserId == userId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// �����ʺŻ�ȡ�û���Ϣ
        /// </summary>
        /// <param name="account">�ʺ�</param>
        /// <returns>�û���Ϣ</returns>
        public static Model.Sys_User GetUserByAccount(string account, string projectId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.Account == account && e.ProjectId == projectId);
            return m;
        }

        /// <summary>
        /// �����û���ȡ����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetPasswordByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.Password;
        }

        /// <summary>
        /// �޸�����
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
        /// ��ȡ��ҳ�б�
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
        /// ��ȡ�б���
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string searchItem, string searchValue, string projectId)
        {
            return count;
        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
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
        /// �޸���Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
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
        /// ������ԱIdɾ��һ����Ա��Ϣ
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
        /// ��Ա����������
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchList()
        {
            ListItem[] lis = new ListItem[1];
            lis[0] = new ListItem("��Ա����", BLL.Const.UserName);
            return lis;
        }

        /// <summary>
        /// ��ѯ�����ڸڵ��û�
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
        /// ��ѯ�����ڸڵ��û�
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
        /// ���ݽ�ɫ������ж�Ӧ�ڸڵ��û�
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
        /// ���ݽ�ɫ������ж�Ӧ�ڸڵ��ܰ��û�
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
        /// ��ȡ���������������û�
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
        /// ���ݽ�ɫ����û���
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <returns></returns>
        public static int GetUserCountByRole(string role)
        {
            var q = (from x in Funs.DB.Sys_User where x.RoleId == role select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// ���ݵ�λ��������û�������
        /// </summary>
        /// <param name="unitId">��λ����</param>
        /// <returns></returns>
        public static int GetUserCountByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.Sys_User where x.UnitId == unitId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// ���ݽ�ɫ�������ŵ��ڸ��û�
        /// </summary>
        /// <returns></returns>
        public static Model.Sys_User GetMaxUserByRoleId(string roleId)
        {
            return (from x in Funs.DB.Sys_User where x.IsPost == true && x.RoleId == roleId orderby x.UserCode descending select x).FirstOrDefault();
        }
    }
}
