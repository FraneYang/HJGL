using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class WorkAreaService
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
        private static IQueryable<Model.Base_WorkArea> qq = from x in db.Base_WorkArea orderby x.UnitId, x.WorkAreaCode select x;

        /// <summary>
        /// 获取施工区域列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string searchItem, string searchValue, string projectId, string unitId, int startRowIndex, int maximumRows)
        {
            var unitInfo = from z in Funs.DB.Base_Unit where z.UnitId == unitId select z;

            IQueryable<Model.Base_WorkArea> q = qq;
            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.WorkAreaCode)
                    {
                        q = q.Where(e => e.WorkAreaCode.Contains(searchValue));
                    }
                }
            }
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }

            if (!string.IsNullOrEmpty(unitId) && unitInfo.First().UnitType=="2")
            {
                q = q.Where(e => e.UnitId == unitId);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.UnitId,  
                       x.WorkAreaId,
                       x.WorkAreaCode,
                       x.InstallationId,  
                       x.Def,
                       x.SupervisorUnitId,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int getListCount(string searchItem, string searchValue, string projectId, string unitId)
        {
            return count;
        }   

        /// <summary>
        /// 根据区域ID获取区域信息
        /// </summary>
        /// <param name="workAreaName"></param>
        /// <returns></returns>
        public static Model.Base_WorkArea getWorkAreaByWorkAreaId(string workAreaId)
        {
            return Funs.DB.Base_WorkArea.FirstOrDefault(e => e.WorkAreaId == workAreaId);
        }

        /// <summary>
        /// 根据区域Code获取区域信息
        /// </summary>
        /// <param name="workAreaName"></param>
        /// <returns></returns>
        public static bool IsExistWorkAreaCode(string projectId, string workAreaCode)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.Base_WorkArea where x.ProjectId == projectId && x.WorkAreaCode == workAreaCode select x;
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
        /// 添加作业区域
        /// </summary>
        /// <param name="workArea"></param>
        public static void AddWorkArea(Model.Base_WorkArea workArea)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Base_WorkArea));
            Model.Base_WorkArea newWorkArea = new Model.Base_WorkArea();
            newWorkArea.WorkAreaId = newKeyID;
            newWorkArea.WorkAreaCode = workArea.WorkAreaCode;
            newWorkArea.UnitId = workArea.UnitId;
            newWorkArea.InstallationId = workArea.InstallationId;
            newWorkArea.ProjectId = workArea.ProjectId;
            newWorkArea.Def = workArea.Def;
            newWorkArea.SupervisorUnitId = workArea.SupervisorUnitId;
            db.Base_WorkArea.InsertOnSubmit(newWorkArea);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业区域
        /// </summary>
        /// <param name="workArea"></param>
        public static void updateWorkArea(Model.Base_WorkArea workArea)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_WorkArea newWorkArea = db.Base_WorkArea.First(e => e.WorkAreaId == workArea.WorkAreaId)
;
            newWorkArea.WorkAreaCode = workArea.WorkAreaCode;
             newWorkArea.UnitId = workArea.UnitId;
            newWorkArea.InstallationId = workArea.InstallationId;
            newWorkArea.ProjectId = workArea.ProjectId;
            newWorkArea.Def = workArea.Def;
            newWorkArea.SupervisorUnitId = workArea.SupervisorUnitId;
            db.SubmitChanges();

        }

        /// <summary>
        /// 根据作业区域Id删除一个作业区域信息
        /// </summary>
        /// <param name="workAreaId"></param>
        public static void DeleteWorkArea(string workAreaId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_WorkArea workArea = db.Base_WorkArea.First(e => e.WorkAreaId == workAreaId);
            db.Base_WorkArea.DeleteOnSubmit(workArea);
            db.SubmitChanges();
        }

        /// <summary>
        /// 工作区域下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchList()
        {
            ListItem[] lis = new ListItem[1];
            lis[0] = new ListItem("作业区域编号", BLL.Const.WorkAreaCode);
            return lis;
        }

        /// <summary>
        /// 根据区域获得设置数
        /// </summary>
        /// <param name="installationId">设置主键</param>
        /// <returns>区域数</returns>
        public static int GeWorkAreaCountByInstallationId(int installationId)
        {
            var q = (from x in Funs.DB.Base_WorkArea where x.InstallationId == installationId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据单位获取数区域
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetWorkAreaCountByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.Base_WorkArea where x.UnitId == unitId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据区域代码获取区域信息
        /// </summary>
        /// <param name="workAreaCode"></param>
        /// <returns></returns>
        public static Model.Base_WorkArea GetWorkAreaByWorkAreaCode(string workAreaCode, string projectId)
        {
            return Funs.DB.Base_WorkArea.FirstOrDefault(x => x.WorkAreaCode == workAreaCode && x.ProjectId == projectId);
        }

        /// <summary>
        /// 根据单位获取区域编码项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWorkAreaListByUnit(string projectId,string unitId)
        {
            var q = (from x in Funs.DB.Base_WorkArea where x.UnitId == unitId && x.ProjectId == projectId orderby x.WorkAreaCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].WorkAreaCode ?? "", q[i].WorkAreaId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 获取区域编码项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWorkAreaList(string projectId)
        {
            var q = (from x in Funs.DB.Base_WorkArea where x.ProjectId == projectId orderby x.WorkAreaCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].WorkAreaCode ?? "", q[i].WorkAreaId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 根据单位获取区域编码项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWorkAreaListByInstallUnit(string projectId, string installId, string unitId)
        {
            var q = (from x in Funs.DB.Base_WorkArea where x.InstallationId.ToString() == installId && x.UnitId == unitId && x.ProjectId == projectId  orderby x.WorkAreaCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].WorkAreaCode ?? "", q[i].WorkAreaId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 根据单位获取区域编码项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWorkAreaListByInstallSupervisorUnit(string projectId, string installId, string unitId, string supervisorUnitId)
        {
            var q = (from x in Funs.DB.Base_WorkArea
                     where x.InstallationId.ToString() == installId
                     && x.UnitId == unitId && x.SupervisorUnitId == supervisorUnitId
                     && x.ProjectId == projectId
                     orderby x.WorkAreaCode
                     select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].WorkAreaCode ?? "", q[i].WorkAreaId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 根据单位获取区域编码项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWorkAreaListBySupervisorUnit(string projectId, string unitId, string supervisorUnitId)
        {
            var q = (from x in Funs.DB.Base_WorkArea
                     where ((x.UnitId == unitId && x.SupervisorUnitId == supervisorUnitId) || x.UnitId == supervisorUnitId)
                         && x.ProjectId == projectId
                     orderby x.WorkAreaCode
                     select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].WorkAreaCode ?? "", q[i].WorkAreaId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 根据单位获取区域编码项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWorkAreaListBySupervisor(string projectId, string supervisorUnitId)
        {
            var q = (from x in Funs.DB.Base_WorkArea
                     where ((x.SupervisorUnitId == supervisorUnitId) || x.UnitId == supervisorUnitId)
                         && x.ProjectId == projectId
                     orderby x.WorkAreaCode
                     select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].WorkAreaCode ?? "", q[i].WorkAreaId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 获取当前单位是否监理单位
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static bool IsSupervisor(string unitId, string projectId)
        {
            bool isSup = false;
            var unit = BLL.UnitService.GetUnit(unitId);
            if (unit != null && unit.UnitType == "4")
            {
                var sysSet = BLL.SysSetService.GetSysSet(4, projectId);
                if (sysSet != null && sysSet.IsAuto == true)
                {
                    isSup = true;
                }
            }
            return isSup;
        }
    }
}
