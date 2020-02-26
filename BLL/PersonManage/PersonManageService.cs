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
    public class PersonManageService
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
       // public static IQueryable<Model.BS_Welder> qq = from x in db.BS_Welder orderby x.WED_Unit, x.WED_Code select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string project, string drpUnitS, string drpEducationS, string txtCodeS, string txtNameS, string txtWorkCodeS, string txtClassS,string IfOnGuard, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_PersonManageList> q = from x in db.View_PersonManageList where x.ProjectId == project orderby x.WED_Unit, x.WED_Code select x;
            if (IfOnGuard == "1")
            {
                q = q.Where(e => e.WED_IfOnGuard == true);
            }
            if (drpUnitS != "0" && !string.IsNullOrEmpty(drpUnitS))
            {
                q = q.Where(e => e.WED_Unit == drpUnitS);
            }
            if (drpEducationS != "0" && !string.IsNullOrEmpty(drpEducationS))
            {
                q = q.Where(e => e.EDU_ID == drpEducationS);
            }
            if (!string.IsNullOrEmpty(txtCodeS))
            {
                q = q.Where(e => e.WED_Code.Contains(txtCodeS));
            }
            if (!string.IsNullOrEmpty(txtNameS))
            {
                q = q.Where(e => e.WED_Name.Contains(txtNameS));
            }
            if (!string.IsNullOrEmpty(txtWorkCodeS))
            {
                q = q.Where(e => e.WED_WorkCode.Contains(txtWorkCodeS));
            }
            if (!string.IsNullOrEmpty(txtClassS))
            {
                q = q.Where(e => e.WED_Class.Contains(txtClassS));
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.WED_ID,
                       x.WED_Code,
                       x.WED_Name,
                       x.UnitName,
                       x.EDU_Name,
                       x.WED_Sex,
                       WED_Birthday = string.Format("{0:yyyy-MM-dd}", x.WED_Birthday),
                       x.WED_WorkCode,
                       x.WED_Class,
                       x.WED_IfOnGuardName,
                       x.WED_Remark,
                       x.Thickness,
                       x.Sizes,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <param name="EDU_Name"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetListCount(string project, string drpUnitS, string drpEducationS, string txtCodeS, string txtNameS, string txtWorkCodeS, string txtClassS, string IfOnGuard)
        {
            return count;
        }

        /// <summary>
        /// 根据id查询人员信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.BS_Welder GetBSWelderByTeamWEDID(string WED_ID)
        {
            return Funs.DB.BS_Welder.FirstOrDefault(e => e.WED_ID == WED_ID);
        }

      
        /// <summary>
        /// 添加人员信息
        /// </summary>
        /// <param name="?"></param>
        public static void AddBSWelder(Model.BS_Welder welder)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_Welder));
            Model.BS_Welder newWelder = new Model.BS_Welder();
            newWelder.WED_ID = newKeyID;
            newWelder.WED_Code = welder.WED_Code;
            newWelder.WED_Name = welder.WED_Name;
            newWelder.WED_Unit = welder.WED_Unit;
            newWelder.EDU_ID = welder.EDU_ID;
            newWelder.WED_Sex = welder.WED_Sex;
            newWelder.WED_Birthday = welder.WED_Birthday;
            newWelder.LimitDate = welder.LimitDate;
            newWelder.WED_WorkCode = welder.WED_WorkCode;
            newWelder.WED_Class = welder.WED_Class;
            newWelder.WED_IfOnGuard = welder.WED_IfOnGuard;
            newWelder.WED_Remark = welder.WED_Remark;
            newWelder.ThicknessMin = welder.ThicknessMin;
            newWelder.ThicknessMax = welder.ThicknessMax;
            newWelder.SizesMax = welder.SizesMax;
            newWelder.SizesMin = welder.SizesMin;
            newWelder.ProjectId = welder.ProjectId;
            newWelder.IdentityCard = welder.IdentityCard;
            newWelder.SE_EquipmentID = welder.SE_EquipmentID;

            db.BS_Welder.InsertOnSubmit(newWelder);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateBSWelder(Model.BS_Welder welder)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_Welder newWelder = db.BS_Welder.First(e => e.WED_ID == welder.WED_ID);           
            newWelder.WED_Code = welder.WED_Code;
            newWelder.WED_Name = welder.WED_Name;
            newWelder.WED_Unit = welder.WED_Unit;
            newWelder.EDU_ID = welder.EDU_ID;
            newWelder.WED_Sex = welder.WED_Sex;
            newWelder.WED_Birthday = welder.WED_Birthday;
            newWelder.LimitDate = welder.LimitDate;
            newWelder.WED_WorkCode = welder.WED_WorkCode;
            newWelder.WED_Class = welder.WED_Class;
            newWelder.WED_IfOnGuard = welder.WED_IfOnGuard;
            newWelder.WED_Remark = welder.WED_Remark;
            newWelder.ThicknessMin = welder.ThicknessMin;
            newWelder.ThicknessMax = welder.ThicknessMax;
            newWelder.SizesMax = welder.SizesMax;
            newWelder.SizesMin = welder.SizesMin;
            newWelder.IdentityCard = welder.IdentityCard;
            newWelder.SE_EquipmentID = welder.SE_EquipmentID;

            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateBSWelderItem(Model.BS_Welder welder)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_Welder newWelder = db.BS_Welder.First(e => e.WED_ID == welder.WED_ID);       
            newWelder.ThicknessMin = welder.ThicknessMin;
            newWelder.ThicknessMax = welder.ThicknessMax;
            newWelder.SizesMax = welder.SizesMax;
            newWelder.SizesMin = welder.SizesMin;
            db.SubmitChanges();
        }

        /// <summary>
        /// 是否存在人员编号
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistWEDName(string WED_Name, string projectId)
        {
            var q = from x in Funs.DB.BS_Welder where x.WED_Name == WED_Name && x.ProjectId == projectId select x;
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
        /// 是否存在人员编号
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistWEDCode(string WED_Code, string projectId)
        {
            var q = from x in Funs.DB.BS_Welder where x.WED_Code == WED_Code && x.ProjectId == projectId select x;
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
        /// 删除人员信息
        /// </summary>
        /// <param name="EDU_ID"></param>
        public static void DeleteBSWelder(string WED_ID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_Welder teamGroup = db.BS_Welder.First(e => e.WED_ID == WED_ID);
            db.BS_Welder.DeleteOnSubmit(teamGroup);
            db.SubmitChanges();
        }
      
        /// <summary>
        /// 根据人员Id获取一条人员信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static Model.BS_Welder GetWelderByWenId(string wenid)
        {
            return Funs.DB.BS_Welder.FirstOrDefault(x => x.WED_ID == wenid);
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWelderListByUnit(string projectId, string unitId)
        {
            var q = (from x in Funs.DB.BS_Welder where x.ProjectId == projectId && x.WED_Unit == unitId orderby x.WED_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].WED_Name ?? "", q[i].WED_ID.ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据班组获取人员
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWelderListByEduId(string projectId, string eduId)
        {
            var q = (from x in Funs.DB.BS_Welder where x.ProjectId == projectId && x.EDU_ID == eduId orderby x.WED_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].WED_Name ?? "", q[i].WED_ID.ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据单位获取焊工人员数
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static int GetPersonByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.BS_Welder where x.WED_Unit == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据班组获取焊工人员数
        /// </summary>
        /// <param name="eduId"></param>
        /// <returns></returns>
        public static int GetPersonByeduId(string eduId)
        {
            var q = (from x in Funs.DB.BS_Welder where x.EDU_ID == eduId select x).ToList();
            return q.Count();
        }
    }
}
