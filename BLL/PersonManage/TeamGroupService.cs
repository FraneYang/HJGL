using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using BLL;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TeamGroupService
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
        public static IQueryable<Model.HS_Education> qq = from x in db.HS_Education orderby x.EDU_Code select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string project, string EDU_Code, string EDU_Name, string EDU_Unit, string EDU_Main, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.HS_Education> q = qq;
            if (!string.IsNullOrEmpty(project))
            {
                q = q.Where(e => e.ProjectId == project);
            }
            if (!string.IsNullOrEmpty(EDU_Code))
            {
                q = q.Where(e => e.EDU_Code.Contains(EDU_Code));
            }
            if (!string.IsNullOrEmpty(EDU_Main))
            {
                q = q.Where(e => e.EDU_Main.Contains(EDU_Main));
            }
            if (!string.IsNullOrEmpty(EDU_Name))
            {
                q = q.Where(e => e.EDU_Name.Contains(EDU_Name));
            }
            if (EDU_Unit != "0")
            {
                q = q.Where(e => e.EDU_Unit == EDU_Unit);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.EDU_ID,
                       x.EDU_Code,
                       x.EDU_Name,
                       UnitName = (from y in db.Base_Unit where y.UnitId == x.EDU_Unit select y.UnitName).First(),
                       x.EDU_Main
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <param name="EDU_Name"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetListCount(string project, string EDU_Code, string EDU_Name, string EDU_Unit, string EDU_Main)
        {
            return count;
        }

        /// <summary>
        /// 根据班组id查询班组信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.HS_Education GetTeamGroupByTeamGroupId(string EDU_ID)
        {
            return Funs.DB.HS_Education.FirstOrDefault(e => e.EDU_ID == EDU_ID);
        }


        /// <summary>
        /// 添加班组信息
        /// </summary>
        /// <param name="?"></param>
        public static void AddTeamGroup(Model.HS_Education teamGroup)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.HS_Education));
            Model.HS_Education newTeamGroup = new Model.HS_Education();
            newTeamGroup.EDU_ID = newKeyID;
            newTeamGroup.EDU_Code = teamGroup.EDU_Code;
            newTeamGroup.EDU_Name = teamGroup.EDU_Name;
            newTeamGroup.EDU_Unit = teamGroup.EDU_Unit;
            newTeamGroup.EDU_Main = teamGroup.EDU_Main;
            newTeamGroup.ProjectId = teamGroup.ProjectId;
            db.HS_Education.InsertOnSubmit(newTeamGroup);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改班组信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateTeamGroup(Model.HS_Education teamGroup)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HS_Education newTeamGroup = db.HS_Education.First(e => e.EDU_ID == teamGroup.EDU_ID);
            newTeamGroup.EDU_Code = teamGroup.EDU_Code;
            newTeamGroup.EDU_Name = teamGroup.EDU_Name;
            newTeamGroup.EDU_Unit = teamGroup.EDU_Unit;
            newTeamGroup.EDU_Main = teamGroup.EDU_Main;

            db.SubmitChanges();
        }

        /// <summary>
        /// 是否存在班组编号
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTeamGroupCode(string EDU_Code)
        {
            var q = from x in Funs.DB.HS_Education where x.EDU_Code == EDU_Code select x;
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
        /// 删除班组信息
        /// </summary>
        /// <param name="EDU_ID"></param>
        public static void DeleteTeamGroup(string EDU_ID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.HS_Education teamGroup = db.HS_Education.First(e => e.EDU_ID == EDU_ID);
            db.HS_Education.DeleteOnSubmit(teamGroup);
            db.SubmitChanges();
        }

        /// <summary>
        /// 获取班组
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetEducationList(string projectId)
        {
            var q = (from x in Funs.DB.HS_Education where x.ProjectId == projectId orderby x.EDU_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].EDU_Name ?? "", q[i].EDU_ID.ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据单位获取班组
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetEducationListByUnitId(string projectId, string unitId)
        {
            var q = (from x in Funs.DB.HS_Education where x.ProjectId == projectId && x.EDU_Unit == unitId orderby x.EDU_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].EDU_Name ?? "", q[i].EDU_ID.ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据单位Id获取班组数
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetTeamGroupByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.HS_Education where x.EDU_Unit == unitId select x).ToList();
            return q.Count();
        }
    }
}
