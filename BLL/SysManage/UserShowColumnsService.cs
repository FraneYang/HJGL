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

namespace BLL
{
    public static class UserShowColumnsService
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
        private static IQueryable<Model.Sys_UserShowColumns> qq = from x in db.Sys_UserShowColumns orderby x.UserId descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Sys_UserShowColumns> q = qq;
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }

            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       UserName = (from u in db.Sys_User where u.UserId == x.UserId select u.UserName).First(),
                       x.ShowColumnId,
                       x.Columns,
                       x.ShowType,
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
        /// 根据用户ID获取信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static Model.Sys_UserShowColumns GetColumnsByUserId(string userId, string type)
        {
            return Funs.DB.Sys_UserShowColumns.FirstOrDefault(x => x.UserId == userId && x.ShowType == type);
        }

        /// <summary>
        /// 添加用户对应显示列信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="opUserShowColumns"></param>
        public static void AddUserShowColumns(Model.Sys_UserShowColumns showColumns)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_UserShowColumns newShowColumns = new Model.Sys_UserShowColumns();

            newShowColumns.ShowColumnId = SQLHelper.GetNewID(typeof(Model.Sys_UserShowColumns));
            newShowColumns.UserId = showColumns.UserId;
            newShowColumns.Columns = showColumns.Columns;
            newShowColumns.ShowType = showColumns.ShowType;
            db.Sys_UserShowColumns.InsertOnSubmit(newShowColumns);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改用户对应显示列信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="def"></param>
        public static void UpdateUserShowColumns(Model.Sys_UserShowColumns showColumns)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_UserShowColumns newShowColumns = db.Sys_UserShowColumns.First(e => e.ShowColumnId == showColumns.ShowColumnId);
            newShowColumns.Columns = showColumns.Columns;
            newShowColumns.ShowType = showColumns.ShowType;
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除用户对应显示列信息
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteUserShowColumns(string showColumnId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_UserShowColumns newShowColumns = db.Sys_UserShowColumns.First(e => e.ShowColumnId == showColumnId);
            db.Sys_UserShowColumns.DeleteOnSubmit(newShowColumns);
            db.SubmitChanges();
        }
    }
}
